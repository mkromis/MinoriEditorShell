using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    public partial class MesManagerView : IMesManagerView
	{
        public MesManagerView()
        {
            InitializeComponent();

            try
            {
                IMesManager manager = Mvx.IoCProvider.Resolve<IMesManager>();
                manager.ManagerView = this;
                DataContext = manager;
            } catch { }
        }

        public void LoadLayout(
            Stream stream,
            Action<IMesTool> addToolCallback,
            Action<IMesDocument> addDocumentCallback,
            Dictionary<String, IMesLayoutItem> itemsState) =>
            MesLayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);

        public void SaveLayout(Stream stream) => MesLayoutUtility.SaveLayout(Manager, stream);

        private void OnManagerLayoutUpdated(Object sender, EventArgs e) => UpdateFloatingWindows();

        public void UpdateFloatingWindows()
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                ImageSource mainWindowIcon = mainWindow.Icon;
                Boolean showFloatingWindowsInTaskbar = ((MesManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
                foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
                {
                    window.Icon = mainWindowIcon;
                    window.ShowInTaskbar = showFloatingWindowsInTaskbar;
                }
            }
        }
    }
}
