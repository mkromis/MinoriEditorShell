using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.Design
{
    public class DesignTimeHistoryViewModel : HistoryViewModel
    {
        public DesignTimeHistoryViewModel()
            : base(null)
        {
            HistoryItems.Add(new HistoryItemViewModel("Initial") { ItemType = HistoryItemType.InitialState });
            HistoryItems.Add(new HistoryItemViewModel("Foo") { ItemType = HistoryItemType.Undo });
            HistoryItems.Add(new HistoryItemViewModel("Bar") { ItemType = HistoryItemType.Current });
            HistoryItems.Add(new HistoryItemViewModel("Baz") { ItemType = HistoryItemType.Redo });
        }
    }
}
