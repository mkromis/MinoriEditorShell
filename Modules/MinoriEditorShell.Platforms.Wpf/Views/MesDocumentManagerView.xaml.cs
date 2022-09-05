using AvalonDock.Controls;
using MinoriEditorShell.Services;
using MinoriEditorShell.ViewModels;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <inheritdoc cref="IMesDocumentManagerView"/>
    public partial class MesDocumentManagerView : IMesDocumentManagerView
    {
        /// <summary>
        /// Main Document view
        /// </summary>
        public MesDocumentManagerView()
        {
            InitializeComponent();

            IMesDocumentManager manager = Mvx.IoCProvider.Resolve<IMesDocumentManager>();
            manager.ManagerView = this;
            DataContext = manager;
        }
        /// <inheritdoc />
        public void LoadLayout(
            Stream stream,
            Action<IMesTool> addToolCallback,
            Action<IMesDocument> addDocumentCallback,
            Dictionary<string, IMesLayoutItem> itemsState) =>
            MesLayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);
        /// <inheritdoc />
        public void SaveLayout(Stream stream) => MesLayoutUtility.SaveLayout(Manager, stream);

        private void OnManagerLayoutUpdated(object sender, EventArgs e) => UpdateFloatingWindows();

        /// <inheritdoc />
        public void UpdateFloatingWindows()
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow == null)
            {
                return;
            }

            ImageSource mainWindowIcon = mainWindow.Icon;
            bool showFloatingWindowsInTaskbar = ((MesDocumentManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
            foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
            {
                window.Icon = mainWindowIcon;
                window.ShowInTaskbar = showFloatingWindowsInTaskbar;
            }
        }
    }
}