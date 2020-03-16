using System;
using MinoriEditorShell.Platforms.Gtk.Core;
using MinoriEditorShell.Platforms.Gtk.Views;
using SimpleDemo.Core;

namespace SimpleDemo.Gtk
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new MesGtkApplication<MesGtkSetup<App>, App, MainWindow>();
            app.Run();
        }
    }
}
