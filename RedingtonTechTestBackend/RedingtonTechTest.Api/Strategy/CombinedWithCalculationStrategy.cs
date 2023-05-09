namespace RedingtonTechTest.Api.Strategy
{
    public class CombinedWithStrategy : ICalculationStrategy
    {
        public float CalculateProbability(float firstValue, float secondValue)
        {
            return firstValue * secondValue;
        }
    }
}
