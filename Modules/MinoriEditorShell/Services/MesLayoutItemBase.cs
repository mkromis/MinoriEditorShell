using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace MinoriEditorShell.Services
{
    public abstract class MesLayoutItemBase : MvxViewModel, IMesLayoutItem
    {
        private bool _isSelected;
        private string _displayName;

        /// <inheritdoc />
        public abstract ICommand CloseCommand { get; }

        /// <inheritdoc />
        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        [Browsable(false)]
        public string ContentId => Id.ToString();

        /// <inheritdoc />
        [Browsable(false)]
        public virtual Uri IconSource => null;

        /// <inheritdoc />
        public string DisplayName
        {
            get => _displayName;
            protected set => SetProperty(ref _displayName, value);
        }

        /// <inheritdoc />
        [Browsable(false)]
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        /// <inheritdoc />
        [Browsable(false)]
        public virtual bool ShouldReopenOnStart => false;

        /// <inheritdoc />
        public virtual void LoadState(BinaryReader reader)
        {
        }

        /// <inheritdoc />
        public virtual void SaveState(BinaryWriter writer)
        {
        }
    }
}