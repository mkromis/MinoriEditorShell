using MinoriEditorShell.Commands;
using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    public interface IMesCommandKeyGestureService
    {
        void BindKeyGestures(UIElement uiElement);
        KeyGesture GetPrimaryKeyGesture(MesCommandDefinitionBase commandDefinition);
    }
}
