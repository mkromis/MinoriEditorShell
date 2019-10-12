using MinoriEditorShell.Commands;
using MinoriEditorShell.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [CommandHandler]
    public class SaveAllFilesCommandHandler : CommandHandlerBase<SaveAllFilesCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public SaveAllFilesCommandHandler(IManager shell) => _shell = shell;

        public override async Task Run(Command command)
        {
            List<Task<Tuple<IPersistedDocument, Boolean>>> tasks = new List<Task<Tuple<IPersistedDocument, Boolean>>>();

            foreach (IPersistedDocument document in _shell.Documents.OfType<IPersistedDocument>().Where(x => !x.IsNew))
            {
                await document.Save(document.FilePath);
            }

            // TODO: display "Item(s) saved" in statusbar
        }
    }
}
