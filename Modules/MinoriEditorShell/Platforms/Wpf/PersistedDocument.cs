using MinoriEditorShell.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Wpf
{
    public abstract class PersistedDocument : Document, IPersistedDocument
    {
        private Boolean _isDirty;

        public Boolean IsNew { get; private set; }
        public String FileName { get; private set; }
        public String FilePath { get; private set; }

        public Boolean IsDirty
        {
            get => _isDirty;
            set
            {
                if (value == _isDirty)
                {
                    return;
                }

                _isDirty = value;
                RaisePropertyChanged(() => IsDirty);
                UpdateDisplayName();
            }
        }

#warning CanClose
#if false
        public override void CanClose(System.Action<bool> callback)
        {
            // TODO: Show save prompt.
            callback(!IsDirty);
        }
#endif

        private void UpdateDisplayName() => DisplayName = (IsDirty) ? FileName + "*" : FileName;

        public async Task New(String fileName)
        {
            FileName = fileName;
            UpdateDisplayName();

            IsNew = true;
            IsDirty = false;

            await DoNew();
        }

        protected abstract Task DoNew();

        public async Task Load(String filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            UpdateDisplayName();

            IsNew = false;
            IsDirty = false;

            await DoLoad(filePath);
        }

        protected abstract Task DoLoad(String filePath);

        public async Task Save(String filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            UpdateDisplayName();

            await DoSave(filePath);

            IsDirty = false;
            IsNew = false;
        }

        protected abstract Task DoSave(String filePath);
    }
}
