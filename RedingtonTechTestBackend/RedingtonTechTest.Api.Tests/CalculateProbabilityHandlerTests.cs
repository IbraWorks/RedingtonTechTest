using FluentAssertions;
using Moq;
using RedingtonTechTest.Api.Commands;
using RedingtonTechTest.Api.Strategy;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonTechTest.Api.Tests
{
    public class CalculateProbabilityHandlerTests
    {
        private readonly Mock<ILogger> _logger;
        private readonly Mock<ICalculationStrategyFactory> _calcStrategyFactoryMock;
        private readonly CalculateProbabilityCommandHandler _sut;

        public CalculateProbabilityHandlerTests()
        {
            _logger = new Mock<ILogger>();
            _calcStrategyFactoryMock = new Mock<ICalculationStrategyFactory>();
            _sut = new CalculateProbabilityCommandHandler(_calcStrategyFactoryMock.Object, _logger.Object);
        }

        [Fact]
        public async Task GivenValidCommand_WhenGivenCombinedCalc_ReturnsCorrectCalculateProbabilityResponse()
        {
            var calculationStrategyFactoryMock = new Mock<ICalculationStrategyFactory>();
            calculationStrategyFactoryMock.Setup(f => f.CreateCalculationStrategy(It.IsAny<int>()))
                .Returns(new CombinedWithStrategy());

            var loggerMock = new Mock<ILogger>();

            var handler = new CalculateProbabilityCommandHandler(calculationStrategyFactoryMock.Object, loggerMock.Object);
            var command = new CalculateProbabilityCommand(0.5f, 0.5f, 0);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Result.Should().Be(0.25f);
        }

        [Fact]
        public async Task GivenValidCommand_WhenGivenEitherCalc_ReturnsCorrectCalculateProbabilityResponse()
        {
            var calculationStrategyFactoryMock = new Mock<ICalculationStrategyFactory>();
            calculationStrategyFactoryMock.Setup(f => f.CreateCalculationStrategy(It.IsAny<int>()))
                .Returns(new EitherCalculationStrategy());

            var loggerMock = new Mock<ILogger>();

            var handler = new CalculateProbabilityCommandHandler(calculationStrategyFactoryMock.Object, loggerMock.Object);
            var command = new CalculateProbabilityCommand(0.5f, 0.5f, 1);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Result.Should().Be(0.75f);
        }
    }
}
