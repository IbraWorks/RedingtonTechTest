namespace RedingtonTechTest.Api.Strategy
{
    public class EitherCalculationStrategy : ICalculationStrategy
    {
        public float CalculateProbability(float firstValue, float secondValue)
        {
            return (firstValue + secondValue) - (firstValue * secondValue);
        }
    }
}
