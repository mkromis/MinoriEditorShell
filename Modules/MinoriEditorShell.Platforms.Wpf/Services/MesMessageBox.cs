using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    internal class MesMessageBox : IMesMessageBox
    {
        public void Alert(string text, string caption)
        {
            System.Windows.MessageBox.Show(text, caption);
        }
    }
}