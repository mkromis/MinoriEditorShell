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

        public abstract ICommand CloseCommand { get; }

        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        [Browsable(false)]
        public string ContentId => Id.ToString();

        [Browsable(false)]
        public virtual Uri IconSource => null;

        private String _displayName;

        public String DisplayName
        {
            get => _displayName;
            protected set => SetProperty(ref _displayName, value);
        }

        [Browsable(false)]
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        [Browsable(false)]
        public virtual bool ShouldReopenOnStart => false;

        public virtual void LoadState(BinaryReader reader)
        {
        }

        public virtual void SaveState(BinaryWriter writer)
        {
        }
    }
}