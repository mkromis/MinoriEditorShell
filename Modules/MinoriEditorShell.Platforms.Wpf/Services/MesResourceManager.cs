using MinoriEditorShell.Services;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
	[Export(typeof(IMesResourceManager))]
	public class MesResourceManager : IMesResourceManager
	{
		public Stream GetStream(String relativeUri, String assemblyName)
		{
			try
			{
                StreamResourceInfo resource = Application.GetResourceStream(new Uri(assemblyName + ";component/" + relativeUri, UriKind.Relative))
					?? Application.GetResourceStream(new Uri(relativeUri, UriKind.Relative));

				return (resource != null)
					? resource.Stream
					: null;
			}
			catch
			{
				return null;
			}
		}

		public BitmapImage GetBitmap(string relativeUri, string assemblyName)
		{
            Stream s = GetStream(relativeUri, assemblyName);
			if (s == null) { return null; }

            using (s)
			{
                BitmapImage bmp = new BitmapImage();
				bmp.BeginInit();
				bmp.StreamSource = s;
				bmp.EndInit();
				bmp.Freeze();
				return bmp;
			}
		}

        public BitmapImage GetBitmap(string relativeUri) => GetBitmap(relativeUri, MesExtensionMethods.GetExecutingAssemblyName());
    }
}
