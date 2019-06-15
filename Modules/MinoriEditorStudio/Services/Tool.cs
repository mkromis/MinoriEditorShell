using System;
using System.Windows.Input;
using MinoriEditorStudio.Framework.Services;
//using MinoriEditorStudio.Framework.ToolBars;
using MinoriEditorStudio.Modules.ToolBars;
using MinoriEditorStudio.Modules.ToolBars.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Views;

namespace MinoriEditorStudio.Framework
{
	public abstract class Tool : LayoutItemBase, ITool
	{
		private ICommand _closeCommand;
        public override ICommand CloseCommand => new MvxCommand(() => IsVisible = false);

	    public abstract PaneLocation PreferredLocation { get; }

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

        public override Boolean ShouldReopenOnStart => true;

        public IMvxView View { get; set; }
        public Boolean CanClose { get; }

        protected Tool()
		{
			IsVisible = true;
		}
	}
}
