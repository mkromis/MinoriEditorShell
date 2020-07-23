using System;
using System.Drawing;

namespace MinoriEditorShell.Services
{
    public interface IMesStatusBar
    {
        Boolean Animation(Image image);

        Boolean Clear();

        Boolean FreezeOutput();

        Boolean IsFrozen { get; }
        String Text { get; set; }
        Color Foreground { get; set; }
        Color Background { get; set; }
        Boolean? InsertMode { get; set; }
        Int32? LineNumber { get; set; }
        Int32? CharPosition { get; set; }
        Int32? ColPosition { get; set; }

        Boolean Progress(Boolean On, UInt32 current, UInt32 total);
    }
}