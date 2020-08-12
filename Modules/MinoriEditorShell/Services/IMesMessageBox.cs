using System;

namespace MinoriEditorShell.Services
{
    public interface IMesMessageBox
    {
        void Alert(String text, String caption);
    }
}