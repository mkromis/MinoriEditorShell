using System.Windows;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
	public interface IInputManager
	{
		void SetShortcut(DependencyObject view, InputGesture gesture, object handler);
		void SetShortcut(InputGesture gesture, object handler);
	}
}
