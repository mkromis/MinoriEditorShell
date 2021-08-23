using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    /// <summary>
    /// Mes Avalonia helper class
    /// </summary>
    public class MesAvnView : UserControl, IMesAvnView, IDisposable
    {
        private IMvxViewModel _viewModel;
        private IMvxBindingContext _bindingContext;

        /// <summary>
        /// Interface to view model.
        /// </summary>
        public IMvxViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
                BindingContext.DataContext = value;
            }
        }

        /// <summary>
        /// Get binding context
        /// </summary>
        public IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext != null)
                    return _bindingContext;

                if (Mvx.IoCProvider != null)
                    this.CreateBindingContext();

                return _bindingContext;
            }
            set => _bindingContext = value;
        }

        /// <summary>
        /// Default constructor for class
        /// </summary>
        public MesAvnView()
        {
            DetachedFromVisualTree += MvxWpfView_Unloaded;
            AttachedToVisualTree += MvxWpfView_Loaded;
        }

        private void MvxWpfView_Unloaded(Object sender, VisualTreeAttachmentEventArgs e) 
        {
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }

        private void MvxWpfView_Loaded(Object sender, VisualTreeAttachmentEventArgs e)
        { 
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        /// <summary>
        /// Standard dispose pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// deconstructor
        /// </summary>
        ~MesAvnView()
        {
            Dispose(false);
        }

        /// <summary>
        /// Standard disposable pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                DetachedFromVisualTree -= MvxWpfView_Unloaded;
                AttachedToVisualTree -= MvxWpfView_Loaded;
            }
        }
    }

    /// <summary>
    /// Helper class for 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class MesAvnView<TViewModel> : MesAvnView, IMesAvnView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        /// <summary>
        /// View model interface
        /// </summary>
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        /// <summary>
        /// Helper to create binding set
        /// </summary>
        /// <returns></returns>
        public MvxFluentBindingDescriptionSet<IMesAvnView<TViewModel>, TViewModel> CreateBindingSet() => 
            this.CreateBindingSet<IMesAvnView<TViewModel>, TViewModel>();
    }
}
