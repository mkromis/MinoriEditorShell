using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MinoriEditorShell.Extensions;
using MinoriEditorShell.Properties;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    public class MesSettingsViewModel : MvxNavigationViewModel, IMesSettingsManager
    {
        private IEnumerable<IMesSettings> _settingsEditors;
        private MesSettingsPageViewModel _selectedPage;
        private String displayName;

        public MesSettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            DisplayName = Resources.SettingsDisplayName;
        }

        public List<MesSettingsPageViewModel> Pages { get; private set; }

        public MesSettingsPageViewModel SelectedPage
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

            List<MesSettingsPageViewModel> pages = new List<MesSettingsPageViewModel>();
            _settingsEditors = Mvx.IoCProvider.GetAll<IMesSettings>();

            foreach (IMesSettings settingsEditor in _settingsEditors)
            {
                if (settingsEditor == null) { throw new InvalidProgramException("ISettingsEditor Missing"); }
                List<MesSettingsPageViewModel> parentCollection = GetParentCollection(settingsEditor, pages);

                MesSettingsPageViewModel page =
                    parentCollection.FirstOrDefault(m => m.Name == settingsEditor.SettingsPageName);

                if (page == null)
                {
                    page = new MesSettingsPageViewModel
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

        private static MesSettingsPageViewModel GetFirstLeafPageRecursive(List<MesSettingsPageViewModel> pages)
        {
            if (!pages.Any())
            {
                return null;
            }

            MesSettingsPageViewModel firstPage = pages.First();
            return !firstPage.Children.Any() ? firstPage : GetFirstLeafPageRecursive(firstPage.Children);
        }

        private List<MesSettingsPageViewModel> GetParentCollection(
            IMesSettings settingsEditor,
            List<MesSettingsPageViewModel> pages)
        {
            if (String.IsNullOrEmpty(settingsEditor.SettingsPagePath))
            {
                return pages;
            }

            String[] path = settingsEditor.SettingsPagePath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (String pathElement in path)
            {
                MesSettingsPageViewModel page = pages.FirstOrDefault(s => s.Name == pathElement);

                if (page == null)
                {
                    page = new MesSettingsPageViewModel { Name = pathElement };
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
