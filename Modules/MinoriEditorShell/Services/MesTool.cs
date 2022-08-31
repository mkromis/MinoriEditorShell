using MvvmCross.Commands;
using MvvmCross.Views;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Abstract tool window. This is the dockable window on side of main window.
    /// </summary>
    public abstract class MesTool : MesLayoutItemBase, IMesTool
    {
        private readonly ICommand _closeCommand;
        public override ICommand CloseCommand => new MvxCommand(() => IsVisible = false);

        public abstract MesPaneLocation PreferredLocation { get; }

        public virtual double PreferredWidth => 200;

        public virtual double PreferredHeight => 200;

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

#warning Fix toolbar
#if false
        private ToolBarDefinition _toolBarDefinition;
        public ToolBarDefinition ToolBarDefinition
        {
            get { return _toolBarDefinition; }
            protected set
            {
                _toolBarDefinition = value;
                RaisePropertyChanged(() => ToolBar);
                RaisePropertyChanged(() => ToolBarDefinition);
            }
        }

	    private IToolBar _toolBar;
        public IToolBar ToolBar
        {
            get
            {
                if (_toolBar != null)
                    return _toolBar;

                if (ToolBarDefinition == null)
                    return null;

                var toolBarBuilder = Mvx.IoCProvider.Resolve<IToolBarBuilder>();
                _toolBar = new ToolBarModel();
                toolBarBuilder.BuildToolBar(ToolBarDefinition, _toolBar);
                return _toolBar;
            }
        }
#endif

        public override bool ShouldReopenOnStart => true;

        public IMvxView View { get; set; }
        public bool CanClose { get; }

        protected MesTool() => IsVisible = true;
    }
}