namespace AvgCalc.Contract
{
    public sealed class CalculationResponse : CalculationRequestBase
    {
        public double Result { get; set; }
    }
}