using Gtk;
using MinoriEditorShell.Platforms.Gtk.Views;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace SimpleDemo.Gtk
{
    public class MainWindow : MesGtkWindow
    {
        public MainWindow() : this(new Builder("SimpleDemo.Gtk.MainWindow.glade")) { }

        private MainWindow(Builder builder) :
            base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
        }
    }
}
