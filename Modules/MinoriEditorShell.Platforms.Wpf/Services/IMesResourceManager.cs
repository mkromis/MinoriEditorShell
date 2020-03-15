using System.IO;
using System.Windows.Media.Imaging;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
	public interface IMesResourceManager
	{
		Stream GetStream(string relativeUri, string assemblyName);
		BitmapImage GetBitmap(string relativeUri, string assemblyName);
		BitmapImage GetBitmap(string relativeUri);
	}
}
