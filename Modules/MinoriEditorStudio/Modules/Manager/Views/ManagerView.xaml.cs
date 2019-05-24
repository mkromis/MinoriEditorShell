using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Attributes;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Manager.ViewModels;
using MinoriEditorStudio.Modules.Shell.Views;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;

namespace MinoriEditorStudio.Modules.Manager.Views
{
    public partial class ManagerView : IManagerView
	{
        public ManagerView()
        {
            InitializeComponent();
            IManager manager = Mvx.IoCProvider.Resolve<IManager>();
            manager.ManagerView = this;
            DataContext = manager;
        }

        public void LoadLayout(
            Stream stream,
            Action<ITool> addToolCallback,
            Action<IDocument> addDocumentCallback,
            Dictionary<String, ILayoutItem> itemsState) =>
            LayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);

        public void SaveLayout(Stream stream) => LayoutUtility.SaveLayout(Manager, stream);

        private void OnManagerLayoutUpdated(Object sender, EventArgs e)
        {
            UpdateFloatingWindows();
        }

        public void UpdateFloatingWindows()
        {
            //Window mainWindow = Window.GetWindow(this);
            //ImageSource mainWindowIcon = mainWindow?.Icon;
            //Boolean showFloatingWindowsInTaskbar = ((ManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
            //foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
            //{
            //    window.Icon = mainWindowIcon;
            //    window.ShowInTaskbar = showFloatingWindowsInTaskbar;
            //}
        }
    }
}
