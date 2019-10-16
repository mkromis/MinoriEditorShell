using MinoriEditorShell.Services;
using System.Windows;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    public interface IMesMainWindow
    {
        WindowState WindowState { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        string Title { get; set; }
        ImageSource Icon { get; set; } 

        IMesManager Shell { get; }
    }
}
