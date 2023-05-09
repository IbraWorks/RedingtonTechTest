using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RedingtonTechTest.Api.Commands;
using RedingtonTechTest.Api.Controllers;
using RedingtonTechTest.Api.Requests;
using RedingtonTechTest.Api.Responses;
using RedingtonTechTest.Api.Validators;

namespace RedingtonTechTest.Api.Tests
{
    public class ProbabilitiesControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly ProbabilitiesController _sut;

        public ProbabilitiesControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _sut = new ProbabilitiesController(_mediator.Object);
        }

        [Fact]
        public async Task GetProbability_WithValidRequest_ReturnsOkResultWithResponse()
        {
            var request = new CalculateProbabilityRequest
            {
                FirstValue = 0.5f,
                SecondValue = 0.5f,
                CalculationType = 0
            };

            var response = new CalculateProbabilityResponse
            {
                Result = 0.25f
            };

            _mediator.Setup(_ => _.Send(It.IsAny<CalculateProbabilityCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _sut.GetProbability(request);

            result.Should().BeOfType<ActionResult<float>>();
            result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK);
            var valueResult = result.Result as OkObjectResult;
            float peopleResult = (float)valueResult?.Value;
            peopleResult.Should().Be(response.Result);
        }


        [Fact]
        public async Task GetProbability_WithInValidRequest_FailsValidation()
        {
            var request = new CalculateProbabilityRequest
            {
                FirstValue = 0.5f,
                SecondValue = 0.5f,
                CalculationType = 2
            };

            var response = new CalculateProbabilityResponse
            {
                Result = 0.25f
            };

            var validator = new RedingtonProbabilityPairValidator();

            // Act
            var validationResult = validator.Validate(request);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle();
            validationResult.Errors.Should().Contain(e => e.PropertyName == nameof(CalculateProbabilityRequest.CalculationType));
        }
    }

}
