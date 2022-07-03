using Dock.Model.Controls;
using Dock.Model.Core;
using MinoriEditorShell.Platforms.Avalonia.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Controls
{
    public class MesToolDock : MesDockBase, IToolDock
    {
        private Alignment _alignment = Alignment.Unset;
        private Boolean _isExpanded;
        private Boolean _autoHide = true;
        private GripMode _gripMode = GripMode.Visible;

        /// <summary>
        /// Current Alignment
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Alignment Alignment
        {
            get => _alignment;
            set => SetProperty(ref _alignment, value);
        }

        /// <summary>
        /// Is expandable?
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        /// <summary>
        /// Is Autohide is set?
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean AutoHide
        {
            get => _autoHide;
            set => SetProperty(ref _autoHide, value);
        }

        /// <summary>
        /// Type of grip
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public GripMode GripMode
        {
            get => _gripMode;
            set => SetProperty(ref _gripMode, value);
        }
    }
}