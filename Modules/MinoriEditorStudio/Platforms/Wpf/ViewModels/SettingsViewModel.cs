using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Properties;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    public class SettingsViewModel : WindowBase, ISettingsManager
    {
        private IEnumerable<ISettingsEditor> _settingsEditors;
        private SettingsPageViewModel _selectedPage;

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //#warning TryClose
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
            await base.Initialize();

            var pages = new List<SettingsPageViewModel>();
            _settingsEditors = Mvx.IoCProvider.GetAll<ISettingsEditor>();

            foreach (ISettingsEditor settingsEditor in _settingsEditors)
            {
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
    }
}
