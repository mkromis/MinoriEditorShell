using System;
using System.Drawing;

namespace MinoriEditorShell.Services
{
    public interface IMesStatusBar
    {
        bool Animation(Image image);

        bool Clear();

        bool FreezeOutput();

        bool IsFrozen { get; }
        string Text { get; set; }
        Color Foreground { get; set; }
        Color Background { get; set; }
        bool? InsertMode { get; set; }
        int? LineNumber { get; set; }
        int? CharPosition { get; set; }
        int? ColPosition { get; set; }

        bool Progress(bool On, uint current, uint total);
    }
}