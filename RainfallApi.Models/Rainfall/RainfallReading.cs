namespace RainfallApi.Models.Rainfall
{
    public class RainfallReading
    {
        public DateTime DateMeasured { get; }
        public decimal AmountMeasured { get; }

        public RainfallReading(DateTime dateMeasured, decimal amountMeasured)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
        }
    }
}
