using System.ComponentModel.Composition;
using MinoriEditorStudio.Modules.ToolBars.Controls;
using MinoriEditorStudio.Modules.ToolBars.Views;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.ToolBars.ViewModels
{
#warning ViewAware
    [Export(typeof(IToolBars))]
    public class ToolBarsViewModel : MvxViewModel, IToolBars
    {
        private readonly MvxObservableCollection<IToolBar> _items;
        public MvxObservableCollection<IToolBar> Items
        {
            get { return _items; }
        }

        private readonly IToolBarBuilder _toolBarBuilder;

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged();
            }
        }

        [ImportingConstructor]
        public ToolBarsViewModel(IToolBarBuilder toolBarBuilder)
        {
            _toolBarBuilder = toolBarBuilder;
            _items = new MvxObservableCollection<IToolBar>();
        }

#warning OnViewLoaded
#if false
        protected override void OnViewLoaded(object view)
        {
            _toolBarBuilder.BuildToolBars(this);

            // TODO: Ideally, the ToolBarTray control would expose ToolBars
            // as a dependency property. We could use an attached property
            // to workaround this. But for now, toolbars need to be
            // created prior to the following code being run.
            foreach (var toolBar in Items)
                ((IToolBarsView) view).ToolBarTray.ToolBars.Add(new MainToolBar
                {
                    ItemsSource = toolBar
                });

            base.OnViewLoaded(view);
            base.ViewAppeared();
        }
#endif
    }
}
