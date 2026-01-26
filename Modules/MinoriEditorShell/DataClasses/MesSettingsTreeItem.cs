using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.DataClasses

{
    /// <summary>
    /// Tree model to hold settings
    /// </summary>
    public class MesSettingsTreeItem
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        public MesSettingsTreeItem()
        {
            Children = [];
            Editors = [];
        }

        /// <summary>
        /// Name of setting
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Holds the editor viewmodel
        /// </summary>
        public List<IMesSettings> Editors { get; private set; }

        /// <summary>
        /// Children contains more views
        /// </summary>
        public List<MesSettingsTreeItem> Children { get; private set; }
    }
}