using MinoriEditorShell.Platforms.Avalonia.Win32;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows;
// using System.Windows.Interop;
// using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Avalonia.Controls
{
    // public class MesClippingHwndHost : HwndHost
    // {
    //     private HwndSource _source;

    //     public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
    //         "Content", typeof(Visual), typeof(MesClippingHwndHost),
    //         new PropertyMetadata(OnContentChanged));

    //     private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //     {
    //         MesClippingHwndHost hwndHost = (MesClippingHwndHost)d;

    //         if (e.OldValue != null)
    //         {
    //             if (hwndHost._source != null)
    //             {
    //                 hwndHost._source.RootVisual = null;
    //             }

    //             hwndHost.RemoveLogicalChild(e.OldValue);
    //         }

    //         if (e.NewValue != null)
    //         {
    //             hwndHost.AddLogicalChild(e.NewValue);
    //             if (hwndHost._source != null)
    //             {
    //                 hwndHost._source.RootVisual = (Visual)e.NewValue;
    //             }
    //         }
    //     }

    //     public Visual Content
    //     {
    //         get => (Visual)GetValue(ContentProperty);
    //         set => SetValue(ContentProperty, value);
    //     }

    //     protected override IEnumerator LogicalChildren
    //     {
    //         get
    //         {
    //             if (Content != null)
    //             {
    //                 yield return Content;
    //             }
    //         }
    //     }

    //     protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    //     {
    //         HwndSourceParameters param = new HwndSourceParameters("MinoriEditorStudioClippingHwndHost", (Int32)Width, (Int32)Height)
    //         {
    //             ParentWindow = hwndParent.Handle,
    //             WindowStyle = NativeMethods.WS_VISIBLE | NativeMethods.WS_CHILD,
    //         };

    //         _source = new HwndSource(param)
    //         {
    //             RootVisual = Content
    //         };

    //         return new HandleRef(null, _source.Handle);
    //     }

    //     protected override void DestroyWindowCore(HandleRef hwnd)
    //     {
    //         _source.Dispose();
    //         _source = null;
    //     }

    //     protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    //     {
    //         // If we don't do this, HwndHost doesn't seem to pick up on all size changes.
    //         UpdateWindowPos();

    //         base.OnRenderSizeChanged(sizeInfo);
    //     }
    //}
}