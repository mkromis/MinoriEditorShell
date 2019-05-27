using System;
using System.Windows.Media.Effects;

namespace MinoriEditorStudio.Framework.ShaderEffects
{
    public class ShaderEffectBase<T> : ShaderEffect, IDisposable
        where T : ShaderEffectBase<T>
    {
        [ThreadStatic]
        private static PixelShader _shader;

        private static PixelShader Shader => _shader ?? (_shader = ShaderEffectUtility.GetPixelShader(typeof(T).Name));

        protected ShaderEffectBase() => PixelShader = Shader;

        void IDisposable.Dispose() => PixelShader = null;
    }
}
