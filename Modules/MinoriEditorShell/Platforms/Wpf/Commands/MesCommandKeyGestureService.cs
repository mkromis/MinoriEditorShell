using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Services;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [Export(typeof(IMesCommandKeyGestureService))]
    public class MesCommandKeyGestureService : IMesCommandKeyGestureService
    {
        private readonly MesCommandKeyboardShortcut[] _keyboardShortcuts;
        private readonly IMesCommandService _commandService;

        [ImportingConstructor]
        public MesCommandKeyGestureService(
            [ImportMany] MesCommandKeyboardShortcut[] keyboardShortcuts,
            [ImportMany] MesExcludeCommandKeyboardShortcut[] excludeKeyboardShortcuts,
            IMesCommandService commandService)
        {
            _keyboardShortcuts = keyboardShortcuts
                .Except(excludeKeyboardShortcuts.Select(x => x.KeyboardShortcut))
                .OrderBy(x => x.SortOrder)
                .ToArray();
            _commandService = commandService;
        }

        public void BindKeyGestures(UIElement uiElement)
        {
            foreach (var keyboardShortcut in _keyboardShortcuts)
                if (keyboardShortcut.KeyGesture != null)
                    uiElement.InputBindings.Add(new InputBinding(
                        _commandService.GetTargetableCommand(_commandService.GetCommand(keyboardShortcut.CommandDefinition)),
                        keyboardShortcut.KeyGesture));
        }

        public KeyGesture GetPrimaryKeyGesture(MesCommandDefinitionBase commandDefinition)
        {
            var keyboardShortcut = _keyboardShortcuts.FirstOrDefault(x => x.CommandDefinition == commandDefinition);
            return keyboardShortcut?.KeyGesture;
        }
    }
}
