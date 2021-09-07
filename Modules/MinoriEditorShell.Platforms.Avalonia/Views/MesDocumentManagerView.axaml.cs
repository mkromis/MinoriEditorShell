using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Dock.Model.Core;
using MinoriEditorShell.Platforms.Avalonia.ViewModels;
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
            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);

            IMesDocumentManager manager = Mvx.IoCProvider.Resolve<IMesDocumentManager>();
            //manager.UpdateFloatingWindows += Manager_UpdateFloatingWindows;
            DataContext = (MesDocumentManagerViewModel)manager;
        }

        private void DragOver(Object sender, DragEventArgs e)
        {
            if (DataContext is IDropTarget dropTarget)
            {
                dropTarget.DragOver(sender, e);
            }
        }

        private void Drop(Object sender, DragEventArgs e)
        {
            if (DataContext is IDropTarget dropTarget)
            {
                dropTarget.Drop(sender, e);
            }
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