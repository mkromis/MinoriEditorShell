using MvvmCross.Commands;
using MvvmCross.Views;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    /// <inheritdoc cref="IMesTool"/>
    public abstract class MesTool : MesLayoutItemBase, IMesTool
    {
        private bool _isVisible;

        /// <inheritdoc />
        public override ICommand CloseCommand => new MvxCommand(() => IsVisible = false);
        /// <inheritdoc />
        public abstract MesPaneLocation PreferredLocation { get; }
        /// <inheritdoc />
        public virtual double PreferredWidth => 200;
        /// <inheritdoc />
        public virtual double PreferredHeight => 200;


        /// <inheritdoc />
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

        /// <inheritdoc />
        public override bool ShouldReopenOnStart => true;
        /// <inheritdoc />
        public IMvxView View { get; set; }
        /// <inheritdoc />
        public bool CanClose { get; } = true;
        /// <inheritdoc />
        protected MesTool() => IsVisible = true;
    }
}