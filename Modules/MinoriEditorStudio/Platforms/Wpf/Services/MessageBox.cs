using MinoriEditorStudio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    class MessageBox : IMessageBox
    {
        public void Alert(String text, String caption)
        {
            System.Windows.MessageBox.Show(text, caption);
        }
    }
}
