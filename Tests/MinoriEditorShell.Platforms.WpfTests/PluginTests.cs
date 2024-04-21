using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Tests
{
    [TestClass()]
    public class PluginTests : MvxIoCSupportingTest
    {
        [TestInitialize]
        public void Init() => Setup();

        [TestMethod()]
        public void LoadTest()
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                Plugin plugin = new();
                plugin.Load(Ioc);

                //Assert.IsNotNull(Ioc.Resolve<IMesAutoScroll>());
                //Assert.IsNotNull(Ioc.Resolve<IMesContentCanvas>());
                //Assert.IsNotNull(Ioc.Resolve<IMesMapZoom>());
                //Assert.IsNotNull(Ioc.Resolve<IMesPan>());
                //Assert.IsNotNull(Ioc.Resolve<IMesRectangleSelectionGesture>());
                //Assert.IsNotNull(Ioc.Resolve<IMesVirtualCanvasControl>());
            }));
        }
    }
}