using System;
using System.Windows.Input;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;

namespace MinoriEditorShell.Platforms.Wpf.Menus
{
    public class MenuDefinition : MenuDefinitionBase
    {
        private readonly MenuBarDefinition _menuBar;
        private readonly int _sortOrder;
        private readonly string _text;

        public MenuBarDefinition MenuBar => _menuBar;

        public override int SortOrder => _sortOrder;

        public override string Text => _text;

        public override Uri IconSource => null;

        public override KeyGesture KeyGesture => null;

        public override CommandDefinitionBase CommandDefinition => null;

        public MenuDefinition(MenuBarDefinition menuBar, int sortOrder, string text)
        {
            _menuBar = menuBar;
            _sortOrder = sortOrder;
            _text = text;
        }
    }
}
