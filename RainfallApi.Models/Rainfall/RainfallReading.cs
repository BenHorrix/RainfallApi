namespace RainfallApi.Models.Rainfall
{
    public class RainfallReading
    {
        public enum Source
        {
            Government = 0,
            UserProvided = 1
        }

        public DateTime DateMeasured { get; }

        public decimal AmountMeasured { get; }

        public Source ValueSource { get; }

        public RainfallReading(DateTime dateMeasured, decimal amountMeasured, Source valueSource)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
            ValueSource = valueSource;
        }
    }
}
