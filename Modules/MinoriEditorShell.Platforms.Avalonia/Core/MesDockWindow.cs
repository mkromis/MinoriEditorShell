using Dock.Model.Adapters;
using Dock.Model.Controls;
using Dock.Model.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Core
{
    internal class MesDockWindow : MvxNotifyPropertyChanged, IDockWindow
    {
        private readonly IHostAdapter _hostAdapter;
        private String _id;
        private Double _x;
        private Double _y;
        private Double _width;
        private Double _height;
        private Boolean _topmost;
        private String _title;
        private IDockable _owner;
        private IFactory _factory;
        private IRootDock _layout;
        private IHostWindow _host;

        /// <summary>
        /// Unique ID?
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public String Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// Window X
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        /// <summary>
        /// Window Y
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        /// <summary>
        /// Window Width
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        /// <summary>
        /// Window Height
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        /// <summary>
        /// Are we on top most view
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean Topmost
        {
            get => _topmost;
            set => SetProperty(ref _topmost, value);
        }

        /// <summary>
        /// Declare title
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public String Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Owner dock
        /// </summary>
        [IgnoreDataMember]
        public IDockable Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        /// <summary>
        /// Main factory dock
        /// </summary>
        [IgnoreDataMember]
        public IFactory Factory
        {
            get => _factory;
            set => SetProperty(ref _factory, value);
        }

        /// <summary>
        /// Layout dock
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public IRootDock Layout
        {
            get => _layout;
            set => SetProperty(ref _layout, value);
        }

        /// <summary>
        /// Host window
        /// </summary>
        [IgnoreDataMember]
        public IHostWindow Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        /// <summary>
        /// Initializes new instance of the <see cref="MesDockWindow"/> class.
        /// </summary>
        public MesDockWindow()
        {
            _id = nameof(IDockWindow);
            _title = nameof(IDockWindow);
            _hostAdapter = new HostAdapter(this);
        }

        /// <summary>
        /// Window is closing
        /// </summary>
        /// <returns></returns>
        public virtual Boolean OnClose() => true;

        /// <summary>
        /// Move or drag has begun
        /// </summary>
        /// <returns></returns>
        public virtual Boolean OnMoveDragBegin() => true;

        /// <summary>
        /// Move or drag
        /// </summary>
        public virtual void OnMoveDrag()
        {
        }

        /// <summary>
        /// Drag has ended
        /// </summary>
        public virtual void OnMoveDragEnd()
        {
        }

        /// <summary>
        /// Save state
        /// </summary>
        public void Save() => _hostAdapter.Save();

        /// <summary>
        /// Present window
        /// </summary>
        /// <param name="isDialog">Are we a dialog</param>
        public void Present(Boolean isDialog) => _hostAdapter.Present(isDialog);

        /// <summary>
        /// Exit window?
        /// </summary>
        public void Exit() => _hostAdapter.Exit();
    }
}