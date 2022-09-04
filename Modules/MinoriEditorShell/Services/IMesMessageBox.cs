using System;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Client interface to an error message
    /// </summary>
    public interface IMesMessageBox
    {
        /// <summary>
        /// An box for the GUI message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        void Alert(string text, string caption);
    }
}