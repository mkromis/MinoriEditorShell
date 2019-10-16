using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Extensions;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.RibbonTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            String _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
        }

        [TestMethod, Ignore]
        public void RibbonTest()
        {
            Setup(); 

            MesBlueTheme blue = new MesBlueTheme();
            //Assert.IsTrue(blue.HasRibbon());
            Assert.AreEqual(10, blue.ApplicationResources.Count());
        }
    }
}
