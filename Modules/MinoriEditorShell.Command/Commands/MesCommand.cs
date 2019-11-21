using MvvmCross.ViewModels;
using System;

namespace MinoriEditorShell.Commands
{
    public class MesCommand : MvxNotifyPropertyChanged
    {
        private bool _visible = true;
        private bool _enabled = true;
        private bool _checked;
        private string _text;
        private string _toolTip;
        private Uri _iconSource;

        public MesCommandDefinitionBase CommandDefinition { get; }

        public bool Visible
        {
            get => _visible;
            set => SetProperty(ref _visible, value);
        }

        public bool Enabled
        {
            get => _enabled;
            set => SetProperty(ref _enabled, value);
        }

        public bool Checked
        {
            get => _checked;
            set => SetProperty(ref _checked, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        public Uri IconSource
        {
            get => _iconSource;
            set => SetProperty(ref _iconSource, value);
        }

        public object Tag { get; set; }

        public MesCommand(MesCommandDefinitionBase commandDefinition)
        {
            CommandDefinition = commandDefinition;
            Text = commandDefinition.Text;
            ToolTip = commandDefinition.ToolTip;
            IconSource = commandDefinition.IconSource;
        }
    }
}
