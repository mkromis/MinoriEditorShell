using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public partial class MesDocumentManagerView : UserControl, IMesDocumentManagerView
    {
        public MesDocumentManagerView()
        {
            InitializeComponent();

            //     try
            //     {
            //         IMesDocumentManager manager = Mvx.IoCProvider.Resolve<IMesDocumentManager>();
            //         manager.ManagerView = this;
            //         DataContext = manager;
            //     }
            //     catch { }
        }

        public void LoadLayout(Stream stream, Action<IMesTool> addToolCallback, Action<IMesDocument> addDocumentCallback, Dictionary<String, IMesLayoutItem> itemsState)
        {
            //MesLayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);
        }

        // private void OnManagerLayoutUpdated(Object sender, EventArgs e) => UpdateFloatingWindows();

        public void SaveLayout(Stream stream) => throw new NotImplementedException();

        public void UpdateFloatingWindows()
        {
            // Window mainWindow = Window.GetWindow(this);
            // if (mainWindow != null)
            // {
            //     ImageSource mainWindowIcon = mainWindow.Icon;
            //     Boolean showFloatingWindowsInTaskbar = ((MesDocumentManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
            //     foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
            //     {
            //         window.Icon = mainWindowIcon;
            //         window.ShowInTaskbar = showFloatingWindowsInTaskbar;
            //     }
            // }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}