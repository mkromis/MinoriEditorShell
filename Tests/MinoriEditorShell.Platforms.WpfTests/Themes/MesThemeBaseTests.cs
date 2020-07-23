using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Tests;
using System;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Themes.Tests
{
    [TestClass()]
    public class MesThemeBaseTests : MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            String s = System.IO.Packaging.PackUriHelper.UriSchemePack;
        }

        [TestMethod]
        public void NoRibbonTest()
        {
            Setup();

            MesBlueTheme blue = new MesBlueTheme();
            Assert.AreEqual(1, blue.ApplicationResources.Count());
        }
    }
}