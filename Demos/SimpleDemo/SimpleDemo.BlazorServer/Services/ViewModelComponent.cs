using Microsoft.AspNetCore.Components;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDemo.BlazorServer.Services
{
    public abstract class ViewModelComponent<T> : ComponentBase where T : MvxViewModel
    {
        [Inject] 
        public T ViewModel { get; set; }

        protected override Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += (o, e) => OnViewModelUpdate();
            return base.OnInitializedAsync();
        }
        protected virtual void OnViewModelUpdate()
        {
            StateHasChanged();
        }
    }
}
