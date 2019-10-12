using System;
using System.Windows.Media.Effects;

namespace MinoriEditorShell.Platforms.Wpf.ShaderEffects
{
    internal static class ShaderEffectUtility
    {
        public static PixelShader GetPixelShader(string name) => new PixelShader
        {
            UriSource = new Uri($@"pack://application:,,,/MinoriEditorStudio;component/Framework/ShaderEffects/{name}.ps")
        };
    }
}
