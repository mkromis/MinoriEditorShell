using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MinoriEditorShell.Platforms.Avalonia.Views;

namespace SimpleDemo.Avalonia
{
    public class App : MesApplication
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void RegisterSetup() { } //this.RegisterSetupType<MesAvaSetup<Core.App>>();
        
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
   }
}