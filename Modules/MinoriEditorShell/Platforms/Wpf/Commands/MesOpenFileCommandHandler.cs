using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MinoriEditorShell.Commands;
using MinoriEditorShell.Services;
using Microsoft.Win32;
using System;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandHandler]
    public class MesOpenFileCommandHandler : CommandHandlerBase<MesOpenFileCommandDefinition>
    {
        private readonly IMesManager _shell;
        private readonly IMesEditorProvider[] _editorProviders;

        [ImportingConstructor]
        public MesOpenFileCommandHandler(IMesManager shell, [ImportMany] IMesEditorProvider[] editorProviders)
        {
            _shell = shell;
            _editorProviders = editorProviders;
        }

        public override async Task Run(MesCommand command)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "All Supported Files|" + String.Join(";", _editorProviders
                .SelectMany(x => x.FileTypes).Select(x => "*" + x.FileExtension))
            };

            dialog.Filter += "|" + String.Join("|", _editorProviders
                .SelectMany(x => x.FileTypes)
                .Select(x => x.Name + "|*" + x.FileExtension));

            if (dialog.ShowDialog() == true)
            {
                _shell.OpenDocument(await GetEditor(dialog.FileName));
            }
        }

        internal static Task<IMesDocument> GetEditor(String path)
        {
            throw new NotImplementedException();
#warning OpenFileGetEditor
#if false
            var provider = IoC.GetAllInstances(typeof(IEditorProvider))
                .Cast<IEditorProvider>()
                .FirstOrDefault(p => p.Handles(path));
            if (provider == null)
                return null;

            var editor = provider.Create();

            var viewAware = (IViewAware) editor;
            viewAware.ViewAttached += (sender, e) =>
            {
                var frameworkElement = (FrameworkElement) e.View;

                RoutedEventHandler loadedHandler = null;
                loadedHandler = async (sender2, e2) =>
                {
                    frameworkElement.Loaded -= loadedHandler;
                    await provider.Open(editor, path);
                };
                frameworkElement.Loaded += loadedHandler;
            };

            return Task.FromResult(editor);
#endif
        }
    }
}
