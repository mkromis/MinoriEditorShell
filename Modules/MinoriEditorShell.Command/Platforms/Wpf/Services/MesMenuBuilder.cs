using MinoriEditorShell.Models;
using MinoriEditorShell.Platforms.Wpf.MenuDefinitionCollection;
using MinoriEditorShell.Platforms.Wpf.Menus;
using MinoriEditorShell.Platforms.Wpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    [Export(typeof(IMesMenuBuilder))]
    public class MesMenuBuilder : IMesMenuBuilder
    {
        private readonly IMesCommandService _commandService;
        private readonly MesMenuDefinition[] _menus;
        private readonly MesMenuItemGroupDefinition[] _menuItemGroups;
        private readonly MesMenuItemDefinition[] _menuItems;
        private readonly MesMenuDefinition[] _excludeMenus;
        private readonly MesMenuItemGroupDefinition[] _excludeMenuItemGroups;
        private readonly MesMenuItemDefinition[] _excludeMenuItems;

        [ImportingConstructor]
        public MesMenuBuilder(
            IMesCommandService commandService,
            [ImportMany] MesMenuBarDefinition[] menuBars,
            [ImportMany] MesMenuDefinition[] menus,
            [ImportMany] MesMenuItemGroupDefinition[] menuItemGroups,
            [ImportMany] MesMenuItemDefinition[] menuItems,
            [ImportMany] MesExcludeMenuDefinition[] excludeMenus,
            [ImportMany] MesExcludeMenuItemGroupDefinition[] excludeMenuItemGroups,
            [ImportMany] MesExcludeMenuItemDefinition[] excludeMenuItems)
        {
            _commandService = commandService;
            MenuBars = menuBars;
            _menus = menus;
            _menuItemGroups = menuItemGroups;
            _menuItems = menuItems;
            _excludeMenus = excludeMenus.Select(x => x.MenuDefinitionToExclude).ToArray();
            _excludeMenuItemGroups = excludeMenuItemGroups.Select(x => x.MenuItemGroupDefinitionToExclude).ToArray();
            _excludeMenuItems = excludeMenuItems.Select(x => x.MenuItemDefinitionToExclude).ToArray();
        }

        public MesMenuBarDefinition[] MenuBars { get; }

        public void BuildMenuBar(MesMenuBarDefinition menuBarDefinition, MesMenuModel result)
        {
            IOrderedEnumerable<MesMenuDefinition> menus = _menus
                .Where(x => x.MenuBar == menuBarDefinition)
                .Where(x => !_excludeMenus.Contains(x))
                .OrderBy(x => x.SortOrder);

            foreach (MesMenuDefinition menu in menus)
            {
                MesTextMenuItem menuModel = new MesTextMenuItem(menu);
                AddGroupsRecursive(menu, menuModel);
                if (menuModel.Children.Any())
                {
                    result.Add(menuModel);
                }
            }
        }

        private void AddGroupsRecursive(MesMenuDefinitionBase menu, MesStandardMenuItem menuModel)
        {
            List<MesMenuItemGroupDefinition> groups = _menuItemGroups
                .Where(x => x.Parent == menu)
                .Where(x => !_excludeMenuItemGroups.Contains(x))
                .OrderBy(x => x.SortOrder)
                .ToList();

            for (Int32 i = 0; i < groups.Count; i++)
            {
                MesMenuItemGroupDefinition group = groups[i];
                IOrderedEnumerable<MesMenuItemDefinition> menuItems = _menuItems
                    .Where(x => x.Group == group)
                    .Where(x => !_excludeMenuItems.Contains(x))
                    .OrderBy(x => x.SortOrder);

                foreach (MesMenuItemDefinition menuItem in menuItems)
                {
                    MesStandardMenuItem menuItemModel = (menuItem.CommandDefinition != null)
                        ? new MesCommandMenuItem(_commandService.GetCommand(menuItem.CommandDefinition), menuModel)
                        : (MesStandardMenuItem)new MesTextMenuItem(menuItem);
                    AddGroupsRecursive(menuItem, menuItemModel);
                    menuModel.Add(menuItemModel);
                }

                if (i < groups.Count - 1 && menuItems.Any())
                {
                    menuModel.Add(new MesMenuItemSeparator());
                }
            }
        }
    }
}
