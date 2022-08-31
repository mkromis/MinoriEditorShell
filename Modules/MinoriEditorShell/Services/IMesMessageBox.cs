using System;

namespace MinoriEditorShell.Services
{
    public interface IMesMessageBox
    {
        void Alert(string text, string caption);
    }
}