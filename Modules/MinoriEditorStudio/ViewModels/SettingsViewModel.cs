using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MinoriEditorStudio.Extensions;
using MinoriEditorStudio.Properties;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Localization;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Views;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    public class SettingsViewModel : WindowBase, ISettingsManager
    {
        private IEnumerable<ISettingsEditor> _settingsEditors;
        private SettingsPageViewModel _selectedPage;

        public SettingsViewModel(
            IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService)
        {
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
            OkCommand = new MvxCommand(SaveChanges);

            DisplayName = Resources.SettingsDisplayName;
        }

        public List<SettingsPageViewModel> Pages { get; private set; }

        public SettingsPageViewModel SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                RaisePropertyChanged(() => SelectedPage);
            }
        }


        public ICommand CancelCommand { get; private set; }
        public ICommand OkCommand { get; private set; }

        public override async Task Initialize()
        {
            IMvxViewsContainer viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            await base.Initialize();

            List<SettingsPageViewModel> pages = new List<SettingsPageViewModel>();
            _settingsEditors = Mvx.IoCProvider.GetAll<ISettingsEditor>();

            foreach (ISettingsEditor settingsEditor in _settingsEditors)
            {
                if (settingsEditor == null) { throw new InvalidProgramException("ISettingsEditor Missing");  }
                List<SettingsPageViewModel> parentCollection = GetParentCollection(settingsEditor, pages);

                SettingsPageViewModel page =
                    parentCollection.FirstOrDefault(m => m.Name == settingsEditor.SettingsPageName);

                if (page == null)
                {
                    page = new SettingsPageViewModel
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

        private static SettingsPageViewModel GetFirstLeafPageRecursive(List<SettingsPageViewModel> pages)
        {
            if (!pages.Any())
            {
                return null;
            }

            SettingsPageViewModel firstPage = pages.First();
            return !firstPage.Children.Any() ? firstPage : GetFirstLeafPageRecursive(firstPage.Children);
        }

        private List<SettingsPageViewModel> GetParentCollection(
            ISettingsEditor settingsEditor,
            List<SettingsPageViewModel> pages)
        {
            if (String.IsNullOrEmpty(settingsEditor.SettingsPagePath))
            {
                return pages;
            }

            String[] path = settingsEditor.SettingsPagePath.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (String pathElement in path)
            {
                SettingsPageViewModel page = pages.FirstOrDefault(s => s.Name == pathElement);

                if (page == null)
                {
                    page = new SettingsPageViewModel {Name = pathElement};
                    pages.Add(page);
                }

                pages = page.Children;
            }

            return pages;
        }

        private void SaveChanges()
        {
            foreach (ISettingsEditor settingsEditor in _settingsEditors)
            {
                settingsEditor.ApplyChanges();
            }

            NavigationService.Close(this);
        }

        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name);

    }
}
