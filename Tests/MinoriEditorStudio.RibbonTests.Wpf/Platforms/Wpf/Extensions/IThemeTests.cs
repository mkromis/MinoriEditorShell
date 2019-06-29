using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorStudio.Platforms.Wpf.Extensions;
using MinoriEditorStudio.Platforms.Wpf.Themes;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.RibbonTests.Wpf.Platforms.Wpf.Extensions
{
    [TestClass]
    public class IThemeTests    {
        [TestMethod]
        public void NoRibbonTest()
        {
            BlueTheme blue = new BlueTheme();
            Assert.IsTrue(blue.HasRibbon());
        }
    }
}
