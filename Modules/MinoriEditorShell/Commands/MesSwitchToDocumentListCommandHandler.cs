using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorShell.Services;
using MinoriEditorShell.Threading;

namespace MinoriEditorShell.Commands
{
    [MesCommandHandler]
    public class MesSwitchToDocumentListCommandHandler : ICommandListHandler<MesSwitchToDocumentCommandListDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesSwitchToDocumentListCommandHandler(IMesManager shell)
        {
            _shell = shell;
        }

        public void Populate(MesCommand command, List<MesCommand> commands)
        {
            for (Int32 i = 0; i < _shell.Documents.Count; i++)
            {
                IMesDocument document = _shell.Documents[i];
                commands.Add(new MesCommand(command.CommandDefinition)
                {
                    Checked = _shell.ActiveItem == document,
                    Text = String.Format("_{0} {1}", i + 1, document.DisplayName),
                    Tag = document
                });
            }
        }

        public Task Run(MesCommand command)
        {
            _shell.OpenDocument((IMesDocument) command.Tag);
            return MesTaskUtility.Completed;
        }
    }
}
