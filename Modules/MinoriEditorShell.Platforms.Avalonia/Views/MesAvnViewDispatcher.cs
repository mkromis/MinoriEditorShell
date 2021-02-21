using Avalonia.Threading;
using MinoriEditorShell.Platforms.Avalonia.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public class MesAvnViewDispatcher
        : MesAvnUIThreadDispatcher, IMvxViewDispatcher
    {
        private readonly IMesAvnViewPresenter _presenter;

        public MesAvnViewDispatcher(Dispatcher dispatcher, IMesAvnViewPresenter presenter)
            : base(dispatcher)
        {
            _presenter = presenter;
        }

        public async Task<bool> ShowViewModel(MvxViewModelRequest request)
        {
            await ExecuteOnMainThreadAsync(() => _presenter.Show(request));
            return true;
        }

        public async Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            await ExecuteOnMainThreadAsync(() => _presenter.ChangePresentation(hint));
            return true;
        }
    }
}
