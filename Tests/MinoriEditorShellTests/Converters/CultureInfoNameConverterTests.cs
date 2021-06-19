using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Converters;
using MvvmCross.Tests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MinoriEditorShell.ConvertersTests
{
    [TestClass]
    public class CultureInfoNameConverterTests : MvxIoCSupportingTest
    {
        [TestMethod, Ignore("Need Finished")]
        public void ConvertBackFailTest()
        {
            Setup();

            CultureInfoNameConverter converter = new();
            Assert.ThrowsException<NotSupportedException>(() => converter.ConvertBack(new Object(), typeof(Object), new Object(), CultureInfo.CurrentCulture));
        }

        [TestMethod, Ignore("Need Finished")]
        public void SystemTest()
        {
            Setup();

            CultureInfoNameConverter converter = new();
        }
    }
}