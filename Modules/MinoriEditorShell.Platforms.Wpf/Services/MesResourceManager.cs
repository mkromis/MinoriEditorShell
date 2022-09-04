using MinoriEditorShell.Services;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    /// <inheritdoc cref="IMesResourceManager" />
    public class MesResourceManager : IMesResourceManager
    {
        /// <inheritdoc />
        public Stream GetStream(string relativePath, string assemblyName)
        {
            StreamResourceInfo resource = Application.GetResourceStream(new Uri(assemblyName + ";component/" + relativePath, UriKind.Relative))
                ?? Application.GetResourceStream(new Uri(relativePath, UriKind.Relative));

            return resource?.Stream;
        }

        /// <inheritdoc />
        public BitmapImage GetBitmap(string relativePath, string assemblyName)
        {
            Stream s = GetStream(relativePath, assemblyName);
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

        /// <inheritdoc />
        public BitmapImage GetBitmap(string relativePath) => GetBitmap(relativePath, Assembly.GetExecutingAssembly().GetName().FullName);
    }
}