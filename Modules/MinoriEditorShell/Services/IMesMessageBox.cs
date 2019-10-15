using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriEditorShell.Services
{
    public interface IMesMessageBox
    {
        void Alert(String text, String caption);
    }
}
