using System.IO;
// using System.Windows.Media.Imaging;

namespace MinoriEditorShell.Platforms.Avalonia.Services
{
    public interface IMesResourceManager
    {
        Stream GetStream(string relativeUri, string assemblyName);

        // BitmapImage GetBitmap(string relativeUri, string assemblyName);

        // BitmapImage GetBitmap(string relativeUri);
    }
}