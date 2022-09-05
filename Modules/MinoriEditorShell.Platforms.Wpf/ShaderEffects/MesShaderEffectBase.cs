using System;
using System.Windows.Media.Effects;

namespace MinoriEditorShell.Platforms.Wpf.ShaderEffects
{
#warning not implemented
#if false
    public class MesShaderEffectBase<T> : ShaderEffect, IDisposable
        where T : MesShaderEffectBase<T>
    {
        [ThreadStatic]
        private static PixelShader _shader;

        private static PixelShader Shader => _shader ?? (_shader = MesShaderEffectUtility.GetPixelShader(typeof(T).Name));

        protected MesShaderEffectBase() => PixelShader = Shader;

        void IDisposable.Dispose() => PixelShader = null;
    }
#endif
}