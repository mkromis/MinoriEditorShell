using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Modules.Services;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.Ribbon.Platforms.Wpf;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using Moq;
using MvvmCross.Binding.Extensions;
using MvvmCross.IoC;
using MvvmCross.Plugin.Messenger;
using MvvmCross.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Provider;

namespace MinoriEditorShell.Ribbon.Platforms.Wpf.Tests
{
    [TestClass()]
    public class PluginTests : MvxIoCSupportingTest
    {
        [TestInitialize]
        public void Init()
        {
            Setup();

            // Plugin support   
            Mock<IMvxMessenger> _mockMessenger = new();
            Ioc.RegisterSingleton(() => _mockMessenger.Object);

            // Register base classes
            Ioc.LazyConstructAndRegisterSingleton<IMesDocumentManager, MesDocumentManagerViewModel>();
            Ioc.LazyConstructAndRegisterSingleton<IMesStatusBar, MesStatusBarViewModel>();
            Ioc.LazyConstructAndRegisterSingleton<IMesLayoutItemStatePersister, MesLayoutItemStatePersister>();
            Ioc.RegisterType<IMesSettingsManager, MesSettingsManagerViewModel>();

            // Register themes
            Ioc.LazyConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();
        }

        [TestMethod()]
        public void LoadTest()
        {
            Plugin plugin = new();
            //plugin.Load(Ioc);

            //Assert.IsNotNull(Ioc.Resolve<IMesThemeManager>());
            //Assert.AreEqual(3, Ioc.Resolve<IMesThemeManager>().Themes.Count());
        }
    }
}