using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesNewFileCommandHandler : ICommandListHandler<MesNewFileCommandListDefinition>
    {
        private readonly Int32 _newFileCounter = 1;

        private readonly IMesManager _shell;
        private readonly IMesEditorProvider[] _editorProviders;

        [ImportingConstructor]
        public MesNewFileCommandHandler(
            IMesManager shell,
            [ImportMany] IMesEditorProvider[] editorProviders)
        {
            _shell = shell;
            _editorProviders = editorProviders;
        }

        public void Populate(MesCommand command, List<MesCommand> commands)
        {
            foreach (IMesEditorProvider editorProvider in _editorProviders)
            {
                foreach (MesEditorFileType editorFileType in editorProvider.FileTypes)
                {
                    commands.Add(new MesCommand(command.CommandDefinition)
                    {
                        Text = editorFileType.Name,
                        Tag = new NewFileTag
                        {
                            EditorProvider = editorProvider,
                            FileType = editorFileType
                        }
                    });
                }
            }
        }

#warning Fix NewFileRun
        public Task Run(MesCommand command)
        {
            throw new NotImplementedException();

#if false
            var tag = (NewFileTag) command.Tag;
            var editor = tag.EditorProvider.Create();

            var viewAware = (IViewAware)editor;
            viewAware.ViewAttached += (sender, e) =>
            {
                var frameworkElement = (FrameworkElement)e.View;

                RoutedEventHandler loadedHandler = null;
                loadedHandler = async (sender2, e2) =>
                {
                    frameworkElement.Loaded -= loadedHandler;
                    await tag.EditorProvider.New(editor, string.Format(Resources.FileNewUntitled, (_newFileCounter++) + tag.FileType.FileExtension));
                };
                frameworkElement.Loaded += loadedHandler;
            };

            _shell.OpenDocument(editor);

            return TaskUtility.Completed;
#endif
        }

        private class NewFileTag
        {
            public IMesEditorProvider EditorProvider;
            public MesEditorFileType FileType;
        }
    }
}
