using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Commands;
using MinoriEditorStudio.Framework.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace MinoriEditorStudio.Modules.Shell.Commands
{
    [CommandHandler]
    public class SaveAllFilesCommandHandler : CommandHandlerBase<SaveAllFilesCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public SaveAllFilesCommandHandler(IManager shell)
        {
            _shell = shell;
        }

        public override async Task Run(Command command)
        {
            var tasks = new List<Task<Tuple<IPersistedDocument, bool>>>();

            foreach (var document in _shell.Documents.OfType<IPersistedDocument>().Where(x => !x.IsNew))
            {
                await document.Save(document.FilePath);
            }

            // TODO: display "Item(s) saved" in statusbar
        }
    }
}
