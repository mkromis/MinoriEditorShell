using System;

namespace MinoriEditorShell.Services
{
    public interface IMesLayoutItemStatePersister
    {
        bool SaveState(IMesDocumentManager shell, IMesDocumentManagerView shellView, string fileName);

        bool LoadState(IMesDocumentManager shell, IMesDocumentManagerView shellView, string fileName);
    }
}