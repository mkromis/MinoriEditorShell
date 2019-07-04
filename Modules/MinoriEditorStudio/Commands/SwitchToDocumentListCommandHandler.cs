using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Threading;

namespace MinoriEditorStudio.Commands
{
    [CommandHandler]
    public class SwitchToDocumentListCommandHandler : ICommandListHandler<SwitchToDocumentCommandListDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public SwitchToDocumentListCommandHandler(IManager shell)
        {
            _shell = shell;
        }

        public void Populate(Command command, List<Command> commands)
        {
            for (Int32 i = 0; i < _shell.Documents.Count; i++)
            {
                IDocument document = _shell.Documents[i];
                commands.Add(new Command(command.CommandDefinition)
                {
                    Checked = _shell.ActiveItem == document,
                    Text = String.Format("_{0} {1}", i + 1, document.DisplayName),
                    Tag = document
                });
            }
        }

        public Task Run(Command command)
        {
            _shell.OpenDocument((IDocument) command.Tag);
            return TaskUtility.Completed;
        }
    }
}
