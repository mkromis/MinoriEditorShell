using System;
using MvvmCross.Base;
using MvvmCross.Converters;
using MvvmCross.Binding;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Combiners;
using MvvmCross.IoC;
using MinoriEditorShell.Platforms.Avalonia.Binding.MesBinding;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Avalonia.Binding
{
    public class MesWindowsBindingBuilder : MvxBindingBuilder
    {
        public enum BindingType
        {
            Windows,
            MvvmCross
        }

        private readonly BindingType _bindingType;

        public MesWindowsBindingBuilder(
            BindingType bindingType = BindingType.MvvmCross)
        {
            _bindingType = bindingType;
        }

        public override void DoRegistration(IMvxIoCProvider iocProvider)
        {
            base.DoRegistration(iocProvider);
            InitializeBindingCreator();
        }

        protected override void RegisterBindingFactories(IMvxIoCProvider iocProvider)
        {
            switch (_bindingType)
            {
                case BindingType.Windows:
                    // no need for MvvmCross binding factories - so don't create them
                    break;

                case BindingType.MvvmCross:
                    base.RegisterBindingFactories(iocProvider);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override IMvxTargetBindingFactoryRegistry CreateTargetBindingRegistry()
        {
            switch (_bindingType)
            {
                case BindingType.Windows:
                    return base.CreateTargetBindingRegistry();

                case BindingType.MvvmCross:
                    return new MesWindowsTargetBindingFactoryRegistry();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitializeBindingCreator()
        {
            var creator = CreateBindingCreator();
            Mvx.IoCProvider.RegisterSingleton(creator);
        }

        protected virtual IMesBindingCreator CreateBindingCreator()
        {
            switch (_bindingType)
            {
                //case BindingType.Windows:
                //    return new MesWindowsBindingCreator();

                case BindingType.MvvmCross:
                    return new MesMvvmCrossBindingCreator();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            if (MvxSingleton<IMesWindowsAssemblyCache>.Instance != null)
            {
                foreach (var assembly in MvxSingleton<IMesWindowsAssemblyCache>.Instance.Assemblies)
                {
                    registry.Fill(assembly);
                }
            }
        }

        protected override void FillValueCombiners(IMvxValueCombinerRegistry registry)
        {
            base.FillValueCombiners(registry);

            if (MvxSingleton<IMesWindowsAssemblyCache>.Instance != null)
            {
                foreach (var assembly in MvxSingleton<IMesWindowsAssemblyCache>.Instance.Assemblies)
                {
                    registry.Fill(assembly);
                }
            }
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
#warning fix FillTargetFactories
            //registry.RegisterCustomBindingFactory<FrameworkElement>(
            //    MesWindowsPropertyBinding.FrameworkElement_Visible,
            //    view => new MvxVisibleTargetBinding(view));

            //registry.RegisterCustomBindingFactory<FrameworkElement>(
            //    MesWindowsPropertyBinding.FrameworkElement_Collapsed,
            //    view => new MvxCollapsedTargetBinding(view));

            //registry.RegisterCustomBindingFactory<FrameworkElement>(
            //    MesWindowsPropertyBinding.FrameworkElement_Hidden,
            //    view => new MvxCollapsedTargetBinding(view));

            base.FillTargetFactories(registry);
        }
    }
}
