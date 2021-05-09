using Microsoft.AspNetCore.Components;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimpleDemo.BlazorServer.Services
{
    /// <summary>
    /// Helper for MvxViewModel support
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="https://bartecki.me/blog/mvvm-in-blazor"/>
    public abstract class ViewModelComponent<T> : ComponentBase where T : MvxViewModel
    {
        [Inject] 
        public T ViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += (o, e) => StateHasChanged();
            await ViewModel.Initialize(); 
            await base.OnInitializedAsync();
        }
    }
}
