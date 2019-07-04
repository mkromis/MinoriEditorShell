using System.IO;
using System.Windows.Media.Imaging;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
	public interface IResourceManager
	{
		Stream GetStream(string relativeUri, string assemblyName);
		BitmapImage GetBitmap(string relativeUri, string assemblyName);
		BitmapImage GetBitmap(string relativeUri);
	}
}
