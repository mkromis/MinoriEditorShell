using MinoriEditorShell.Models;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Services
{
    public interface IToolboxService
    {
        IEnumerable<ToolboxItem> GetToolboxItems(Type documentType);
    }
}
