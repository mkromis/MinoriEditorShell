 using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinoriEditorShell.Services
{
    public interface IMesEditorProvider
	{
        IEnumerable<MesEditorFileType> FileTypes { get; }

		bool Handles(string path);

        IMesDocument Create();

        Task New(IMesDocument document, string name);
        Task Open(IMesDocument document, string path);
	}
}
