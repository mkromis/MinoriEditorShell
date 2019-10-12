using System;
using System.ComponentModel.Composition;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Services;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
#warning ViewAware
    [Export(typeof(IToolBars))]
    public class ToolBarsViewModel : MvxViewModel, IToolBars
    {
        public MvxObservableCollection<IToolBar> Items { get; }

        private Boolean _visible;
        public Boolean Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                RaisePropertyChanged();
            }
        }

        public IToolBarBuilder ToolBarBuilder { get; }

        [ImportingConstructor]
        public ToolBarsViewModel(IToolBarBuilder toolBarBuilder)
        {
            ToolBarBuilder = toolBarBuilder;
            Items = new MvxObservableCollection<IToolBar>();
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
