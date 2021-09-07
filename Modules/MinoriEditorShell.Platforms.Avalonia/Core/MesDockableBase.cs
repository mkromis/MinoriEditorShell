using Dock.Model.Adapters;
using Dock.Model.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Core
{
    /// <summary>
    /// This is used as a base for other dockable interfaces
    /// </summary>
    [DataContract(IsReference = true)]
    public abstract class MesDockableBase : MvxNotifyPropertyChanged, IDockable
    {
        private readonly TrackingAdapter _trackingAdapter;
        private String _id = String.Empty;
        private String _title = String.Empty;
        private Object _context;
        private IDockable _owner;
        private IFactory _factory;
        private Boolean _canClose = true;
        private Boolean _canPin = true;
        private Boolean _canFloat = true;

        /// <summary>
        /// Unique ID
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public String Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// The title / Name
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public String Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Content?
        /// </summary>
        [IgnoreDataMember]
        public Object Context
        {
            get => _context;
            set => SetProperty(ref _context, value);
        }

        /// <summary>
        /// Define which dock we are in
        /// </summary>
        [IgnoreDataMember]
        public IDockable Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        /// <summary>
        /// The factory class, should be same for all classes
        /// </summary>
        [IgnoreDataMember]
        public IFactory Factory
        {
            get => _factory;
            set => SetProperty(ref _factory, value);
        }

        /// <summary>
        /// Can the window close
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean CanClose
        {
            get => _canClose;
            set => SetProperty(ref _canClose, value);
        }

        /// <summary>
        /// Can pin the dockable window
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean CanPin
        {
            get => _canPin;
            set => SetProperty(ref _canPin, value);
        }

        /// <summary>
        /// Set if document can close
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public Boolean CanFloat
        {
            get => _canFloat;
            set => SetProperty(ref _canFloat, value);
        }

        /// <summary>
        /// Initializes new instance of the <see cref="MesDockableBase"/> class.
        /// </summary>
        protected MesDockableBase() =>
            _trackingAdapter = new TrackingAdapter();

        /// <summary>
        /// Can Close
        /// </summary>
        /// <returns></returns>
        public virtual Boolean OnClose() => true;

        /// <summary>
        /// Selection changed
        /// </summary>
        public virtual void OnSelected()
        {
        }

        /// <summary>
        /// Get Visible bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetVisibleBounds(out Double x, out Double y, out Double width, out Double height) =>
            _trackingAdapter.GetVisibleBounds(out x, out y, out width, out height);

        /// <summary>
        /// Set Visible bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetVisibleBounds(Double x, Double y, Double width, Double height)
        {
            _trackingAdapter.SetVisibleBounds(x, y, width, height);
            OnVisibleBoundsChanged(x, y, width, height);
        }

        /// <summary>
        /// Visible bouncs changed
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void OnVisibleBoundsChanged(Double x, Double y, Double width, Double height)
        {
        }

        /// <summary>
        /// Get Pinned Bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetPinnedBounds(out Double x, out Double y, out Double width, out Double height) =>
            _trackingAdapter.GetPinnedBounds(out x, out y, out width, out height);

        /// <summary>
        /// Set Pinned Bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetPinnedBounds(Double x, Double y, Double width, Double height)
        {
            _trackingAdapter.SetPinnedBounds(x, y, width, height);
            OnPinnedBoundsChanged(x, y, width, height);
        }

        /// <summary>
        /// Pinned Bounds Change method event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void OnPinnedBoundsChanged(Double x, Double y, Double width, Double height)
        {
        }

        /// <summary>
        /// Get Tab Bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetTabBounds(out Double x, out Double y, out Double width, out Double height) =>
            _trackingAdapter.GetTabBounds(out x, out y, out width, out height);

        /// <summary>
        /// Set Tab Bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetTabBounds(Double x, Double y, Double width, Double height)
        {
            _trackingAdapter.SetTabBounds(x, y, width, height);
            OnTabBoundsChanged(x, y, width, height);
        }

        /// <summary>
        /// Tab Bounds changed method event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void OnTabBoundsChanged(Double x, Double y, Double width, Double height)
        {
        }

        /// <summary>
        /// Get Pointer position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void GetPointerPosition(out Double x, out Double y) =>
            _trackingAdapter.GetPointerPosition(out x, out y);

        /// <summary>
        /// Set pointer position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPointerPosition(Double x, Double y)
        {
            _trackingAdapter.SetPointerPosition(x, y);
            OnPointerPositionChanged(x, y);
        }

        /// <summary>
        /// Event for pointer position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void OnPointerPositionChanged(Double x, Double y)
        {
        }

        /// <summary>
        /// Get the screen position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void GetPointerScreenPosition(out Double x, out Double y) =>
            _trackingAdapter.GetPointerScreenPosition(out x, out y);

        /// <summary>
        /// Set the pointer position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPointerScreenPosition(Double x, Double y)
        {
            _trackingAdapter.SetPointerScreenPosition(x, y);
            OnPointerScreenPositionChanged(x, y);
        }

        /// <summary>
        /// When pointer screen position changed
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void OnPointerScreenPositionChanged(Double x, Double y)
        {
        }
    }
}