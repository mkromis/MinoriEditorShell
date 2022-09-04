using MinoriEditorShell.Services;
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
            Children = new List<MesSettingsTreeItem>();
            Editors = new List<IMesSettings>();
        }
        /// <summary>
        /// Name of setting
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Holds the editor viewmodel
        /// </summary>
        public IList<IMesSettings> Editors { get; }
        /// <summary>
        /// Children contains more views
        /// </summary>
        public IList<MesSettingsTreeItem> Children { get; }
    }
}