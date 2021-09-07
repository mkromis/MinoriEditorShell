using Dock.Model.Adapters;
using Dock.Model.Core;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Avalonia.Core
{
    /// <summary>
    /// Base of dockable interfaces
    /// </summary>
    public abstract class MesDockBase : MesDockableBase, IDock
    {
        internal readonly INavigateAdapter _navigateAdapter;
        private IList<IDockable> _visibleDockables;
        private IList<IDockable> _hiddenDockables;
        private IList<IDockable> _pinnedDockables;
        private IDockable _activeDockable;
        private IDockable _defaultDockable;
        private IDockable _focusedDockable;
        private Double _proportion = Double.NaN;
        private DockMode _dock = DockMode.Center;
        private Boolean _isCollapsable = true;
        private Boolean _isActive;

        /// <summary>
        /// Initializes new instance of the <see cref="MesDockBase"/> class.
        /// </summary>
        protected MesDockBase()
        {
            _navigateAdapter = new NavigateAdapter(this);
            GoBack = new MvxCommand(() => _navigateAdapter.GoBack());
            GoForward = new MvxCommand(() => _navigateAdapter.GoForward());
            Navigate = new MvxCommand<Object>(root => _navigateAdapter.Navigate(root, true));
            Close = new MvxCommand(() => _navigateAdapter.Close());
        }

        /// <summary>
        /// List of visible docks
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IList<IDockable> VisibleDockables
        {
            get => _visibleDockables;
            set => SetProperty(ref _visibleDockables, value);
        }

        /// <summary>
        /// Dock items that are hidden
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IList<IDockable> HiddenDockables
        {
            get => _hiddenDockables;
            set => SetProperty(ref _hiddenDockables, value);
        }

        /// <summary>
        /// Docks that are pinned
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IList<IDockable> PinnedDockables
        {
            get => _pinnedDockables;
            set => SetProperty(ref _pinnedDockables, value);
        }

        /// <summary>
        /// Active dock
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IDockable ActiveDockable
        {
            get => _activeDockable;
            set
            {
                SetProperty(ref _activeDockable, value);
                Factory?.OnActiveDockableChanged(value);
                if (value is { })
                {
                    Factory?.UpdateDockable(value, this);
                    value.OnSelected();
                }
                if (value is { })
                {
                    Factory?.SetFocusedDockable(this, value);
                }
                RaisePropertyChanged(nameof(CanGoBack));
                RaisePropertyChanged(nameof(CanGoForward));
            }
        }

        /// <summary>
        /// Default dock item
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IDockable DefaultDockable
        {
            get => _defaultDockable;
            set => SetProperty(ref _defaultDockable, value);
        }

        /// <summary>
        /// dock item that is focused
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IDockable FocusedDockable
        {
            get => _focusedDockable;
            set
            {
                SetProperty(ref _focusedDockable, value);
                Factory?.OnFocusedDockableChanged(value);
            }
        }

        /// <summary>
        /// Porportion of the dock size
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Double Proportion
        {
            get => _proportion;
            set => SetProperty(ref _proportion, value);
        }

        /// <summary>
        /// Dock mode
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public DockMode Dock
        {
            get => _dock;
            set => SetProperty(ref _dock, value);
        }

        /// <summary>
        /// Determine if dock item is active
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        /// <summary>
        /// Is dock item collapsed (on the side)
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean IsCollapsable
        {
            get => _isCollapsable;
            set => SetProperty(ref _isCollapsable, value);
        }

        /// <summary>
        /// Has a back step
        /// </summary>
        [IgnoreDataMember]
        public Boolean CanGoBack => _navigateAdapter.CanGoBack;

        /// <summary>
        /// Has a forward step
        /// </summary>
        [IgnoreDataMember]
        public Boolean CanGoForward => _navigateAdapter.CanGoForward;

        /// <summary>
        /// Command to go back
        /// </summary>
        [IgnoreDataMember]
        public ICommand GoBack { get; }

        /// <summary>
        /// Command to go forward
        /// </summary>
        [IgnoreDataMember]
        public ICommand GoForward { get; }

        /// <summary>
        /// Navigate command?
        /// </summary>
        [IgnoreDataMember]
        public ICommand Navigate { get; }

        /// <summary>
        /// Close command
        /// </summary>
        [IgnoreDataMember]
        public ICommand Close { get; }
    }
}