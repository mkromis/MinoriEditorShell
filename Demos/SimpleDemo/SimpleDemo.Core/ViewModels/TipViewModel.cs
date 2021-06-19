using MinoriEditorShell.Services;
using SimpleDemo.Core.Services;
using System.Threading.Tasks;

namespace SimpleDemo.Core.ViewModels
{
    public class TipViewModel : MesDocument
    {
        private readonly ICalculationService _calculationService;
        private int _generosity;
        private double _subTotal;
        private double _tip;

        public TipViewModel(ICalculationService calculationService)
        {
            _calculationService = calculationService;

            DisplayName = "TipCalc Sample";
        }

        public int Generosity
        {
            get => _generosity;
            set
            {
                _generosity = value;
                RaisePropertyChanged(() => Generosity);

                Recalculate();
            }
        }

        public double SubTotal
        {
            get => _subTotal;
            set
            {
                _subTotal = value;
                RaisePropertyChanged(() => SubTotal);

                Recalculate();
            }
        }

        public double Tip
        {
            get => _tip;
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _subTotal = 100;
            _generosity = 10;

            Recalculate();
        }

        private void Recalculate()
        {
            Tip = _calculationService.TipAmount(SubTotal, Generosity);
        }
    }
}