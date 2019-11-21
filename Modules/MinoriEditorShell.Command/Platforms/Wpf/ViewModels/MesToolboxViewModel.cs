using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MvvmCross.Commands;
using System.Collections.ObjectModel;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
    [Export(typeof(IMesToolbox))]
    public class MesToolboxViewModel : MesTool, IMesToolbox
    {

        private MvxCommand _searchCommand;

        public ICommand SearchCommand
        {
            get => throw new NotImplementedException();
            //get { return _searchCommand == null ? _searchCommand = new MvxCommand<String>(a => Search(a as string)) : _searchCommand; }
        }

        private readonly IMesToolboxService _toolboxService;

        public override MesPaneLocation PreferredLocation => MesPaneLocation.Left;

        private ObservableCollection<MesToolboxItemViewModel> _filteredItems;

        private readonly ObservableCollection<MesToolboxItemViewModel> _items;
        public ObservableCollection<MesToolboxItemViewModel> Items => _filteredItems.Count == 0 ? _items : _filteredItems;

        [ImportingConstructor]
        public MesToolboxViewModel(IMesManager shell, IMesToolboxService toolboxService)
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

        private void RefreshToolboxItems(IMesManager shell)
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
