namespace RedingtonTechTest.Api.Strategy
{
    public class CalculationStrategyFactory : ICalculationStrategyFactory
    {
        public ICalculationStrategy CreateCalculationStrategy(int strategy)
        {
            switch (strategy)
            {
                case 0:
                    return new CombinedWithStrategy();
                case 1:
                    return new EitherCalculationStrategy();
                default:
                    throw new ArgumentException("Invalid calculation type");
            }
        }
    }
}
