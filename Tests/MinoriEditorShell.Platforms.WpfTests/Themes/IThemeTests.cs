using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Services;
using MvvmCross.IoC;
using MvvmCross.Tests;
using System;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Themes.Tests
{
    [TestClass()]
    public class IThemeTests : MvxIoCSupportingTest
    {
        [TestMethod]
        public void GetAllThemesTest()
        {
            Setup();

            IMesThemeManager themeManager = Ioc.Resolve<IMesThemeManager>();

            var themes = themeManager.Themes;
            Assert.AreEqual(3, themes.Count());

            var blue = themes.First(x => x.Name.Contains("Blue"));
            Assert.AreEqual(1, blue.ApplicationResources.Count());
        }

        [TestMethod]
        public void NoRibbonTest()
        {
            Setup();

            MesBlueTheme blue = new MesBlueTheme();
            Assert.AreEqual(1, blue.ApplicationResources.Count());
        }

        protected override void AdditionalSetup()
        {
            string _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
            new MvvmCross.Plugin.Messenger.Plugin().Load();

            // register necessary interfaces
            Ioc.ConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();
        }
    }
}