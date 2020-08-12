using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Views;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    public abstract class MesDocument : MesLayoutItemBase, IMesDocument
#warning fix command handler inherit.
    //ICommandHandler<UndoCommandDefinition>,
    //ICommandHandler<RedoCommandDefinition>,
    //ICommandHandler<SaveFileCommandDefinition>,
    //ICommandHandler<SaveFileAsCommandDefinition>
    {
        //private IMesUndoRedoManager _undoRedoManager;
        //public IMesUndoRedoManager UndoRedoManager => _undoRedoManager ?? (_undoRedoManager = new MesUndoRedoManager());

        /// <summary>
        /// Sets weather a document can be closed by close button
        /// </summary>
        public Boolean CanClose { get; protected set; } = true;

        /// <summary>
        /// Removes the document from manager
        /// </summary>
        public override ICommand CloseCommand => new MvxCommand(() => Mvx.IoCProvider.Resolve<IMesDocumentManager>().Documents.Remove(this));

#warning Fix Toolbar
#if false
        private ToolBarDefinition _toolBarDefinition;
        public ToolBarDefinition ToolBarDefinition
        {
            get => _toolBarDefinition;
            protected set
            {
                if (SetProperty(ref _toolBarDefinition, value))
                {
                    RaisePropertyChanged(() => ToolBar);
                }
            }
        }

        private IToolBar _toolBar;
        public IToolBar ToolBar
        {
            get
            {
                if (_toolBar != null)
                    return _toolBar;

                if (ToolBarDefinition == null)
                    return null;

                IToolBarBuilder toolBarBuilder = Mvx.IoCProvider.Resolve<IToolBarBuilder>();
                _toolBar = new ToolBarModel();
                toolBarBuilder.BuildToolBar(ToolBarDefinition, _toolBar);
                return _toolBar;
            }
        }
#endif

        public IMvxView View { get; set; }

#if false
        void ICommandHandler<UndoCommandDefinition>.Update(Command command)
	    {
            command.Enabled = UndoRedoManager.CanUndo;
	    }

	    Task ICommandHandler<UndoCommandDefinition>.Run(Command command)
	    {
            UndoRedoManager.Undo(1);
            return TaskUtility.Completed;
	    }

        void ICommandHandler<RedoCommandDefinition>.Update(Command command)
        {
            command.Enabled = UndoRedoManager.CanRedo;
        }

        Task ICommandHandler<RedoCommandDefinition>.Run(Command command)
        {
            UndoRedoManager.Redo(1);
            return TaskUtility.Completed;
        }

        void ICommandHandler<SaveFileCommandDefinition>.Update(Command command)
        {
            command.Enabled = this is IPersistedDocument;
        }

	    async Task ICommandHandler<SaveFileCommandDefinition>.Run(Command command)
	    {
	        var persistedDocument = this as IPersistedDocument;
	        if (persistedDocument == null)
            {
                return;
            }

            // If file has never been saved, show Save As dialog.
            if (persistedDocument.IsNew)
	        {
	            await DoSaveAs(persistedDocument);
	            return;
	        }

            // Save file.
            String filePath = persistedDocument.FilePath;
            await persistedDocument.Save(filePath);
	    }

        void ICommandHandler<SaveFileAsCommandDefinition>.Update(Command command)
        {
            command.Enabled = this is IPersistedDocument;
        }

	    async Task ICommandHandler<SaveFileAsCommandDefinition>.Run(Command command)
	    {
            var persistedDocument = this as IPersistedDocument;
            if (persistedDocument == null)
                return;

            await DoSaveAs(persistedDocument);
	    }
#endif
#if false
	    private static async Task DoSaveAs(IPersistedDocument persistedDocument)
	    {
            throw new NotImplementedException();

            // Show user dialog to choose filename.
            var dialog = new SaveFileDialog();
            dialog.FileName = persistedDocument.FileName;
            var filter = string.Empty;

            var fileExtension = Path.GetExtension(persistedDocument.FileName);
            var fileType = IoC.GetAll<IEditorProvider>()
                .SelectMany(x => x.FileTypes)
                .SingleOrDefault(x => x.FileExtension == fileExtension);
            if (fileType != null)
                filter = fileType.Name + "|*" + fileType.FileExtension + "|";

            filter += "All Files|*.*";
            dialog.Filter = filter;

            if (dialog.ShowDialog() != true)
                return;

            var filePath = dialog.FileName;

            // Save file.
            await persistedDocument.Save(filePath);
	    }
#endif
    }
}