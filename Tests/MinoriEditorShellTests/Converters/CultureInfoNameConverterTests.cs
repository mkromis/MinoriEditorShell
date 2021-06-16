using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MinoriEditorShell.ConvertersTests
{
    [TestClass]
    public class CultureInfoNameConverterTests
    {
        [TestMethod]
        public void SystemTest()
        {
            CultureInfoNameConverter converter = new();
        }

        [TestMethod]
        public void ConvertBackFailTest()
        {
            CultureInfoNameConverter converter = new();
            Assert.ThrowsException<NotSupportedException>(() => converter.ConvertBack(new Object(), typeof(Object), new Object(), CultureInfo.CurrentCulture));
        }
    }
}
