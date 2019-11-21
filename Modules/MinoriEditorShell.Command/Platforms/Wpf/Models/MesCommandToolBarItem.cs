using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Services;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Models
{
	public class MesCommandToolBarItem : MesToolBarItemBase, IMesCommandUiItem
    {
	    private readonly MesToolBarItemDefinition _toolBarItem;
	    private readonly MesCommand _command;
        private readonly KeyGesture _keyGesture;
        private readonly IMesToolBar _parent;

        public String Text => _command.Text;

        public ToolBarItemDisplay Display => _toolBarItem.Display;

        public Uri IconSource => _command.IconSource;

        public string ToolTip
	    {
	        get
	        {
                String inputGestureText = (_keyGesture != null)
                    ? String.Format(" ({0})", _keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture))
                    : String.Empty;

                return String.Format("{0}{1}", _command.ToolTip, inputGestureText).Trim();
	        }
	    }

        public Boolean HasToolTip => !String.IsNullOrWhiteSpace(ToolTip);

        public ICommand Command => Mvx.IoCProvider.Resolve<IMesCommandService>().GetTargetableCommand(_command);

        public Boolean IsChecked => _command.Checked;

        public MesCommandToolBarItem(MesToolBarItemDefinition toolBarItem, MesCommand command, IMesToolBar parent)
		{
		    _toolBarItem = toolBarItem;
		    _command = command;
            _keyGesture = Mvx.IoCProvider.Resolve<IMesCommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            command.PropertyChanged += OnCommandPropertyChanged;
		}

        private void OnCommandPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => Text);
            RaisePropertyChanged(() => IconSource);
            RaisePropertyChanged(() => ToolTip);
            RaisePropertyChanged(() => HasToolTip);
            RaisePropertyChanged(() => IsChecked);
        }

        MesCommandDefinitionBase IMesCommandUiItem.CommandDefinition => _command.CommandDefinition;

        void IMesCommandUiItem.Update(MesCommandHandlerWrapper commandHandler)
	    {
	        // TODO
	    }
    }
}
