using MinoriEditorShell.Services;
using System;

namespace MinoriEditorShell.Platforms.Gtk.Views
{
    public class MesGtkWindow : global::Gtk.Window, IMesWindow
    {
        public MesGtkWindow() : base(global::Gtk.WindowType.Toplevel) { }

        public MesGtkWindow(IntPtr ptr) : base(ptr) { }

        public String DisplayName {
            get => Title;
            set => Title = value;
        }
    }
}