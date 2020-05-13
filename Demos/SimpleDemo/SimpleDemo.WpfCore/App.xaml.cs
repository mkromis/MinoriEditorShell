using MinoriEditorShell.Platforms.Wpf;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleDemo.WpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup() => this.RegisterSetupType<MesWpfSetup<Core.App>>();
    }
}
