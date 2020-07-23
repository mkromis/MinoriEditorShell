﻿using MinoriEditorShell.Platforms.Wpf.Themes;
using System;

namespace MinoriDemo.RibbonWpfCore.Modules.Themes
{
    internal class CyberTheme : MesThemeBase
    {
        /// <summary>
        /// When doing this in your own project, try to localize if if necessary
        /// </summary>
        public override String Name => "CyberTheme";

        public CyberTheme() : base() => Add(new Uri("pack://application:,,,/MinoriDemo.RibbonWpfCore;component/Modules/Themes/CyberTheme.xaml"));
    }
}