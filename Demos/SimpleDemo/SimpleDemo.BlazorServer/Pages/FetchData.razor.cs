using Microsoft.AspNetCore.Components;
using SimpleDemo.BlazorServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDemo.BlazorServer.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[] forecasts;

        [Inject] 
        WeatherForecastService ForecastService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
