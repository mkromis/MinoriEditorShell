using Microsoft.Extensions.Logging;
using MinoriEditorShell.DataClasses;
using MinoriEditorShell.Extensions;
using MinoriEditorShell.Properties;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    public class MesSettingsManagerViewModel : MvxNavigationViewModel, IMesSettingsManager
    {
        private IEnumerable<IMesSettings> _settingsEditors;
        private MesSettingsTreeItem _selectedPage;
        private String displayName;

        public MesSettingsManagerViewModel(ILoggerFactory logger, IMvxNavigationService navigationService)
            : base(logger, navigationService)
        {
            DisplayName = Resources.SettingsDisplayName;
        }

        public List<MesSettingsTreeItem> Pages { get; private set; }

        public MesSettingsTreeItem SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                RaisePropertyChanged(() => SelectedPage);
            }
        }

        public ICommand CancelCommand => new MvxCommand(() => NavigationService.Close(this));
        public ICommand OkCommand => new MvxCommand(SaveChanges);
        public String DisplayName { get => displayName; set => SetProperty(ref displayName, value); }

        public IMvxCommand ShowCommand => new MvxCommand(() =>
        {
            IMesSettingsManager settingsManager = Mvx.IoCProvider.Resolve<IMesSettingsManager>();
            NavigationService.Navigate(settingsManager);
        });

        public override async Task Initialize()
        {
            IMvxViewsContainer viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            await base.Initialize();

            List<MesSettingsTreeItem> pages = new List<MesSettingsTreeItem>();
            _settingsEditors = Mvx.IoCProvider.GetAll<IMesSettings>();

            foreach (IMesSettings settingsEditor in _settingsEditors)
            {
                if (settingsEditor == null) { throw new InvalidProgramException("ISettingsEditor Missing"); }
                List<MesSettingsTreeItem> parentCollection = GetParentCollection(settingsEditor, pages);

                MesSettingsTreeItem page =
                    parentCollection.FirstOrDefault(m => m.Name == settingsEditor.SettingsPageName);

                if (page == null)
                {
                    page = new MesSettingsTreeItem
                    {
                        Name = settingsEditor.SettingsPageName,
                    };
                    parentCollection.Add(page);
                }

                // Try to create view/viewmodel
                // we already have viewmodel, go get view type
                Type type = viewFinder.GetViewType(settingsEditor.GetType());
                IMvxView view = (IMvxView)Activator.CreateInstance(type);

                // Assign them to each other.
                settingsEditor.View = view;
                view.ViewModel = settingsEditor;

                page.Editors.Add(settingsEditor);
            }

            Pages = pages;
            SelectedPage = GetFirstLeafPageRecursive(pages);
        }

        private static MesSettingsTreeItem GetFirstLeafPageRecursive(List<MesSettingsTreeItem> pages)
        {
            if (!pages.Any())
            {
                return null;
            }

            MesSettingsTreeItem firstPage = pages.First();
            return !firstPage.Children.Any() ? firstPage : GetFirstLeafPageRecursive(firstPage.Children);
        }

        private List<MesSettingsTreeItem> GetParentCollection(
            IMesSettings settingsEditor,
            List<MesSettingsTreeItem> pages)
        {
            if (String.IsNullOrEmpty(settingsEditor.SettingsPagePath))
            {
                return pages;
            }

            String[] path = settingsEditor.SettingsPagePath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (String pathElement in path)
            {
                MesSettingsTreeItem page = pages.FirstOrDefault(s => s.Name == pathElement);

                if (page == null)
                {
                    page = new MesSettingsTreeItem { Name = pathElement };
                    pages.Add(page);
                }

                pages = page.Children;
            }

            return pages;
        }

        private void SaveChanges()
        {
            foreach (IMesSettings settingsEditor in _settingsEditors)
            {
                settingsEditor.ApplyChanges();
            }

            NavigationService.Close(this);
        }
    }
}