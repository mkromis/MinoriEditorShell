using Dock.Model.Controls;
using MinoriEditorShell.Platforms.Avalonia.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Controls
{
    internal class MesDockDock : MesDockBase, IDockDock
    {
        private Boolean _lastChildFill = true;

        /// <summary>
        /// if last child
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean LastChildFill
        {
            get => _lastChildFill;
            set => SetProperty(ref _lastChildFill, value);
        }
    }
}