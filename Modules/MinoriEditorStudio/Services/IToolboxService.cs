using System;
using System.Collections.Generic;
using MinoriEditorStudio.Modules.Toolbox.Models;

namespace MinoriEditorStudio.Modules.Toolbox.Services
{
    public interface IToolboxService
    {
        IEnumerable<ToolboxItem> GetToolboxItems(Type documentType);
    }
}
