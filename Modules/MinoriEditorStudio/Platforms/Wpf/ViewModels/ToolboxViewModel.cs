using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Toolbox.Services;
using MinoriEditorStudio.Properties;
using System.Windows.Input;
using MvvmCross.Commands;
using System.Collections.ObjectModel;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.Platforms.Wpf.ViewModels
{
    [Export(typeof(IToolbox))]
    public class ToolboxViewModel : Tool, IToolbox
    {

        private MvxCommand _searchCommand;

        public ICommand SearchCommand
        {
            get => throw new NotImplementedException();
            //get { return _searchCommand == null ? _searchCommand = new MvxCommand<String>(a => Search(a as string)) : _searchCommand; }
        }

        private readonly IToolboxService _toolboxService;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        private ObservableCollection<ToolboxItemViewModel> _filteredItems;

        private readonly ObservableCollection<ToolboxItemViewModel> _items;
        public ObservableCollection<ToolboxItemViewModel> Items
        {
            get { return _filteredItems.Count == 0 ? _items : _filteredItems; }
        }

        [ImportingConstructor]
        public ToolboxViewModel(IManager shell, IToolboxService toolboxService)
        {
            throw new NotImplementedException();
#if false
            DisplayName = Resources.ToolboxDisplayName;

            _items = new BindableCollection<ToolboxItemViewModel>();
            _filteredItems = new BindableCollection<ToolboxItemViewModel>();

            var groupedItems = CollectionViewSource.GetDefaultView(_items);
            groupedItems.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

            _toolboxService = toolboxService;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            shell.ActiveDocumentChanged += (sender, e) => RefreshToolboxItems(shell);
            RefreshToolboxItems(shell);
#endif
        }

        private void RefreshToolboxItems(IManager shell)
        {
            throw new NotImplementedException();
#warning RefreshToolboxItems
#if false
            _items.Clear();

            if (shell.ActiveItem == null) 
                return;

            _items.AddRange(_toolboxService.GetToolboxItems(shell.ActiveItem.GetType())
                .Select(x => new ToolboxItemViewModel(x)));
#endif
        }

#warning Search
#if false
        private void Search(string searchTerm)
        {
            _filteredItems.Clear();
            if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm.Length >= 2)
            {
                foreach (var item in _items.Where(x => x.Name.ToUpper().Contains(searchTerm.ToUpper()) || x.Name.ToUpper().Equals(searchTerm.ToUpper())))
                    _filteredItems.Add(item);
            }
            NotifyOfPropertyChange(() => Items);
        }
#endif
    }
}
