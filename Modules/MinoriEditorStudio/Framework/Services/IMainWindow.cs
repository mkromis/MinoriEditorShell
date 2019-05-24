using System.Windows;
using System.Windows.Media;

namespace MinoriEditorStudio.Framework.Services
{
    public interface IMainWindow
    {
        WindowState WindowState { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        string Title { get; set; }
        ImageSource Icon { get; set; } 

        IManager Shell { get; }
    }
}
