using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.Design
{
    public class MesDesignTimeHistoryViewModel : MesHistoryViewModel
    {
        public MesDesignTimeHistoryViewModel()
            : base(null)
        {
            HistoryItems.Add(new MesHistoryItemViewModel("Initial") { ItemType = MesHistoryItemType.InitialState });
            HistoryItems.Add(new MesHistoryItemViewModel("Foo") { ItemType = MesHistoryItemType.Undo });
            HistoryItems.Add(new MesHistoryItemViewModel("Bar") { ItemType = MesHistoryItemType.Current });
            HistoryItems.Add(new MesHistoryItemViewModel("Baz") { ItemType = MesHistoryItemType.Redo });
        }
    }
}
