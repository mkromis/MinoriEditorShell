using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    class MesMessageBox : IMesMessageBox
    {
        public void Alert(String text, String caption)
        {
            System.Windows.MessageBox.Show(text, caption);
        }
    }
}
