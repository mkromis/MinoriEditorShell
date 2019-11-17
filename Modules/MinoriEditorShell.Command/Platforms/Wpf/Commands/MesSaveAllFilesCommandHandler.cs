using MinoriEditorShell.Commands;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [MesCommandHandler]
    public class MesSaveAllFilesCommandHandler : CommandHandlerBase<MesSaveAllFilesCommandDefinition>
    {
        private readonly IMesManager _shell;

        [ImportingConstructor]
        public MesSaveAllFilesCommandHandler(IMesManager shell) => _shell = shell;

        public override async Task Run(MesCommand command)
        {
            List<Task<Tuple<IMesPersistedDocument, Boolean>>> tasks = new List<Task<Tuple<IMesPersistedDocument, Boolean>>>();

            foreach (IMesPersistedDocument document in _shell.Documents.OfType<IMesPersistedDocument>().Where(x => !x.IsNew))
            {
                await document.Save(document.FilePath);
            }

            // TODO: display "Item(s) saved" in statusbar
        }
    }
}
