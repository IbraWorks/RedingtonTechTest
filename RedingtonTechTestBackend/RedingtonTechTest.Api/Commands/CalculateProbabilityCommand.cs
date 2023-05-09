using MediatR;
using RedingtonTechTest.Api.Requests;
using RedingtonTechTest.Api.Responses;
using RedingtonTechTest.Api.Strategy;
using ILogger = Serilog.ILogger;

namespace RedingtonTechTest.Api.Commands
{
    public class CalculateProbabilityCommand : IRequest<CalculateProbabilityResponse>
    {
        public CalculateProbabilityCommand(float firstValue, float secondValue, int calculationType)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            CalculationType = calculationType;
        }

        public float FirstValue { get; }
        public float SecondValue { get; }
        public int CalculationType { get; }
    }

    public class CalculateProbabilityCommandHandler : IRequestHandler<CalculateProbabilityCommand, CalculateProbabilityResponse>
    {
        private readonly ICalculationStrategyFactory _calculationStrategyFactory;
        private readonly ILogger _logger;

        public CalculateProbabilityCommandHandler(ICalculationStrategyFactory calculationStrategyFactory, ILogger logger)
        {
            _calculationStrategyFactory = calculationStrategyFactory;
            _logger = logger;
        }

        public Task<CalculateProbabilityResponse> Handle(CalculateProbabilityCommand request, CancellationToken cancellationToken)
        {

            var calculationStrategy = _calculationStrategyFactory.CreateCalculationStrategy(request.CalculationType);
            var result = calculationStrategy.CalculateProbability(request.FirstValue, request.SecondValue);
            
            var calcType = Enum.GetName(typeof(RedingtonCalculationTypes), request.CalculationType);
            _logger.Information("Using calc strategy {calcStrat}, Calculated result {res} using inputs {first} and {second}", calcType, result, request.FirstValue, request.SecondValue);
            var response = new CalculateProbabilityResponse()
            {
                Result = result
            };
            return Task.FromResult(response);
        }
    }
}
