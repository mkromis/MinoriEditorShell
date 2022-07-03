using Avalonia.Controls;
using Avalonia.Controls.Templates;
using MinoriEditorShell.Platforms.Avalonia.ViewModels;
using System;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public class ViewTemplate : IDataTemplate
    {
        public IControl Build(Object param)
        {
            if (param is MesDocumentWrapper mdw)
            {
                return (IControl)mdw.Context;
            }
            return null;
        }

        public Boolean Match(Object data) =>
            data is MesDocumentWrapper;
    }
}