using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using Moq;
using Poketto.Application.Accounts.Commands;
using Poketto.Application.Common.Behaviours;
using Poketto.Application.Common.Interfaces;
using Xunit;

namespace Poketto.Tests.UnitTests.Behaviours
{
    public class LoggerBehaviourTests
    {
        private readonly Mock<ILogger<AddAccountCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly IFixture _fixture;

        public LoggerBehaviourTests()
        {
            _logger = new Mock<ILogger<AddAccountCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public async Task ShouldCall_GetCurrentUser()
        {
            var userId = _fixture.Create<string>();
            var command = _fixture.Create<AddAccountCommand>();
            _currentUserService.Setup(x => x.GetCurrentUser()).Returns(userId);

            var requestLogger = new LoggingBehaviour<AddAccountCommand>(_logger.Object, _currentUserService.Object);

            await requestLogger.Process(command, new CancellationToken());

            _currentUserService.Verify(i => i.GetCurrentUser(), Times.Once);
        }
    }
}
