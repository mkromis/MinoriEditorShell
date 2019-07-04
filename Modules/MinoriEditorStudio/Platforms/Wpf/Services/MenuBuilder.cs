using MinoriEditorStudio.Models;
using MinoriEditorStudio.Platforms.Wpf.MenuDefinitions;
using MinoriEditorStudio.Platforms.Wpf.Menus;
using MinoriEditorStudio.Platforms.Wpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MinoriEditorStudio.Platforms.Wpf.Services
{
    [Export(typeof(IMenuBuilder))]
    public class MenuBuilder : IMenuBuilder
    {
        private readonly ICommandService _commandService;
        private readonly MenuDefinition[] _menus;
        private readonly MenuItemGroupDefinition[] _menuItemGroups;
        private readonly MenuItemDefinition[] _menuItems;
        private readonly MenuDefinition[] _excludeMenus;
        private readonly MenuItemGroupDefinition[] _excludeMenuItemGroups;
        private readonly MenuItemDefinition[] _excludeMenuItems;

        [ImportingConstructor]
        public MenuBuilder(
            ICommandService commandService,
            [ImportMany] MenuBarDefinition[] menuBars,
            [ImportMany] MenuDefinition[] menus,
            [ImportMany] MenuItemGroupDefinition[] menuItemGroups,
            [ImportMany] MenuItemDefinition[] menuItems,
            [ImportMany] ExcludeMenuDefinition[] excludeMenus,
            [ImportMany] ExcludeMenuItemGroupDefinition[] excludeMenuItemGroups,
            [ImportMany] ExcludeMenuItemDefinition[] excludeMenuItems)
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

        public MenuBarDefinition[] MenuBars { get; }

        public void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result)
        {
            IOrderedEnumerable<MenuDefinition> menus = _menus
                .Where(x => x.MenuBar == menuBarDefinition)
                .Where(x => !_excludeMenus.Contains(x))
                .OrderBy(x => x.SortOrder);

            foreach (MenuDefinition menu in menus)
            {
                TextMenuItem menuModel = new TextMenuItem(menu);
                AddGroupsRecursive(menu, menuModel);
                if (menuModel.Children.Any())
                {
                    result.Add(menuModel);
                }
            }
        }

        private void AddGroupsRecursive(MenuDefinitionBase menu, StandardMenuItem menuModel)
        {
            List<MenuItemGroupDefinition> groups = _menuItemGroups
                .Where(x => x.Parent == menu)
                .Where(x => !_excludeMenuItemGroups.Contains(x))
                .OrderBy(x => x.SortOrder)
                .ToList();

            for (Int32 i = 0; i < groups.Count; i++)
            {
                MenuItemGroupDefinition group = groups[i];
                IOrderedEnumerable<MenuItemDefinition> menuItems = _menuItems
                    .Where(x => x.Group == group)
                    .Where(x => !_excludeMenuItems.Contains(x))
                    .OrderBy(x => x.SortOrder);

                foreach (MenuItemDefinition menuItem in menuItems)
                {
                    StandardMenuItem menuItemModel = (menuItem.CommandDefinition != null)
                        ? new CommandMenuItem(_commandService.GetCommand(menuItem.CommandDefinition), menuModel)
                        : (StandardMenuItem)new TextMenuItem(menuItem);
                    AddGroupsRecursive(menuItem, menuItemModel);
                    menuModel.Add(menuItemModel);
                }

                if (i < groups.Count - 1 && menuItems.Any())
                {
                    menuModel.Add(new MenuItemSeparator());
                }
            }
        }
    }
}
