using System.Windows;
using System.Windows.Media;

namespace MinoriEditorShell.Platforms.Wpf.ShaderEffects
{
#warning Not Implemented
#if false
    public class MesGrayscaleEffect : MesShaderEffectBase<MesGrayscaleEffect>
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(MesGrayscaleEffect), 0);

        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public MesGrayscaleEffect()
        {
            UpdateShaderValue(InputProperty);
        }
    }
#endif
}