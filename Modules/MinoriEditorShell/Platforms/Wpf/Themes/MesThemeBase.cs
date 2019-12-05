using MinoriEditorShell.Platforms.Wpf.Extensions;
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
        readonly List<Uri> _resources;

        public virtual String Name => null;

        public void Add(Uri uri) => _resources.Add(uri);
        public void AddRange(IEnumerable<Uri> uri) => _resources.AddRange(uri);

        public IEnumerable<Uri> ApplicationResources => _resources;

        public MesThemeBase()
        {
            // Initialize the base Mahapps.Metro resources.
            _resources = new List<Uri> {
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"),
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"),
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Blue.xaml"),

                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/VS/Colors.xaml"),
            };
        }
    }
}
