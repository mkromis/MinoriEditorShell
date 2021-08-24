using System;
using System.Drawing;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Helper to for default status bar
    /// </summary>
    public interface IMesStatusBar
    {
        /// <summary>
        ///  Set animationi icon
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Boolean Animation(Image image);

        /// <summary>
        /// Clear status
        /// </summary>
        /// <returns></returns>
        Boolean Clear();

        /// <summary>
        /// Get frozen status
        /// </summary>
        /// <returns></returns>
        Boolean FreezeOutput();

        /// <summary>
        /// Deterimine if status is frozen
        /// </summary>
        Boolean IsFrozen { get; }

        /// <summary>
        /// Status Text
        /// </summary>
        String Text { get; set; }

        /// <summary>
        /// Text Color
        /// </summary>
        Color Foreground { get; set; }

        /// <summary>
        /// Background Color
        /// </summary>
        Color Background { get; set; }

        /// <summary>
        /// Set Insert mode indicator
        /// </summary>
        Boolean? InsertMode { get; set; }

        /// <summary>
        /// Set Line Number
        /// </summary>
        Int32? LineNumber { get; set; }

        /// <summary>
        /// Set Character number
        /// </summary>
        Int32? CharPosition { get; set; }

        /// <summary>
        /// Set column number
        /// </summary>
        Int32? ColPosition { get; set; }

        /// <summary>
        ///  Set progress status
        /// </summary>
        /// <param name="On"></param>
        /// <param name="current"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Boolean Progress(Boolean On, UInt32 current, UInt32 total);
    }
}