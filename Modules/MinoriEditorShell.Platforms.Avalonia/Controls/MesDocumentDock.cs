using Dock.Model.Controls;
using MinoriEditorShell.Platforms.Avalonia.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Avalonia.Controls
{
    internal class MesDocumentDock : MesDockBase, IDocumentDock
    {
        private Boolean _canCreateDocument;

        /// <summary>
        /// Determine if we can crate command
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean CanCreateDocument
        {
            get => _canCreateDocument;
            set => SetProperty(ref _canCreateDocument, value);
        }

        /// <summary>
        /// Command to create document
        /// </summary>
        [IgnoreDataMember]
        public ICommand CreateDocument { get; set; }
    }
}