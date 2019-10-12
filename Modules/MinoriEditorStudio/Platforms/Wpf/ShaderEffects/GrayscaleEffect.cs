using System.Windows;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.ShaderEffects
{
    public class GrayscaleEffect : ShaderEffectBase<GrayscaleEffect>
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0);

        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public GrayscaleEffect()
        {
            UpdateShaderValue(InputProperty);
        }
    }
}
