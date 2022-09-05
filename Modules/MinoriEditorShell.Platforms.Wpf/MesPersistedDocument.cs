using MinoriEditorShell.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Wpf
{
    /// <inheritdoc cref="IMesPersistedDocument"/>
    public abstract class MesPersistedDocument : MesDocument, IMesPersistedDocument
    {
        private bool _isDirty;

        /// <inheritdoc />
        public bool IsNew { get; private set; }
        /// <inheritdoc />
        public string FileName { get; private set; }
        /// <inheritdoc />
        public string FilePath { get; private set; }
        /// <summary>
        /// Shows weather document is modified since save
        /// </summary>
        public bool IsDirty
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
        /// <summary>
        /// Update tile / tab name
        /// </summary>
        private void UpdateDisplayName() => DisplayName = IsDirty ? FileName + "*" : FileName;
        /// <inheritdoc />
        public async Task New(string fileName)
        {
            FileName = fileName;
            UpdateDisplayName();

            IsNew = true;
            IsDirty = false;

            await DoNew().ConfigureAwait(true);
        }
        /// <summary>
        /// Task to create new document without path
        /// </summary>
        /// <returns></returns>
        protected abstract Task DoNew();
        /// <inheritdoc />
        public async Task Load(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            
            UpdateDisplayName();
            await DoLoad(filePath).ConfigureAwait(true);

            IsNew = false;
            IsDirty = false;

        }
        /// <summary>
        /// Task to load the file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected abstract Task DoLoad(string filePath);
        /// <inheritdoc />
        public async Task Save(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            UpdateDisplayName();

            await DoSave(filePath).ConfigureAwait(true);

            IsDirty = false;
            IsNew = false;
        }
        /// <summary>
        /// Task to do saving
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected abstract Task DoSave(string filePath);
    }
}