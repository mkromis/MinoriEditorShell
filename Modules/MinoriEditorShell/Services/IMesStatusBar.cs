using System;
using System.Drawing;

namespace MinoriEditorShell.Services
{
    /// <summary>
    /// Optional status bar
    /// </summary>
    public interface IMesStatusBar
    {
        /// <summary>
        /// icon animation in task bar
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        bool Animation(Image image);
        /// <summary>
        /// Clear status bar
        /// </summary>
        /// <returns></returns>
        bool Clear();
        /// <summary>
        /// Make taskbar frozen
        /// </summary>
        /// <returns></returns>
        bool FreezeOutput();
        /// <summary>
        /// Weather output is frozen
        /// </summary>
        bool IsFrozen { get; }
        /// <summary>
        /// Status text
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// Foreground color
        /// </summary>
        Color Foreground { get; set; }
        /// <summary>
        /// Background color
        /// </summary>
        Color Background { get; set; }
        /// <summary>
        /// Insert mode indicator
        /// </summary>
        bool? InsertMode { get; set; }
        /// <summary>
        /// Line number
        /// </summary>
        int? LineNumber { get; set; }
        /// <summary>
        ///  Character position
        /// </summary>
        int? CharPosition { get; set; }
        /// <summary>
        /// Column position
        /// </summary>
        int? ColPosition { get; set; }
        /// <summary>
        ///  Progress bar
        /// </summary>
        /// <param name="On">To show or not</param>
        /// <param name="current">current value</param>
        /// <param name="total">Text</param>
        bool Progress(bool On, uint current, uint total);
    }
}