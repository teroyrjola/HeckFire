using HeckFire;

namespace Calculator.Models
{
    public class CalculationResult
    {
        public string ResultString { get; set; }

        public CalculationResult(Quest q)
        {
            this.ResultString = q.Name();
        }

        public CalculationResult(string s)
        {
            this.ResultString = s;
        }
    }
}