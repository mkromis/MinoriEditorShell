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
        private IMesDocument _docViewModel;

        public MesDocumentWrapper(IMesDocument docViewModel)
        {
            _docViewModel = docViewModel;
            Context = _docViewModel.View;
        }
    }
}