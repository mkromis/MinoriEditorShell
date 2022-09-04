using System.Threading.Tasks;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Document that persists over restart
    /// </summary>
    public interface IMesPersistedDocument : IMesDocument
    {
        /// <summary>
        /// New document without save path
        /// </summary>
        bool IsNew { get; }
        /// <summary>
        /// Short file name useful for view
        /// </summary>
        string FileName { get; }
        /// <summary>
        ///  Full file path
        /// </summary>
        string FilePath { get; }
        /// <summary>
        /// New document
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task New(string fileName);
        /// <summary>
        /// Load document
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task Load(string filePath);
        /// <summary>
        /// Save document
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task Save(string filePath);
    }
}