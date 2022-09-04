using System.IO;
using System.Windows.Media.Imaging;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    /// <summary>
    /// Resource Manager helper
    /// </summary>
    public interface IMesResourceManager
    {
        /// <summary>
        /// Get a stream to item uri
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        Stream GetStream(string relativePath, string assemblyName);
        /// <summary>
        /// Get bitmap from external assembly
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        BitmapImage GetBitmap(string relativePath, string assemblyName);
        /// <summary>
        /// Get embedded bitmap
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        BitmapImage GetBitmap(string relativePath);
    }
}