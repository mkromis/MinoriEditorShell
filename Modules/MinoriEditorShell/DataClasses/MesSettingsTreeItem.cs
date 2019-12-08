using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.DataClasses

{
    public class MesSettingsTreeItem
    {
        public MesSettingsTreeItem()
        {
            Children = new List<MesSettingsTreeItem>();
            Editors = new List<IMesSettings>();
        }

        public String Name { get; set; }
        public List<IMesSettings> Editors { get; private set; }
        public List<MesSettingsTreeItem> Children { get; private set; }
    }
}
