namespace SimpleDemo.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public double TipAmount(double subTotal, int generosity) => subTotal * ((double)generosity) / 100.0;
    }
}