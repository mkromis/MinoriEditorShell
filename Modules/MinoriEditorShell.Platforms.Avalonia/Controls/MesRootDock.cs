using Dock.Model.Controls;
using Dock.Model.Core;
using MinoriEditorShell.Platforms.Avalonia.Core;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Avalonia.Controls
{
    /// <summary>
    /// Main root dock
    /// </summary>
    internal class MesRootDock : MesDockBase, IRootDock
    {
        private Boolean _isFocusableRoot = true;
        private IDockWindow _window;
        private IList<IDockWindow> _windows;

        /// <summary>
        /// is root focusable
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean IsFocusableRoot
        {
            get => _isFocusableRoot;
            set => SetProperty(ref _isFocusableRoot, value);
        }

        /// <summary>
        /// Dockable Window
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IDockWindow Window
        {
            get => _window;
            set => SetProperty(ref _window, value);
        }

        /// <summary>
        /// List of windows
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IList<IDockWindow> Windows
        {
            get => _windows;
            set => SetProperty(ref _windows, value);
        }

        /// <summary>
        /// Initializes new instance of the <see cref="MesRootDock"/> class.
        /// </summary>
        public MesRootDock()
        {
            ShowWindows = new MvxCommand(() => _navigateAdapter.ShowWindows());
            ExitWindows = new MvxCommand(() => _navigateAdapter.ExitWindows());
        }

        /// <summary>
        /// Show window command
        /// </summary>
        [IgnoreDataMember]
        public ICommand ShowWindows { get; }

        /// <summary>
        /// Exit window command
        /// </summary>
        [IgnoreDataMember]
        public ICommand ExitWindows { get; }
    }
}