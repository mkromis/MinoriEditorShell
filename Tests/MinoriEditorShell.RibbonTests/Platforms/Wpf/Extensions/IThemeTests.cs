using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Services;
using MvvmCross.Binding.Extensions;
using MvvmCross.IoC;
using System;
using System.Linq;

namespace MinoriEditorShell.RibbonTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            string _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
            new MvvmCross.Plugin.Messenger.Plugin().Load();

            // register necessary interfaces

            Ioc.ConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();

            // Register themes
            new Ribbon.Platforms.Wpf.Plugin().Load();
        }

        [TestMethod]
        public void RibbonTest()
        {
            Setup();

            IMesThemeManager themeManager = Ioc.Resolve<IMesThemeManager>();

            var themes = themeManager.Themes;
            Assert.AreEqual(3, themes.Count());

            var blue = themes.First(x => x.Name.Contains("Blue"));
            Assert.AreEqual(2, blue.ApplicationResources.Count());
        }
    }
}