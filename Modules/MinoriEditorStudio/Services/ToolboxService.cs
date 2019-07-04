using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using MinoriEditorStudio.Models;

namespace MinoriEditorStudio.Services
{
    [Export(typeof(IToolboxService))]
    public class ToolboxService : IToolboxService
    {
        private readonly Dictionary<Type, IEnumerable<ToolboxItem>> _toolboxItems;

#warning ToolboxService
        public ToolboxService()
        {
#if false
            _toolboxItems = AssemblySource.Instance
                .SelectMany(x => x.GetTypes().Where(y => y.GetAttributes<ToolboxItemAttribute>(false).Any()))
                .Select(x =>
                {
                    var attribute = x.GetAttributes<ToolboxItemAttribute>(false).First();
                    return new ToolboxItem
                    {
                        DocumentType = attribute.DocumentType,
                        Name = attribute.Name,
                        Category = attribute.Category,
                        IconSource = (attribute.IconSource != null) ? new Uri(attribute.IconSource) : null,
                        ItemType = x,
                    };
                })
                .GroupBy(x => x.DocumentType)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());
#endif
        }

        public IEnumerable<Models.ToolboxItem> GetToolboxItems(Type documentType) => 
            _toolboxItems.TryGetValue(documentType, out IEnumerable<ToolboxItem> result) ? result : new List<ToolboxItem>();
        IEnumerable<Models.ToolboxItem> IToolboxService.GetToolboxItems(Type documentType) => throw new NotImplementedException();
    }
}
