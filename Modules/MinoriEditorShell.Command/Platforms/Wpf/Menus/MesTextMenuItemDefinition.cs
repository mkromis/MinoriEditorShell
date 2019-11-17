using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Menus
{
    public class MesTextMenuItemDefinition : MesMenuItemDefinition
    {
        private readonly string _text;
        private readonly Uri _iconSource;

        public override string Text
        {
            get { return _text; }
        }

        public override Uri IconSource
        {
            get { return _iconSource; }
        }

        public override KeyGesture KeyGesture
        {
            get { return null; }
        }

        public override MesCommandDefinitionBase CommandDefinition
        {
            get { return null; }
        }

        public MesTextMenuItemDefinition(MesMenuItemGroupDefinition group, int sortOrder, string text, Uri iconSource = null)
            : base(group, sortOrder)
        {
            _text = text;
            _iconSource = iconSource;
        }
    }
}
