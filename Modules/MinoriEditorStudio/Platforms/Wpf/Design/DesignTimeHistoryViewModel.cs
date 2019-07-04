using MinoriEditorStudio.Platforms.Wpf.ViewModels;
using MinoriEditorStudio.ViewModels;

namespace MinoriEditorStudio.Platforms.Wpf.Design
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
