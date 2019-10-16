using MinoriEditorShell.Models;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IMesToolboxService
    {
        IEnumerable<MesToolboxItem> GetToolboxItems(Type documentType);
    }
}
