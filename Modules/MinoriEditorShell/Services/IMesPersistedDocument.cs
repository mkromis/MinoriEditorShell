using System.Threading.Tasks;

namespace MinoriEditorShell.Services
{
    public interface IMesPersistedDocument : IMesDocument
    {
        bool IsNew { get; }
        string FileName { get; }
        string FilePath { get; }

        Task New(string fileName);

        Task Load(string filePath);

        Task Save(string filePath);
    }
}