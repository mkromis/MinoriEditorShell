using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using MinoriEditorShell.Models;

namespace MinoriEditorShell.Services
{
    [Export(typeof(IMesToolboxService))]
    public class MesToolboxService : IMesToolboxService
    {
        private readonly Dictionary<Type, IEnumerable<MesToolboxItem>> _toolboxItems;

#warning ToolboxService
        public MesToolboxService()
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

        public IEnumerable<Models.MesToolboxItem> GetToolboxItems(Type documentType) => 
            _toolboxItems.TryGetValue(documentType, out IEnumerable<MesToolboxItem> result) ? result : new List<MesToolboxItem>();
        IEnumerable<Models.MesToolboxItem> IMesToolboxService.GetToolboxItems(Type documentType) => throw new NotImplementedException();
    }
}
