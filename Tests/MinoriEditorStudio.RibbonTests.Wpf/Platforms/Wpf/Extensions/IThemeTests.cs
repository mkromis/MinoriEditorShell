using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Platforms.Wpf.Themes;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.RibbonTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            String s = System.IO.Packaging.PackUriHelper.UriSchemePack;
        }

        [TestMethod]
        public void RibbonTest()
        {
            Setup(); 

            BlueTheme blue = new BlueTheme();
            Assert.IsTrue(blue.HasRibbon());
            Assert.AreEqual(10, blue.ApplicationResources.Count());
        }
    }
}
