using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Dock.Model.Core;
using MinoriEditorShell.Services;
using MvvmCross;
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

            try
            {
                IMesDocumentManager manager = Mvx.IoCProvider.Resolve<IMesDocumentManager>();
                manager.UpdateFloatingWindows += Manager_UpdateFloatingWindows;
                DataContext = manager;
            }
            catch { }
        }

        public void LoadLayout(Stream stream, Action<IMesTool> addToolCallback, Action<IMesDocument> addDocumentCallback, Dictionary<String, IMesLayoutItem> itemsState)
        {
            IDockControl Manager = this.Get<IDockControl>("Manager");
            MesLayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);
        }

        // private void OnManagerLayoutUpdated(Object sender, EventArgs e) => UpdateFloatingWindows();

        public void SaveLayout(Stream stream) => throw new NotImplementedException();

        private void Manager_UpdateFloatingWindows(Object sender, EventArgs e)
        {
            //if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            //{
            //    WindowIcon mainWindowIcon = desktop.MainWindow.Icon;
            //    Boolean showFloatingWindowsInTaskbar = ((MesDocumentManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
            //    foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
            //    {
            //        window.Icon = mainWindowIcon;
            //        window.ShowInTaskbar = showFloatingWindowsInTaskbar;
            //    }
            //}
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}