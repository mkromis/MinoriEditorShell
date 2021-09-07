using Dock.Model.Controls;
using MinoriEditorShell.Platforms.Avalonia.Core;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.ViewModels
{
    internal class MesDocumentWrapper : MesDockableBase, IDocument
    {
        private IMesDocument docViewModel;

        public MesDocumentWrapper(IMesDocument docViewModel) => this.docViewModel = docViewModel;

        public IMesDocument ViewModel { get; set; }
    }
}