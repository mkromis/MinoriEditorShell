using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.ToolBars;
using MvvmCross;

namespace MinoriEditorStudio.Modules.ToolBars.Models
{
	public class CommandToolBarItem : ToolBarItemBase, ICommandUiItem
    {
	    private readonly ToolBarItemDefinition _toolBarItem;
	    private readonly Command _command;
        private readonly KeyGesture _keyGesture;
        private readonly IToolBar _parent;

        public string Text => _command.Text;

        public ToolBarItemDisplay Display => _toolBarItem.Display;

        public Uri IconSource => _command.IconSource;

        public string ToolTip
	    {
	        get
	        {
                var inputGestureText = (_keyGesture != null)
                    ? string.Format(" ({0})", _keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture))
                    : string.Empty;

                return string.Format("{0}{1}", _command.ToolTip, inputGestureText).Trim();
	        }
	    }

        public bool HasToolTip => !string.IsNullOrWhiteSpace(ToolTip);

        public ICommand Command => Mvx.IoCProvider.Resolve<ICommandService>().GetTargetableCommand(_command);

        public bool IsChecked => _command.Checked;

        public CommandToolBarItem(ToolBarItemDefinition toolBarItem, Command command, IToolBar parent)
		{
		    _toolBarItem = toolBarItem;
		    _command = command;
            _keyGesture = Mvx.IoCProvider.Resolve<ICommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            command.PropertyChanged += OnCommandPropertyChanged;
		}

        private void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => Text);
            RaisePropertyChanged(() => IconSource);
            RaisePropertyChanged(() => ToolTip);
            RaisePropertyChanged(() => HasToolTip);
            RaisePropertyChanged(() => IsChecked);
        }

        CommandDefinitionBase ICommandUiItem.CommandDefinition => _command.CommandDefinition;

        void ICommandUiItem.Update(CommandHandlerWrapper commandHandler)
	    {
	        // TODO
	    }
    }
}
