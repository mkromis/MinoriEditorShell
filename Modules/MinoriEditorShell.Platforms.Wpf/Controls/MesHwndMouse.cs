using MinoriEditorShell.Platforms.Wpf.Win32;
using System;
using System.Windows;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
    public static class MesHwndMouse
    {
        public static Point GetCursorPosition()
        {
            NativeMethods.NativePoint point = new NativeMethods.NativePoint();
            NativeMethods.GetCursorPos(ref point);
            return new Point(point.X, point.Y);
        }

        public static void SetCursorPosition(Point point) => NativeMethods.SetCursorPos((Int32)point.X, (Int32)point.Y);

        public static void ShowCursor() => NativeMethods.ShowCursor(true);

        public static void HideCursor() => NativeMethods.ShowCursor(false);
    }
}