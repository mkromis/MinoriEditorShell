using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;

namespace MinoriEditorShell.Platforms.Wpf.Themes
{
    /// <summary>
    /// Base theme to initilizer
    /// </summary>
    public abstract class MesThemeBase : IMesTheme
    {
        private readonly List<Uri> _resources;

        /// <summary>
        /// Name of theme
        /// </summary>
        public virtual string Name => null;

        /// <summary>
        /// Add resources to themes
        /// </summary>
        /// <param name="uri"></param>
        public void Add(Uri uri) => _resources.Add(uri);

        /// <summary>
        ///  Add multiple resources Uri
        /// </summary>
        /// <param name="uri"></param>
        public void AddRange(IEnumerable<Uri> uri) => _resources.AddRange(uri);

        /// <summary>
        /// Get resources for application (All Resources)
        /// </summary>
        public IEnumerable<Uri> ApplicationResources => _resources;

        /// <summary>
        /// Setup base load definition
        /// </summary>
        protected MesThemeBase()
        {
            // Initialize the base Mahapps.Metro resources.
            _resources = new List<Uri>();
        }
    }
}