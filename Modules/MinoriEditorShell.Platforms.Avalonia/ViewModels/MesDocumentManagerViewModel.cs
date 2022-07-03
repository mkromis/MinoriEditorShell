using Avalonia.Controls.Templates;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Core.Events;
using MinoriEditorShell.Platforms.Avalonia.Controls;
using MinoriEditorShell.Platforms.Avalonia.Core;
using MinoriEditorShell.Services;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.ViewModels
{
    public class MesDocumentManagerViewModel : MvxViewModel, IFactory, IMesDocumentManager
    {
        private IDocumentDock _documentDock;

        //private ITool _findTool;
        private IRootDock _layout;

        //private ITool _replaceTool;
        private IRootDock _rootDock;

        /// <summary>
        /// Create document dock
        /// </summary>
        /// <returns></returns>
        //public IDocumentDock CreateDocumentDock() => new FilesDocumentDock();

        /// <summary>
        /// Initializes the new instance of <see cref="Factory"/> class.
        /// </summary>
        public MesDocumentManagerViewModel()
        {
            VisibleDockableControls = new Dictionary<IDockable, IDockableControl>();
            PinnedDockableControls = new Dictionary<IDockable, IDockableControl>();
            TabDockableControls = new Dictionary<IDockable, IDockableControl>();
            DockControls = new MvxObservableCollection<IDockControl>();
            HostWindows = new MvxObservableCollection<IHostWindow>();
        }

        public event EventHandler<ActiveDockableChangedEventArgs> ActiveDockableChanged;

        public event EventHandler ActiveDocumentChanged;

        public event EventHandler ActiveDocumentChanging;

        public event EventHandler<DockableAddedEventArgs> DockableAdded;

        public event EventHandler<DockableClosedEventArgs> DockableClosed;

        public event EventHandler<DockableMovedEventArgs> DockableMoved;

        public event EventHandler<DockablePinnedEventArgs> DockablePinned;

        public event EventHandler<DockableRemovedEventArgs> DockableRemoved;

        public event EventHandler<DockableSwappedEventArgs> DockableSwapped;

        public event EventHandler<DockableUnpinnedEventArgs> DockableUnpinned;

        public event EventHandler<FocusedDockableChangedEventArgs> FocusedDockableChanged;

        public event EventHandler UpdateFloatingWindows;

        public event EventHandler<WindowAddedEventArgs> WindowAdded;

        public event EventHandler<WindowClosedEventArgs> WindowClosed;

        public event EventHandler<WindowClosingEventArgs> WindowClosing;

        public event EventHandler<WindowMoveDragEventArgs> WindowMoveDrag;

        public event EventHandler<WindowMoveDragBeginEventArgs> WindowMoveDragBegin;

        public event EventHandler<WindowMoveDragEndEventArgs> WindowMoveDragEnd;

        public event EventHandler<WindowOpenedEventArgs> WindowOpened;

        public event EventHandler<WindowRemovedEventArgs> WindowRemoved;

        public IMesLayoutItem ActiveItem { get; set; }

        public IDictionary<String, Func<Object>> ContextLocator { get; set; }
        public Func<Object> DefaultContextLocator { get; set; }

        public IDictionary<String, Func<IDockable>> DockableLocator { get; set; }

        public IList<IDockControl> DockControls { get; }

        public IDocumentDock DocumentDock { get; internal set; }

        public MvxObservableCollection<IMesDocument> Documents { get; }

        public IDictionary<String, Func<IHostWindow>> HostWindowLocator { get; set; }
        public Func<IHostWindow> DefaultHostWindowLocator { get; set; }

        public IList<IHostWindow> HostWindows { get; }

        public Boolean IsDataTemplatesInitialized { get; }

        /// <summary>
        /// Get initial layout
        /// </summary>
        public IRootDock Layout
        {
            get
            {
                if (_layout == null)
                {
                    _layout = CreateLayout();
                    if (_layout is { })
                    {
                        InitLayout(_layout);
                    }
                }
                return _layout;
            }
        }

        public IDictionary<IDockable, IDockableControl> PinnedDockableControls { get; }

        public IMesDocument SelectedDocument { get; }

        public Boolean ShowFloatingWindowsInTaskbar { get; set; }

        public IDictionary<IDockable, IDockableControl> TabDockableControls { get; }

        public MvxObservableCollection<IMesTool> Tools { get; }

        public IDictionary<IDockable, IDockableControl> VisibleDockableControls { get; }

        public void AddDockable(IDock dock, IDockable dockable)
        {
            UpdateDockable(dockable, dock);
            dock.VisibleDockables ??= CreateList<IDockable>();
            dock.VisibleDockables.Add(dockable);
            OnDockableAdded(dockable);
        }

        public void AddWindow(IRootDock rootDock, IDockWindow window)
        {
            rootDock.Windows ??= CreateList<IDockWindow>();
            rootDock.Windows.Add(window);
            OnWindowAdded(window);
            UpdateDockWindow(window, rootDock);
        }

        public void Close() => throw new NotImplementedException();

        public virtual void CloseDockable(IDockable dockable)
        {
            if (dockable.OnClose())
            {
                RemoveDockable(dockable, true);
                OnDockableClosed(dockable);
            }
        }

        public virtual void CloseAllDockables(IDockable dockable) => throw new NotImplementedException();

        public virtual void CloseLeftDockables(IDockable dockable) => throw new NotImplementedException();

        public virtual void CloseRightDockables(IDockable dockable) => throw new NotImplementedException();

        public virtual void CloseOtherDockables(IDockable dockable) => throw new NotImplementedException();

        public void CloseDocument(IMesDocument document) => throw new NotImplementedException();

        public void CollapseDock(IDock dock)
        {
            if (!dock.IsCollapsable || dock.VisibleDockables is null || dock.VisibleDockables.Count != 0)
            {
                return;
            }

            if (dock.PinnedDockables is not null && dock.PinnedDockables.Count != 0)
            {
                return;
            }

            if (dock.Owner is IDock ownerDock && ownerDock.VisibleDockables is { })
            {
                List<IDockable> toRemove = new();
                Int32 dockIndex = ownerDock.VisibleDockables.IndexOf(dock);

                if (dockIndex >= 0)
                {
                    Int32 indexSplitterPrevious = dockIndex - 1;
                    if (dockIndex > 0 && indexSplitterPrevious >= 0)
                    {
                        IDockable previousVisible = ownerDock.VisibleDockables[indexSplitterPrevious];
                        if (previousVisible is IProportionalDockSplitter splitterPrevious)
                        {
                            toRemove.Add(splitterPrevious);
                        }
                    }

                    Int32 indexSplitterNext = dockIndex + 1;
                    if (dockIndex < ownerDock.VisibleDockables.Count - 1 && indexSplitterNext >= 0)
                    {
                        IDockable nextVisible = ownerDock.VisibleDockables[indexSplitterNext];
                        if (nextVisible is IProportionalDockSplitter splitterNext)
                        {
                            toRemove.Add(splitterNext);
                        }
                    }

                    foreach (IDockable removeVisible in toRemove)
                    {
                        RemoveDockable(removeVisible, true);
                    }
                }
            }

            if (dock is IRootDock rootDock && rootDock.Window is { })
            {
                RemoveWindow(rootDock.Window);
            }
            else
            {
                RemoveDockable(dock, true);
            }
        }

        public IDockDock CreateDockDock() => new MesDockDock();

        public IDockWindow CreateDockWindow() => new MesDockWindow();

        public IDocumentDock CreateDocumentDock() => new MesDocumentDock();

        public virtual IRootDock CreateLayout()
        {
            //var untitledFileViewModel = new FileViewModel()
            //{
            //    Path = string.Empty,
            //    Title = "Untitled",
            //    Text = "",
            //    Encoding = Encoding.Default.WebName
            //};

            //var findViewModel = new FindViewModel()
            //{
            //    Id = "Find",
            //    Title = "Find"
            //};

            //var replaceViewModel = new ReplaceViewModel()
            //{
            //    Id = "Replace",
            //    Title = "Replace"
            //};

            MesDocumentDock documentDock = new()
            {
                Id = "Files",
                Title = "Files",
                IsCollapsable = false,
                Proportion = Double.NaN,
                //ActiveDockable = untitledFileViewModel,
                VisibleDockables = CreateList<IDockable>
                (
                //untitledFileViewModel
                ),
                CanCreateDocument = true
            };

            MesProportionalDock tools = new()
            {
                Proportion = 0.2,
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>
                (
                    new MesToolDock
                    {
                        //ActiveDockable = findViewModel,
                        VisibleDockables = CreateList<IDockable>
                        (
                        //findViewModel
                        ),
                        Alignment = Alignment.Right,
                        GripMode = GripMode.Visible
                    },
                    new MesProportionalDockSplitter(),
                    new MesToolDock
                    {
                        //ActiveDockable = replaceViewModel,
                        VisibleDockables = CreateList<IDockable>
                        (
                        //replaceViewModel
                        ),
                        Alignment = Alignment.Right,
                        GripMode = GripMode.Visible
                    }
                )
            };

            IRootDock windowLayout = CreateRootDock();
            windowLayout.Title = "Default";
            MesProportionalDock windowLayoutContent = new()
            {
                Orientation = Orientation.Horizontal,
                IsCollapsable = false,
                VisibleDockables = CreateList<IDockable>
                (
                    documentDock,
                    new MesProportionalDockSplitter(),
                    tools
                )
            };
            windowLayout.IsCollapsable = false;
            windowLayout.VisibleDockables = CreateList<IDockable>(windowLayoutContent);
            windowLayout.ActiveDockable = windowLayoutContent;

            IRootDock rootDock = CreateRootDock();

            rootDock.IsCollapsable = false;
            rootDock.VisibleDockables = CreateList<IDockable>(windowLayout);
            rootDock.ActiveDockable = windowLayout;
            rootDock.DefaultDockable = windowLayout;

            _documentDock = documentDock;
            DocumentDock = documentDock;
            _rootDock = rootDock;
            //_findTool = findViewModel;
            //_replaceTool = replaceViewModel;

            return rootDock;
        }

#warning disable CreateLayout
        //public IRootDock CreateLayout() => CreateRootDock();

        public IList<T> CreateList<T>(params T[] items) => new ObservableCollection<T>(items);

        public IProportionalDock CreateProportionalDock() => new MesProportionalDock();

        public IRootDock CreateRootDock() => new MesRootDock();

        public virtual IDock CreateSplitLayout(IDock dock, IDockable dockable, DockOperation operation)
        {
            IDock split;

            if (dockable is IDock dockableDock)
            {
                split = dockableDock;
            }
            else
            {
                split = CreateProportionalDock();
                split.Id = nameof(IProportionalDock);
                split.Title = nameof(IProportionalDock);
                split.VisibleDockables = CreateList<IDockable>();
                if (split.VisibleDockables is not null)
                {
                    split.VisibleDockables.Add(dockable);
                    OnDockableAdded(dockable);
                    split.ActiveDockable = dockable;
                }
            }

            Double containerProportion = dock.Proportion;
            dock.Proportion = Double.NaN;

            IProportionalDock layout = CreateProportionalDock();
            layout.Id = nameof(IProportionalDock);
            layout.Title = nameof(IProportionalDock);
            layout.VisibleDockables = CreateList<IDockable>();
            layout.Proportion = containerProportion;

            IProportionalDockSplitter splitter = CreateProportionalDockSplitter();
            splitter.Id = nameof(IProportionalDockSplitter);
            splitter.Title = nameof(IProportionalDockSplitter);

            switch (operation)
            {
                case DockOperation.Left:
                case DockOperation.Right:
                    layout.Orientation = Orientation.Horizontal;
                    break;

                case DockOperation.Top:
                case DockOperation.Bottom:
                    layout.Orientation = Orientation.Vertical;
                    break;
            }

            switch (operation)
            {
                case DockOperation.Left:
                case DockOperation.Top:
                    if (layout.VisibleDockables is not null)
                    {
                        layout.VisibleDockables.Add(split);
                        OnDockableAdded(split);
                        layout.ActiveDockable = split;
                    }
                    break;

                case DockOperation.Right:
                case DockOperation.Bottom:
                    if (layout.VisibleDockables is not null)
                    {
                        layout.VisibleDockables.Add(dock);
                        OnDockableAdded(dock);
                        layout.ActiveDockable = dock;
                    }
                    break;
            }

            layout.VisibleDockables?.Add(splitter);
            OnDockableAdded(splitter);

            switch (operation)
            {
                case DockOperation.Left:
                case DockOperation.Top:
                    if (layout.VisibleDockables is not null)
                    {
                        layout.VisibleDockables.Add(dock);
                        OnDockableAdded(dock);
                        layout.ActiveDockable = dock;
                    }
                    break;

                case DockOperation.Right:
                case DockOperation.Bottom:
                    if (layout.VisibleDockables is not null)
                    {
                        layout.VisibleDockables.Add(split);
                        OnDockableAdded(split);
                        layout.ActiveDockable = split;
                    }
                    break;
            }

            return layout;
        }

        public IProportionalDockSplitter CreateProportionalDockSplitter() => new MesProportionalDockSplitter();

        public IToolDock CreateToolDock() => new MesToolDock();

        public IDockWindow CreateWindowFrom(IDockable dockable)
        {
            IDockable? target;
            Boolean topmost;

            switch (dockable)
            {
                case ITool:
                    {
                        target = CreateToolDock();
                        target.Id = nameof(IToolDock);
                        target.Title = nameof(IToolDock);
                        if (target is IDock dock)
                        {
                            dock.VisibleDockables = CreateList<IDockable>();
                            if (dock.VisibleDockables is not null)
                            {
                                dock.VisibleDockables.Add(dockable);
                                OnDockableAdded(dockable);
                                dock.ActiveDockable = dockable;
                            }
                        }
                        topmost = true;
                    }
                    break;

                case IDocument:
                    {
                        target = CreateDocumentDock();
                        target.Id = nameof(IDocumentDock);
                        target.Title = nameof(IDocumentDock);
                        if (target is IDock dock)
                        {
                            dock.VisibleDockables = CreateList<IDockable>();
                            if (dockable.Owner is IDocumentDock sourceDocumentDock)
                            {
                                ((target as IDocumentDock)!).CanCreateDocument = sourceDocumentDock.CanCreateDocument;
                            }
                            if (dock.VisibleDockables is not null)
                            {
                                dock.VisibleDockables.Add(dockable);
                                OnDockableAdded(dockable);
                                dock.ActiveDockable = dockable;
                            }
                        }
                        topmost = false;
                    }
                    break;

                case IToolDock:
                    {
                        target = dockable;
                        topmost = true;
                    }
                    break;

                case IDocumentDock:
                    {
                        target = dockable;
                        topmost = false;
                    }
                    break;

                case IProportionalDock proportionalDock:
                    {
                        target = proportionalDock;
                        topmost = false;
                    }
                    break;

                case IDockDock dockDock:
                    {
                        target = dockDock;
                        topmost = false;
                    }
                    break;

                case IRootDock rootDock:
                    {
                        target = rootDock.ActiveDockable;
                        topmost = false;
                    }
                    break;

                default:
                    {
                        return null;
                    }
            }

            IRootDock root = CreateRootDock();
            root.Id = nameof(IRootDock);
            root.Title = nameof(IRootDock);
            root.VisibleDockables = CreateList<IDockable>();
            if (root.VisibleDockables is not null && target is not null)
            {
                root.VisibleDockables.Add(target);
                OnDockableAdded(target);
                root.ActiveDockable = target;
                root.DefaultDockable = target;
            }
            root.Owner = null;

            IDockWindow window = CreateDockWindow();
            window.Id = nameof(IDockWindow);
            window.Title = "";
            window.Width = Double.NaN;
            window.Height = Double.NaN;
            window.Topmost = topmost;
            window.Layout = root;

            root.Window = window;

            return window;
        }

        public IDockable FindDockable(IDock dock, Func<IDockable, Boolean> predicate)
        {
            if (predicate(dock))
            {
                return dock;
            }

            if (dock.VisibleDockables is not null)
            {
                foreach (IDockable dockable in dock.VisibleDockables)
                {
                    if (predicate(dockable))
                    {
                        return dockable;
                    }

                    if (dockable is IDock childDock)
                    {
                        IDockable result = FindDockable(childDock, predicate);
                        if (result is not null)
                        {
                            return result;
                        }
                    }
                }
            }

            if (dock is IRootDock rootDock && rootDock.Windows is not null)
            {
                foreach (IDockWindow window in rootDock.Windows)
                {
                    if (window.Layout is null)
                    {
                        continue;
                    }

                    if (predicate(window.Layout))
                    {
                        return window.Layout;
                    }

                    IDockable result = FindDockable(window.Layout, predicate);
                    if (result is not null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public virtual IRootDock FindRoot(IDockable dockable, Func<IRootDock, Boolean> predicate)
        {
            if (dockable.Owner is null)
            {
                return null;
            }
            return dockable.Owner is IRootDock rootDock && predicate(rootDock)
                ? rootDock
                : FindRoot(dockable.Owner, predicate);
        }

        public void FloatDockable(IDockable dockable)
        {
            if (dockable.Owner is not IDock dock)
            {
                return;
            }

            dock.GetPointerScreenPosition(out Double dockPointerScreenX, out Double dockPointerScreenY);
            dockable.GetPointerScreenPosition(out Double dockablePointerScreenX, out Double dockablePointerScreenY);

            if (Double.IsNaN(dockablePointerScreenX))
            {
                dockablePointerScreenX = dockPointerScreenX;
            }
            if (Double.IsNaN(dockablePointerScreenY))
            {
                dockablePointerScreenY = dockPointerScreenY;
            }

            dock.GetVisibleBounds(out Double ownerX, out Double ownerY, out Double ownerWidth, out Double ownerHeight);
            dockable.GetVisibleBounds(out Double dockableX, out Double dockableY, out Double dockableWidth, out Double dockableHeight);

            if (Double.IsNaN(dockablePointerScreenX))
            {
                dockablePointerScreenX = !Double.IsNaN(dockableX) ? dockableX : !Double.IsNaN(ownerX) ? ownerX : 0;
            }
            if (Double.IsNaN(dockablePointerScreenY))
            {
                dockablePointerScreenY = !Double.IsNaN(dockableY) ? dockableY : !Double.IsNaN(ownerY) ? ownerY : 0;
            }
            if (Double.IsNaN(dockableWidth))
            {
                dockableWidth = Double.IsNaN(ownerWidth) ? 300 : ownerWidth;
            }
            if (Double.IsNaN(dockableHeight))
            {
                dockableHeight = Double.IsNaN(ownerHeight) ? 400 : ownerHeight;
            }

            SplitToWindow(dock, dockable, dockablePointerScreenX, dockablePointerScreenY, dockableWidth, dockableHeight);
        }

        public Object GetContext(String id)
        {
            if (!String.IsNullOrEmpty(id) && ContextLocator != null)
            {
                if (ContextLocator.TryGetValue(id, out Func<Object> locator))
                {
                    return locator.Invoke();
                }
            }
            return null;
        }

        public virtual T GetDockable<T>(String id) where T : class, IDockable
        {
            if (!String.IsNullOrEmpty(id) && DockableLocator != null)
            {
                if (DockableLocator.TryGetValue(id, out Func<IDockable> locator))
                {
                    return locator.Invoke() as T;
                }
            }
            return default;
        }

        public IHostWindow GetHostWindow(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                if (HostWindowLocator.TryGetValue(id, out Func<IHostWindow> locator))
                {
                    return locator.Invoke();
                }
            }
            return null;
        }

        public virtual void InitLayout(IDockable layout)
        {
            UpdateDockable(layout, null);

            if (layout is IDock dock)
            {
                if (dock.DefaultDockable is not null)
                {
                    dock.ActiveDockable = dock.DefaultDockable;
                }
            }

            if (layout is IRootDock rootDock)
            {
                if (rootDock.ShowWindows.CanExecute(null))
                {
                    rootDock.ShowWindows.Execute(null);
                }
            }

            ContextLocator = new Dictionary<String, Func<Object>>
            {
                //["Find"] = () => layout,
                //["Replace"] = () => layout
            };

            DockableLocator = new Dictionary<String, Func<IDockable?>>()
            {
                ["Root"] = () => _rootDock,
                ["Files"] = () => _documentDock,
                //["Find"] = () => _findTool,
                //["Replace"] = () => _replaceTool
            };

            HostWindowLocator = new Dictionary<String, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () => new HostWindow()
            };
        }

        public virtual void InsertDockable(IDock dock, IDockable dockable, Int32 index)
        {
            if (index >= 0)
            {
                UpdateDockable(dockable, dock);
                dock.VisibleDockables ??= CreateList<IDockable>();
                dock.VisibleDockables.Insert(index, dockable);
                OnDockableAdded(dockable);
            }
        }

        public virtual void MoveDockable(IDock dock, IDockable sourceDockable, IDockable targetDockable)
        {
            if (dock.VisibleDockables is null)
            {
                return;
            }

            Int32 sourceIndex = dock.VisibleDockables.IndexOf(sourceDockable);
            Int32 targetIndex = dock.VisibleDockables.IndexOf(targetDockable);

            if (sourceIndex >= 0 && targetIndex >= 0 && sourceIndex != targetIndex)
            {
                dock.VisibleDockables.RemoveAt(sourceIndex);
                OnDockableRemoved(sourceDockable);
                dock.VisibleDockables.Insert(targetIndex, sourceDockable);
                OnDockableAdded(sourceDockable);
                OnDockableMoved(sourceDockable);
                dock.ActiveDockable = sourceDockable;
            }
        }

        public void MoveDockable(IDock sourceDock, IDock targetDock, IDockable sourceDockable, IDockable targetDockable)
        {
            if (targetDock.VisibleDockables is null)
            {
                targetDock.VisibleDockables = CreateList<IDockable>();
                if (targetDock.VisibleDockables is null)
                {
                    return;
                }
            }

            Boolean isSameOwner = sourceDock == targetDock;

            Int32 targetIndex = 0;

            if (sourceDock.VisibleDockables is not null && targetDock.VisibleDockables is not null && targetDock.VisibleDockables.Count > 0)
            {
                if (isSameOwner)
                {
                    Int32 sourceIndex = sourceDock.VisibleDockables.IndexOf(sourceDockable);

                    if (targetDockable is not null)
                    {
                        targetIndex = targetDock.VisibleDockables.IndexOf(targetDockable);
                    }
                    else
                    {
                        targetIndex = targetDock.VisibleDockables.Count - 1;
                    }

                    if (sourceIndex == targetIndex)
                    {
                        return;
                    }
                }
                else
                {
                    if (targetDockable is not null)
                    {
                        targetIndex = targetDock.VisibleDockables.IndexOf(targetDockable);
                        if (targetIndex >= 0)
                        {
                            targetIndex += 1;
                        }
                        else
                        {
                            targetIndex = targetDock.VisibleDockables.Count - 1;
                        }
                    }
                    else
                    {
                        targetIndex = targetDock.VisibleDockables.Count - 1;
                    }
                }
            }

            if (sourceDock.VisibleDockables is not null && targetDock.VisibleDockables is not null)
            {
                if (isSameOwner)
                {
                    Int32 sourceIndex = sourceDock.VisibleDockables.IndexOf(sourceDockable);
                    if (sourceIndex < targetIndex)
                    {
                        targetDock.VisibleDockables.Insert(targetIndex + 1, sourceDockable);
                        OnDockableAdded(sourceDockable);
                        targetDock.VisibleDockables.RemoveAt(sourceIndex);
                        OnDockableRemoved(sourceDockable);
                        OnDockableMoved(sourceDockable);
                    }
                    else
                    {
                        Int32 removeIndex = sourceIndex + 1;
                        if (targetDock.VisibleDockables.Count + 1 > removeIndex)
                        {
                            targetDock.VisibleDockables.Insert(targetIndex, sourceDockable);
                            OnDockableAdded(sourceDockable);
                            targetDock.VisibleDockables.RemoveAt(removeIndex);
                            OnDockableRemoved(sourceDockable);
                            OnDockableMoved(sourceDockable);
                        }
                    }
                }
                else
                {
                    RemoveDockable(sourceDockable, true);
                    targetDock.VisibleDockables.Insert(targetIndex, sourceDockable);
                    OnDockableAdded(sourceDockable);
                    OnDockableMoved(sourceDockable);
                    UpdateDockable(sourceDockable, targetDock);
                    targetDock.ActiveDockable = sourceDockable;
                }
            }
        }

        public virtual void OnActiveDockableChanged(IDockable dockable) =>
            ActiveDockableChanged?.Invoke(this, new ActiveDockableChangedEventArgs(dockable));

        public virtual void OnDockableAdded(IDockable dockable) =>
            DockableAdded?.Invoke(this, new DockableAddedEventArgs(dockable));

        public virtual void OnDockableClosed(IDockable dockable) =>
            DockableClosed?.Invoke(this, new DockableClosedEventArgs(dockable));

        public virtual void OnDockableMoved(IDockable dockable) =>
            DockableMoved?.Invoke(this, new DockableMovedEventArgs(dockable));

        public virtual void OnDockablePinned(IDockable dockable) =>
            DockablePinned?.Invoke(this, new DockablePinnedEventArgs(dockable));

        public virtual void OnDockableRemoved(IDockable dockable) =>
            DockableRemoved?.Invoke(this, new DockableRemovedEventArgs(dockable));

        public virtual void OnDockableSwapped(IDockable dockable) =>
            DockableSwapped?.Invoke(this, new DockableSwappedEventArgs(dockable));

        public virtual void OnDockableUnpinned(IDockable dockable) =>
            DockableUnpinned?.Invoke(this, new DockableUnpinnedEventArgs(dockable));

        public virtual void OnFocusedDockableChanged(IDockable dockable) =>
            FocusedDockableChanged?.Invoke(this, new FocusedDockableChangedEventArgs(dockable));

        public virtual void OnWindowAdded(IDockWindow window) =>
            WindowAdded?.Invoke(this, new WindowAddedEventArgs(window));

        public virtual void OnWindowClosed(IDockWindow window) =>
            WindowClosed?.Invoke(this, new WindowClosedEventArgs(window));

        public Boolean OnWindowClosing(IDockWindow window)
        {
            Boolean canClose = window?.OnClose() ?? true;

            WindowClosingEventArgs eventArgs = new(window)
            {
                Cancel = !canClose
            };

            WindowClosing?.Invoke(this, eventArgs);

            return !eventArgs.Cancel;
        }

        public virtual void OnWindowMoveDrag(IDockWindow window)
        {
            window?.OnMoveDrag();
            WindowMoveDrag?.Invoke(this, new WindowMoveDragEventArgs(window));
        }

        public Boolean OnWindowMoveDragBegin(IDockWindow window)
        {
            Boolean canMoveDrag = window?.OnMoveDragBegin() ?? true;

            WindowMoveDragBeginEventArgs eventArgs = new(window)
            {
                Cancel = !canMoveDrag
            };

            WindowMoveDragBegin?.Invoke(this, eventArgs);

            return !eventArgs.Cancel;
        }

        public virtual void OnWindowMoveDragEnd(IDockWindow window)
        {
            window?.OnMoveDragEnd();
            WindowMoveDragEnd?.Invoke(this, new WindowMoveDragEndEventArgs(window));
        }

        public virtual void OnWindowOpened(IDockWindow window) =>
            WindowOpened?.Invoke(this, new WindowOpenedEventArgs(window));

        public virtual void OnWindowRemoved(IDockWindow window) =>
            WindowRemoved?.Invoke(this, new WindowRemovedEventArgs(window));

        public void OpenDocument(IMesDocument model) => throw new NotImplementedException();

        public virtual void PinDockable(IDockable dockable)
        {
            switch (dockable.Owner)
            {
                case IToolDock toolDock:
                    {
                        Boolean isVisible = false;
                        Boolean isPinned = false;

                        if (toolDock.VisibleDockables is not null)
                        {
                            isVisible = toolDock.VisibleDockables.Contains(dockable);
                        }

                        if (toolDock.PinnedDockables is not null)
                        {
                            isPinned = toolDock.PinnedDockables.Contains(dockable);
                        }

                        if (isVisible && !isPinned)
                        {
                            // Pin dockable.

                            toolDock.PinnedDockables ??= CreateList<IDockable>();

                            if (toolDock.VisibleDockables is not null)
                            {
                                toolDock.VisibleDockables.Remove(dockable);
                                OnDockableRemoved(dockable);
                                toolDock.PinnedDockables.Add(dockable);
                                OnDockablePinned(dockable);
                            }

                            // TODO: Handle ActiveDockable state.
                            // TODO: Handle IsExpanded property of IToolDock.
                            // TODO: Handle AutoHide property of IToolDock.
                        }
                        else if (!isVisible && isPinned)
                        {
                            // Unpin dockable.

                            toolDock.VisibleDockables ??= CreateList<IDockable>();

                            if (toolDock.PinnedDockables is not null)
                            {
                                toolDock.PinnedDockables.Remove(dockable);
                                OnDockableUnpinned(dockable);
                                toolDock.VisibleDockables.Add(dockable);
                                OnDockableAdded(dockable);
                            }

                            // TODO: Handle ActiveDockable state.
                            // TODO: Handle IsExpanded property of IToolDock.
                            // TODO: Handle AutoHide property of IToolDock.
                        }
                        else
                        {
                            // TODO: Handle invalid state.
                        }

                        break;
                    }
            }
        }

        public void RemoveDockable(IDockable dockable, Boolean collapse)
        {
            if (dockable.Owner is not IDock dock || dock.VisibleDockables is null)
            {
                return;
            }

            Int32 index = dock.VisibleDockables.IndexOf(dockable);
            if (index < 0)
            {
                return;
            }

            dock.VisibleDockables.Remove(dockable);
            OnDockableRemoved(dockable);

            Int32 indexActiveDockable = index > 0 ? index - 1 : 0;
            if (dock.VisibleDockables.Count > 0)
            {
                IDockable nextActiveDockable = dock.VisibleDockables[indexActiveDockable];
                dock.ActiveDockable = nextActiveDockable is not IProportionalDockSplitter ? nextActiveDockable : null;
            }
            else
            {
                dock.ActiveDockable = null;
            }

            if (dock.VisibleDockables.Count == 1)
            {
                IDockable dockable0 = dock.VisibleDockables[0];
                if (dockable0 is IProportionalDockSplitter splitter0)
                {
                    RemoveDockable(splitter0, false);
                }
            }

            if (dock.VisibleDockables.Count == 2)
            {
                IDockable dockable0 = dock.VisibleDockables[0];
                IDockable dockable1 = dock.VisibleDockables[1];
                if (dockable0 is IProportionalDockSplitter splitter0)
                {
                    RemoveDockable(splitter0, false);
                }
                if (dockable1 is IProportionalDockSplitter splitter1)
                {
                    RemoveDockable(splitter1, false);
                }
            }

            if (collapse)
            {
                CollapseDock(dock);
            }
        }

        public virtual void RemoveWindow(IDockWindow window)
        {
            if (window.Owner is IRootDock rootDock)
            {
                window.Exit();
                rootDock.Windows?.Remove(window);
                OnWindowRemoved(window);
            }
        }

        public virtual void SetActiveDockable(IDockable dockable)
        {
            if (dockable.Owner is IDock dock)
            {
                dock.ActiveDockable = dockable;
            }
        }

        public virtual void SetFocusedDockable(IDock dock, IDockable dockable)
        {
            if (dock.ActiveDockable is not null && FindRoot(dock.ActiveDockable, x => x.IsFocusableRoot) is { } root)
            {
                if (root.FocusedDockable?.Owner is not null)
                {
                    SetIsActive(root.FocusedDockable.Owner, false);
                }

                if (dockable is not null)
                {
                    if (root.FocusedDockable != dockable)
                    {
                        root.FocusedDockable = dockable;
                    }
                }

                if (root.FocusedDockable?.Owner is not null)
                {
                    SetIsActive(root.FocusedDockable.Owner, true);
                }
            }
        }

        public void ShowTool<TTool>() where TTool : IMesTool => throw new NotImplementedException();

        public void ShowTool(IMesTool model) => throw new NotImplementedException();

        public void SplitToDock(IDock dock, IDockable dockable, DockOperation operation)
        {
            switch (operation)
            {
                case DockOperation.Left:
                case DockOperation.Right:
                case DockOperation.Top:
                case DockOperation.Bottom:
                    {
                        if (dock.Owner is IDock ownerDock && ownerDock.VisibleDockables is { })
                        {
                            Int32 index = ownerDock.VisibleDockables.IndexOf(dock);
                            if (index >= 0)
                            {
                                IDock layout = CreateSplitLayout(dock, dockable, operation);
                                ownerDock.VisibleDockables.RemoveAt(index);
                                OnDockableRemoved(dockable);
                                ownerDock.VisibleDockables.Insert(index, layout);
                                OnDockableAdded(dockable);
                                UpdateDockable(layout, ownerDock);
                                ownerDock.ActiveDockable = layout;
                            }
                        }
                    }
                    break;

                default:
                    throw new NotSupportedException($"Not supported split operation: {operation}.");
            }
        }

        public virtual void SplitToWindow(IDock dock, IDockable dockable, Double x, Double y, Double width, Double height)
        {
            IRootDock rootDock = FindRoot(dock, _ => true);
            if (rootDock is null)
            {
                return;
            }

            RemoveDockable(dockable, true);

            IDockWindow window = CreateWindowFrom(dockable);
            if (window is not null)
            {
                AddWindow(rootDock, window);
                window.X = x;
                window.Y = y;
                window.Width = width;
                window.Height = height;
                window.Present(false);
            }
        }

        public virtual void SwapDockable(IDock dock, IDockable sourceDockable, IDockable targetDockable)
        {
            if (dock.VisibleDockables is null)
            {
                return;
            }

            Int32 sourceIndex = dock.VisibleDockables.IndexOf(sourceDockable);
            Int32 targetIndex = dock.VisibleDockables.IndexOf(targetDockable);

            if (sourceIndex >= 0 && targetIndex >= 0 && sourceIndex != targetIndex)
            {
                IDockable originalSourceDockable = dock.VisibleDockables[sourceIndex];
                IDockable originalTargetDockable = dock.VisibleDockables[targetIndex];

                dock.VisibleDockables[targetIndex] = originalSourceDockable;
                OnDockableRemoved(originalTargetDockable);
                OnDockableAdded(originalSourceDockable);
                dock.VisibleDockables[sourceIndex] = originalTargetDockable;
                OnDockableAdded(originalTargetDockable);
                OnDockableSwapped(originalSourceDockable);
                OnDockableSwapped(originalTargetDockable);
                dock.ActiveDockable = originalTargetDockable;
            }
        }

        public void SwapDockable(IDock sourceDock, IDock targetDock, IDockable sourceDockable, IDockable targetDockable)
        {
            if (sourceDock.VisibleDockables is null || targetDock.VisibleDockables is null)
            {
                return;
            }

            Int32 sourceIndex = sourceDock.VisibleDockables.IndexOf(sourceDockable);
            Int32 targetIndex = targetDock.VisibleDockables.IndexOf(targetDockable);

            if (sourceIndex >= 0 && targetIndex >= 0)
            {
                IDockable originalSourceDockable = sourceDock.VisibleDockables[sourceIndex];
                IDockable originalTargetDockable = targetDock.VisibleDockables[targetIndex];
                sourceDock.VisibleDockables[sourceIndex] = originalTargetDockable;
                targetDock.VisibleDockables[targetIndex] = originalSourceDockable;

                UpdateDockable(originalSourceDockable, targetDock);
                UpdateDockable(originalTargetDockable, sourceDock);

                OnDockableSwapped(originalTargetDockable);
                OnDockableSwapped(originalSourceDockable);

                sourceDock.ActiveDockable = originalTargetDockable;
                targetDock.ActiveDockable = originalSourceDockable;
            }
        }

        public virtual void UpdateDockable(IDockable dockable, IDockable owner)
        {
            if (GetContext(dockable.Id) is { } context)
            {
                dockable.Context = context;
            }

            dockable.Owner = owner;

            if (dockable is IDock dock)
            {
                dock.Factory = this;

                if (dock.VisibleDockables is not null)
                {
                    foreach (IDockable child in dock.VisibleDockables)
                    {
                        UpdateDockable(child, dockable);
                    }
                }
            }

            if (dockable is IRootDock rootDock)
            {
                rootDock.Factory = this;
                if (rootDock.Windows is not null)
                {
                    foreach (IDockWindow child in rootDock.Windows)
                    {
                        UpdateDockWindow(child, dockable);
                    }
                }
            }
        }

        public virtual void UpdateDockWindow(IDockWindow window, IDockable owner)
        {
            window.Host = GetHostWindow(window.Id);
            if (window.Host is not null)
            {
                window.Host.Window = window;
            }

            window.Owner = owner;
            window.Factory = this;

            if (window.Layout is not null)
            {
                UpdateDockable(window.Layout, window.Layout.Owner);
            }
        }

        private void SetIsActive(IDockable dockable, Boolean active)
        {
            if (dockable is IDock dock)
            {
                dock.Factory = this;
                dock.IsActive = active;
            }
        }
    }
}