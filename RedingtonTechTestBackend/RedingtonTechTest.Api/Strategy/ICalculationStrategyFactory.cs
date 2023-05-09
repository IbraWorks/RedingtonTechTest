namespace RedingtonTechTest.Api.Strategy
{
    public interface ICalculationStrategyFactory
    {
        ICalculationStrategy CreateCalculationStrategy(int strategy);
    }
}
