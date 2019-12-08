using System;

namespace MinoriEditorShell.Services
{
    public interface IMesLayoutItemStatePersister
    {
        Boolean SaveState(IMesDocumentManager shell, IMesDocumentManagerView shellView, String fileName);
        Boolean LoadState(IMesDocumentManager shell, IMesDocumentManagerView shellView, String fileName);
    }
}
