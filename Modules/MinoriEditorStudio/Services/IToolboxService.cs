using System;
using System.Collections.Generic;
using MinoriEditorStudio.Modules.Toolbox.Models;

namespace MinoriEditorStudio.Services
{
    public interface IToolboxService
    {
        IEnumerable<ToolboxItem> GetToolboxItems(Type documentType);
    }
}
