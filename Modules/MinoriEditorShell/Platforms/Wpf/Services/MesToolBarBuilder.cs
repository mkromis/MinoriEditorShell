using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.Models;
using MinoriEditorShell.Platforms.Wpf.ToolBars;
using MinoriEditorShell.Services;
using System.ComponentModel.Composition;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    [Export(typeof(IMesToolBarBuilder))]
    public class MesToolBarBuilder : IMesToolBarBuilder
    {
        private readonly IMesCommandService _commandService;
        private readonly MesToolBarDefinition[] _toolBars;
        private readonly MesToolBarItemGroupDefinition[] _toolBarItemGroups;
        private readonly MesToolBarItemDefinition[] _toolBarItems;

        [ImportingConstructor]
        public MesToolBarBuilder(
            IMesCommandService commandService,
            [ImportMany] MesToolBarDefinition[] toolBars,
            [ImportMany] MesToolBarItemGroupDefinition[] toolBarItemGroups,
            [ImportMany] MesToolBarItemDefinition[] toolBarItems,
            [ImportMany] MesExcludeToolBarDefinition[] excludeToolBars,
            [ImportMany] MesExcludeToolBarItemGroupDefinition[] excludeToolBarItemGroups,
            [ImportMany] MesExcludeToolBarItemDefinition[] excludeToolBarItems)
        {
            _commandService = commandService;
            _toolBars = toolBars
                .Where(x => !excludeToolBars.Select(y => y.ToolBarDefinitionToExclude).Contains(x))
                .ToArray();
            _toolBarItemGroups = toolBarItemGroups
                .Where(x => !excludeToolBarItemGroups.Select(y => y.ToolBarItemGroupDefinitionToExclude).Contains(x))
                .ToArray();
            _toolBarItems = toolBarItems
                .Where(x => !excludeToolBarItems.Select(y => y.ToolBarItemDefinitionToExclude).Contains(x))
                .ToArray();
        }

        public void BuildToolBars(IMesToolBars result)
        {
            var toolBars = _toolBars.OrderBy(x => x.SortOrder);

            foreach (var toolBar in toolBars)
            {
                var toolBarModel = new MesToolBarModel();
                BuildToolBar(toolBar, toolBarModel);
                if (toolBarModel.Any())
                    result.Items.Add(toolBarModel);
            }
        }

        public void BuildToolBar(MesToolBarDefinition toolBarDefinition, IMesToolBar result)
        {
            var groups = _toolBarItemGroups
                .Where(x => x.ToolBar == toolBarDefinition)
                .OrderBy(x => x.SortOrder)
                .ToList();

            for (int i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                var toolBarItems = _toolBarItems
                    .Where(x => x.Group == group)
                    .OrderBy(x => x.SortOrder);

                foreach (var toolBarItem in toolBarItems)
                    result.Add(new MesCommandToolBarItem(toolBarItem, _commandService.GetCommand(toolBarItem.CommandDefinition), result));

                if (i < groups.Count - 1 && toolBarItems.Any())
                    result.Add(new MesToolBarItemSeparator());
            }
        }
    }
}
