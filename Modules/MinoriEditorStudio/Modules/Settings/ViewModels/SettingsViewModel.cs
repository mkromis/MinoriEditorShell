using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Properties;
using MvvmCross.Commands;

namespace MinoriEditorStudio.Modules.Settings.ViewModels
{
    [Export(typeof (SettingsViewModel))]
    public class SettingsViewModel : WindowBase
    {
        private IEnumerable<ISettingsEditor> _settingsEditors;
        private SettingsPageViewModel _selectedPage;

        public SettingsViewModel()
        {
#warning TryClose
            CancelCommand = null; // new MvxCommand(() => /*TryClose(false)*/);
            OkCommand = null; // new MvxCommand(SaveChanges);

            DisplayName = Resources.SettingsDisplayName;
        }

        public List<SettingsPageViewModel> Pages { get; private set; }

        public SettingsPageViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                RaisePropertyChanged(() => SelectedPage);
            }
        }

        public ICommand CancelCommand { get; private set; }
        public ICommand OkCommand { get; private set; }

        #warning OnInit
#if false
        protected override void OnInitialize()
        {
            base.OnInitialize();

            var pages = new List<SettingsPageViewModel>();
            _settingsEditors = IoC.GetAll<ISettingsEditor>();

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
#endif

        private static SettingsPageViewModel GetFirstLeafPageRecursive(List<SettingsPageViewModel> pages)
        {
            if (!pages.Any())
                return null;

            var firstPage = pages.First();
            if (!firstPage.Children.Any())
                return firstPage;

            return GetFirstLeafPageRecursive(firstPage.Children);
        }

        private List<SettingsPageViewModel> GetParentCollection(ISettingsEditor settingsEditor,
            List<SettingsPageViewModel> pages)
        {
            if (string.IsNullOrEmpty(settingsEditor.SettingsPagePath))
            {
                return pages;
            }

            string[] path = settingsEditor.SettingsPagePath.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pathElement in path)
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

        private void SaveChanges(object obj)
        {
            foreach (ISettingsEditor settingsEditor in _settingsEditors)
            {
                settingsEditor.ApplyChanges();
            }

            throw new NotImplementedException();
            //TryClose(true);
        }
    }
}
