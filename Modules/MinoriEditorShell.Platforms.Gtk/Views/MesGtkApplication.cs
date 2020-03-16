using System;
using System.Reflection;
using Gtk;
using MinoriEditorShell.Platforms.Gtk.Core;
using MinoriEditorShell.Services;
using MvvmCross.Core;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Platforms.Gtk.Views
{
    public class MesGtkApplication : Application
    {
        public static MesGtkApplication Instance { get; private set; }

        public MesGtkApplication() : base(
            Assembly.GetCallingAssembly().GetName().Name,
            GLib.ApplicationFlags.None)
        {
            Instance = this;
            Init();
            RegisterSetup();
        }


        public new void Run()
        {
            Application.Run();
        }

        ~MesGtkApplication()
        {
            Quit();
        }

        protected virtual void RegisterSetup() { }
    }

    /// <summary>
    /// Main setup for Gtk+ applications with passing window type
    /// </summary>
    public class MesGtkApplication<TMesGtkSetup, TApplication, TWindow> : MesGtkApplication
        where TMesGtkSetup : MesGtkSetup<TApplication>, new()
        where TApplication : class, IMvxApplication, new()
        where TWindow : MesGtkWindow, new()
    {
        public MesGtkApplication()
        {
            TWindow window = new TWindow();
            window.Show();
        }

        protected override void RegisterSetup() =>
            this.RegisterSetupType<TMesGtkSetup>();
    }
}
