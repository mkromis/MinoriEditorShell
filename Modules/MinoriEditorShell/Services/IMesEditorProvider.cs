using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinoriEditorShell.Services
{
#warning Not Used
    /// <summary>
    /// Editor provider
    /// </summary>
    public interface IMesEditorProvider
    {
        /// <summary>
        /// Editor file types
        /// </summary>
        IEnumerable<MesEditorFileType> FileTypes { get; }
        /// <summary>
        /// Handles to document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Handles(string path);
        /// <summary>
        /// Create new document
        /// </summary>
        /// <returns></returns>
        IMesDocument Create();
        /// <summary>
        /// New empty Document (unsaved)
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task New(IMesDocument document, string name);
        /// <summary>
        /// Open document to model from path
        /// </summary>
        /// <param name="document"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task Open(IMesDocument document, string path);
    }
}