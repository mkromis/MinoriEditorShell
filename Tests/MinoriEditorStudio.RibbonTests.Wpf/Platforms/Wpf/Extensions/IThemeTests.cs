﻿using System;
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
