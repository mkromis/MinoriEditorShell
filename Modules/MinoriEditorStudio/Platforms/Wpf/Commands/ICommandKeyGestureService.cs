using System.Windows;
using System.Windows.Input;

namespace MinoriEditorStudio.Platforms.Wpf.Commands
{
    public interface ICommandKeyGestureService
    {
        void BindKeyGestures(UIElement uiElement);
        KeyGesture GetPrimaryKeyGesture(CommandDefinitionBase commandDefinition);
    }
}
