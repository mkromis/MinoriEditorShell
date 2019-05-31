using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriDemo.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public double TipAmount(double subTotal, int generosity) => subTotal * ((double)generosity) / 100.0;
    }
}
