using MinoriEditorShell.Commands;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public interface ICommandKeyGestureService
    {
        void BindKeyGestures(UIElement uiElement);
        KeyGesture GetPrimaryKeyGesture(CommandDefinitionBase commandDefinition);
    }
}
