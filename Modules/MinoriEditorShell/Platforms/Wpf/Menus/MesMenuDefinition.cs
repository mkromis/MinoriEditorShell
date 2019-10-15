using System;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;

namespace MinoriEditorShell.Platforms.Wpf.Menus
{
    public class MesMenuDefinition : MesMenuDefinitionBase
    {
        private readonly MesMenuBarDefinition _menuBar;
        private readonly int _sortOrder;
        private readonly string _text;

        public MesMenuBarDefinition MenuBar => _menuBar;

        public override int SortOrder => _sortOrder;

        public override string Text => _text;

        public override Uri IconSource => null;

        public override KeyGesture KeyGesture => null;

        public override MesCommandDefinitionBase CommandDefinition => null;

        public MesMenuDefinition(MesMenuBarDefinition menuBar, int sortOrder, string text)
        {
            _menuBar = menuBar;
            _sortOrder = sortOrder;
            _text = text;
        }
    }
}
