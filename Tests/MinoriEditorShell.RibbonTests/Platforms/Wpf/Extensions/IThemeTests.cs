using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Services;
using MinoriEditorShell.Services;
using MvvmCross.Binding.Extensions;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoriEditorShell.RibbonTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            String _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
            new MvvmCross.Plugin.Messenger.Plugin().Load(Ioc);

            // register necessary interfaces

            Ioc.ConstructAndRegisterSingleton<IMesThemeManager, MesThemeManager>();

            // Register themes
            new Ribbon.Platforms.Wpf.Plugin().Load(Ioc);
        }

        [TestMethod]
        public void RibbonTest()
        {
            Setup();

            IMesThemeManager themeManager = Ioc.Resolve<IMesThemeManager>();

            IEnumerable<IMesTheme> themes = themeManager.Themes;
            Assert.AreEqual(3, themes.Count());

            IMesTheme blue = themes.First(x => x.Name.Contains("Blue"));
            Assert.AreEqual(2, blue.ApplicationResources.Count());
        }
    }
}