using MinoriEditorStudio.Models;
using System;
using System.Collections.Generic;

namespace MinoriEditorStudio.Services
{
    public interface IToolboxService
    {
        IEnumerable<ToolboxItem> GetToolboxItems(Type documentType);
    }
}
