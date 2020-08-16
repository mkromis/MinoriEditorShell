using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Extensions;
using MinoriEditorShell.Modules.Services;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Platforms.Wpf.ViewModels;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Themes.Tests
{
    [TestClass()]
    public class IThemeTests : MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            Ioc.RegisterType<IMesDocumentManager, MesDocumentManagerViewModel>();
            Ioc.RegisterType<IMesStatusBar, MesStatusBarViewModel>();
            Ioc.RegisterType<IMesLayoutItemStatePersister, MesLayoutItemStatePersister>();
            Ioc.RegisterType<IMesSettingsManager, MesSettingsManagerViewModel>();
            Ioc.RegisterType<IMesThemeManager, MesThemeManager>();

            String _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
        }

        [TestMethod]
        public void NoRibbonTest()
        {
            Setup();

            MesBlueTheme blue = new MesBlueTheme();
            Assert.AreEqual(1, blue.ApplicationResources.Count());
        }

        [TestMethod]
        public void GetAllThemesTest()
        {
            Setup();

            //IMesThemeManager themeManager = Ioc.Resolve<IMesThemeManager>();

            IEnumerable<IMesTheme> results = Ioc.GetAll<IMesTheme>();
            //Assert.AreEqual(3, results.Count());
            Assert.Inconclusive();
        }
    }
}