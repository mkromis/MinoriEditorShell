using System;
using System.ComponentModel.Composition;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Services;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Platforms.Wpf.ViewModels
{
#warning ViewAware
    [Export(typeof(IMesToolBars))]
    public class MesToolBarsViewModel : MvxViewModel, IMesToolBars
    {
        public MvxObservableCollection<IMesToolBar> Items { get; }

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

        public IMesToolBarBuilder ToolBarBuilder { get; }

        [ImportingConstructor]
        public MesToolBarsViewModel(IMesToolBarBuilder toolBarBuilder)
        {
            ToolBarBuilder = toolBarBuilder;
            Items = new MvxObservableCollection<IMesToolBar>();
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
