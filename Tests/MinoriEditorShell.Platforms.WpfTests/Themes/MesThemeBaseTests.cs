using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MvvmCross.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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