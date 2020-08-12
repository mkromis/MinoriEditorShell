using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Themes;
using System;
using System.Linq;

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
            Assert.AreEqual(2, blue.ApplicationResources.Count());
        }
    }
}