#region File Description

//-----------------------------------------------------------------------------
// Copyright 2011, Nick Gravelyn.
// Licensed under the terms of the Ms-PL:
// http://www.microsoft.com/opensource/licenses.mspx#Ms-PL
//-----------------------------------------------------------------------------

#endregion File Description

using MinoriEditorShell.Platforms.Wpf.Win32;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
#warning Not Implemented
#if false
    /// <summary>
    /// A control that enables graphics rendering inside a WPF control through
    /// the use of a hosted child Hwnd.
    /// </summary>
    public abstract class MesHwndWrapper : HwndHost
    {
#region Fields

        // The name of our window class
        private const string WindowClass = "GraphicsDeviceControlHostWindowClass";

        // The HWND we present to when rendering
        private IntPtr _hWnd;

        // For holding previous hWnd focus
        private IntPtr _hWndPrev;

        // Track if the application has focus
        private bool _applicationHasFocus;

        // Track if the mouse is in the window
        private bool _mouseInWindow;

        // Track the previous mouse position
        private Point _previousPosition;

        // Track the mouse state
        private readonly MesHwndMouseState _mouseState = new MesHwndMouseState();

        // Tracking whether we've "capture" the mouse
        private bool _isMouseCaptured;

#endregion Fields

#region Events

        /// <summary>
        /// Invoked when the control receives a left mouse down message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndLButtonDown;

        /// <summary>
        /// Invoked when the control receives a left mouse up message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndLButtonUp;

        /// <summary>
        /// Invoked when the control receives a left mouse double click message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndLButtonDblClick;

        /// <summary>
        /// Invoked when the control receives a right mouse down message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndRButtonDown;

        /// <summary>
        /// Invoked when the control receives a right mouse up message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndRButtonUp;

        /// <summary>
        /// Invoked when the control receives a rigt mouse double click message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndRButtonDblClick;

        /// <summary>
        /// Invoked when the control receives a middle mouse down message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMButtonDown;

        /// <summary>
        /// Invoked when the control receives a middle mouse up message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMButtonUp;

        /// <summary>
        /// Invoked when the control receives a middle mouse double click message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMButtonDblClick;

        /// <summary>
        /// Invoked when the control receives a mouse down message for the first extra button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX1ButtonDown;

        /// <summary>
        /// Invoked when the control receives a mouse up message for the first extra button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX1ButtonUp;

        /// <summary>
        /// Invoked when the control receives a double click message for the first extra mouse button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX1ButtonDblClick;

        /// <summary>
        /// Invoked when the control receives a mouse down message for the second extra button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX2ButtonDown;

        /// <summary>
        /// Invoked when the control receives a mouse up message for the second extra button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX2ButtonUp;

        /// <summary>
        /// Invoked when the control receives a double click message for the first extra mouse button.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndX2ButtonDblClick;

        /// <summary>
        /// Invoked when the control receives a mouse move message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMouseMove;

        /// <summary>
        /// Invoked when the control first gets a mouse move message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMouseEnter;

        /// <summary>
        /// Invoked when the control gets a mouse leave message.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMouseLeave;

        /// <summary>
        /// Invoked when the control recieves a mouse wheel delta.
        /// </summary>
        public event EventHandler<MesHwndMouseEventArgs> HwndMouseWheel;

#endregion Events

#region Properties

        public new bool IsMouseCaptured
        {
            get { return _isMouseCaptured; }
        }

#endregion Properties

#region Construction and Disposal

        protected MesHwndWrapper()
        {
            // We must be notified of the application foreground status for our mouse input events
            Application.Current.Activated += OnApplicationActivated;
            Application.Current.Deactivated += OnApplicationDeactivated;

            // We use the CompositionTarget.Rendering event to trigger the control to draw itself
            CompositionTarget.Rendering += OnCompositionTargetRendering;

            // Check whether the application is active (it almost certainly is, at this point).
            if (Application.Current.Windows.Cast<Window>().Any(x => x.IsActive))
            {
                _applicationHasFocus = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            // Unhook all events.
            CompositionTarget.Rendering -= OnCompositionTargetRendering;
            if (Application.Current != null)
            {
                Application.Current.Activated -= OnApplicationActivated;
                Application.Current.Deactivated -= OnApplicationDeactivated;
            }

            base.Dispose(disposing);
        }

#endregion Construction and Disposal

#region Public Methods

        /// <summary>
        /// Captures the mouse, hiding it and trapping it inside the window bounds.
        /// </summary>
        /// <remarks>
        /// This method is useful for tooling scenarios where you only care about the mouse deltas
        /// and want the user to be able to continue interacting with the window while they move
        /// the mouse. A good example of this is rotating an object based on the mouse deltas where
        /// through capturing you can spin and spin without having the cursor leave the window.
        /// </remarks>
        public new void CaptureMouse()
        {
            // Don't do anything if the mouse is already captured
            if (_isMouseCaptured)
            {
                return;
            }

            NativeMethods.SetCapture(_hWnd);
            _isMouseCaptured = true;
        }

        /// <summary>
        /// Releases the capture of the mouse which makes it visible and allows it to leave the window bounds.
        /// </summary>
        public new void ReleaseMouseCapture()
        {
            // Don't do anything if the mouse is not captured
            if (!_isMouseCaptured)
            {
                return;
            }

            NativeMethods.ReleaseCapture();
            _isMouseCaptured = false;
        }

#endregion Public Methods

#region Graphics Device Control Implementation

        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            // Get the current width and height of the control
            int width = (int)ActualWidth;
            int height = (int)ActualHeight;

            // If the control has no width or no height, skip drawing since it's not visible
            if (width < 1 || height < 1)
            {
                return;
            }

            Render(_hWnd);
        }

        protected abstract void Render(IntPtr windowHandle);

        private void OnApplicationActivated(object sender, EventArgs e)
        {
            _applicationHasFocus = true;
        }

        private void OnApplicationDeactivated(object sender, EventArgs e)
        {
            _applicationHasFocus = false;
            ResetMouseState();

            if (_mouseInWindow)
            {
                _mouseInWindow = false;
                HwndMouseLeave?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
            }

            ReleaseMouseCapture();
        }

        private void ResetMouseState()
        {
            // We need to invoke events for any buttons that were pressed
            bool fireL = _mouseState.LeftButton == MouseButtonState.Pressed;
            bool fireM = _mouseState.MiddleButton == MouseButtonState.Pressed;
            bool fireR = _mouseState.RightButton == MouseButtonState.Pressed;
            bool fireX1 = _mouseState.X1Button == MouseButtonState.Pressed;
            bool fireX2 = _mouseState.X2Button == MouseButtonState.Pressed;

            // Update the state of all of the buttons
            _mouseState.LeftButton = MouseButtonState.Released;
            _mouseState.MiddleButton = MouseButtonState.Released;
            _mouseState.RightButton = MouseButtonState.Released;
            _mouseState.X1Button = MouseButtonState.Released;
            _mouseState.X2Button = MouseButtonState.Released;

            // Fire any events
            MesHwndMouseEventArgs args = new MesHwndMouseEventArgs(_mouseState);
            if (fireL)
            {
                HwndLButtonUp?.Invoke(this, args);
            }

            if (fireM)
            {
                HwndMButtonUp?.Invoke(this, args);
            }

            if (fireR)
            {
                HwndRButtonUp?.Invoke(this, args);
            }

            if (fireX1)
            {
                HwndX1ButtonUp?.Invoke(this, args);
            }

            if (fireX2)
            {
                HwndX2ButtonUp?.Invoke(this, args);
            }

            // The mouse is no longer considered to be in our window
            _mouseInWindow = false;
        }

#endregion Graphics Device Control Implementation

#region HWND Management

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            // Create the host window as a child of the parent
            _hWnd = CreateHostWindow(hwndParent.Handle);
            return new HandleRef(this, _hWnd);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            // Destroy the window and reset our hWnd value
            NativeMethods.DestroyWindow(hwnd.Handle);
            _hWnd = IntPtr.Zero;
        }

        /// <summary>
        /// Creates the host window as a child of the parent window.
        /// </summary>
        private IntPtr CreateHostWindow(IntPtr hWndParent)
        {
            // Register our window class
            RegisterWindowClass();

            // Create the window
            return NativeMethods.CreateWindowEx(0, WindowClass, "",
               NativeMethods.WS_CHILD | NativeMethods.WS_VISIBLE,
               0, 0, (int)Width, (int)Height, hWndParent, IntPtr.Zero, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Registers the window class.
        /// </summary>
        private void RegisterWindowClass()
        {
            NativeMethods.WNDCLASSEX wndClass = new NativeMethods.WNDCLASSEX();
            wndClass.cbSize = (uint)Marshal.SizeOf(wndClass);
            wndClass.hInstance = NativeMethods.GetModuleHandle(null);
            wndClass.lpfnWndProc = NativeMethods.DefaultWindowProc;
            wndClass.lpszClassName = WindowClass;
            wndClass.hCursor = NativeMethods.LoadCursor(IntPtr.Zero, NativeMethods.IDC_ARROW);

            NativeMethods.RegisterClassEx(ref wndClass);
        }

#endregion HWND Management

#region WndProc Implementation

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case NativeMethods.WM_MOUSEWHEEL:
                    if (_mouseInWindow)
                    {
                        int delta = NativeMethods.GetWheelDeltaWParam(wParam.ToInt32());
                        HwndMouseWheel?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, delta, 0));
                    }
                    break;

                case NativeMethods.WM_LBUTTONDOWN:
                    _mouseState.LeftButton = MouseButtonState.Pressed;
                    HwndLButtonDown?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_LBUTTONUP:
                    _mouseState.LeftButton = MouseButtonState.Released;
                    HwndLButtonUp?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_LBUTTONDBLCLK:
                    HwndLButtonDblClick?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, MouseButton.Left));
                    break;

                case NativeMethods.WM_RBUTTONDOWN:
                    _mouseState.RightButton = MouseButtonState.Pressed;
                    HwndRButtonDown?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_RBUTTONUP:
                    _mouseState.RightButton = MouseButtonState.Released;
                    HwndRButtonUp?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_RBUTTONDBLCLK:
                    HwndRButtonDblClick?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, MouseButton.Right));
                    break;

                case NativeMethods.WM_MBUTTONDOWN:
                    _mouseState.MiddleButton = MouseButtonState.Pressed;
                    HwndMButtonDown?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_MBUTTONUP:
                    _mouseState.MiddleButton = MouseButtonState.Released;
                    HwndMButtonUp?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    break;

                case NativeMethods.WM_MBUTTONDBLCLK:
                    HwndMButtonDblClick?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, MouseButton.Middle));
                    break;

                case NativeMethods.WM_XBUTTONDOWN:
                    if (((int)wParam & NativeMethods.MK_XBUTTON1) != 0)
                    {
                        _mouseState.X1Button = MouseButtonState.Pressed;
                        HwndX1ButtonDown?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    }
                    else if (((int)wParam & NativeMethods.MK_XBUTTON2) != 0)
                    {
                        _mouseState.X2Button = MouseButtonState.Pressed;
                        HwndX2ButtonDown?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    }
                    break;

                case NativeMethods.WM_XBUTTONUP:
                    if (((int)wParam & NativeMethods.MK_XBUTTON1) != 0)
                    {
                        _mouseState.X1Button = MouseButtonState.Released;
                        HwndX1ButtonUp?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    }
                    else if (((int)wParam & NativeMethods.MK_XBUTTON2) != 0)
                    {
                        _mouseState.X2Button = MouseButtonState.Released;
                        HwndX2ButtonUp?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    }
                    break;

                case NativeMethods.WM_XBUTTONDBLCLK:
                    if (((int)wParam & NativeMethods.MK_XBUTTON1) != 0)
                    {
                        HwndX1ButtonDblClick?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, MouseButton.XButton1));
                    }
                    else if (((int)wParam & NativeMethods.MK_XBUTTON2) != 0)
                    {
                        HwndX2ButtonDblClick?.Invoke(this, new MesHwndMouseEventArgs(_mouseState, MouseButton.XButton2));
                    }

                    break;

                case NativeMethods.WM_MOUSEMOVE:
                    // If the application isn't in focus, we don't handle this message
                    if (!_applicationHasFocus)
                    {
                        break;
                    }

                    // record the prevous and new position of the mouse
                    _mouseState.ScreenPosition = PointToScreen(new Point(
                        NativeMethods.GetXLParam((int)lParam),
                        NativeMethods.GetYLParam((int)lParam)));

                    if (!_mouseInWindow)
                    {
                        _mouseInWindow = true;

                        HwndMouseEnter?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));

                        // Track the previously focused window, and set focus to this window.
                        _hWndPrev = NativeMethods.GetFocus();
                        NativeMethods.SetFocus(_hWnd);

                        // send the track mouse event so that we get the WM_MOUSELEAVE message
                        NativeMethods.TRACKMOUSEEVENT tme = new NativeMethods.TRACKMOUSEEVENT
                        {
                            cbSize = Marshal.SizeOf(typeof(NativeMethods.TRACKMOUSEEVENT)),
                            dwFlags = NativeMethods.TME_LEAVE,
                            hWnd = hwnd
                        };
                        NativeMethods.TrackMouseEvent(ref tme);
                    }

                    if (_mouseState.ScreenPosition != _previousPosition)
                    {
                        HwndMouseMove?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    }

                    _previousPosition = _mouseState.ScreenPosition;

                    break;

                case NativeMethods.WM_MOUSELEAVE:

                    // If we have capture, we ignore this message because we're just
                    // going to reset the cursor position back into the window
                    if (_isMouseCaptured)
                    {
                        break;
                    }

                    // Reset the state which releases all buttons and
                    // marks the mouse as not being in the window.
                    ResetMouseState();
                    HwndMouseLeave?.Invoke(this, new MesHwndMouseEventArgs(_mouseState));
                    NativeMethods.SetFocus(_hWndPrev);

                    break;
            }

            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }

#endregion WndProc Implementation
    }
#endif
}