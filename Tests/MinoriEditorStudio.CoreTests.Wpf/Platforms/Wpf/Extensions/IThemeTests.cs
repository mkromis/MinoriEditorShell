using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Platforms.Wpf.Themes;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.CoreTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            String s = System.IO.Packaging.PackUriHelper.UriSchemePack;
        }

        [TestMethod]
        public void NoRibbonTest()
        {
            Setup();

            BlueTheme blue = new BlueTheme();
            Assert.IsFalse(blue.HasRibbon());
            Assert.AreEqual(6, blue.ApplicationResources.Count());
        }
    }
}
