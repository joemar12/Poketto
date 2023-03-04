using AutoFixture;
using FluentAssertions;
using FluentValidation.Results;
using Poketto.Application.Common;
using Poketto.Application.Common.Exceptions;
using Poketto.Application.GraphQL.Errors;
using Xunit;

namespace Poketto.Tests.UnitTests.GraphQL.Errors;

public class ErrorFactoryTests
{
    private readonly IFixture _fixture;

    public ErrorFactoryTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void AuthorizationError_CanBeCreatedFrom_AuthorizationException()
    {
        var exception = _fixture.Create<AuthorizationException>();
        var resultError = AuthorizationError.CreateErrorFrom(exception);

        resultError.Should().NotBeNull();
        resultError!.Message.Should().Be(exception.Message);
        resultError!.Code.Should().Be(exception.Code);
    }
    [Fact]
    public void InternalServerError_CanBeCreatedFrom_Exception()
    {
        var exception = _fixture.Create<Exception>();
        var resultError = InternalServerError.CreateErrorFrom(exception);

        resultError.Should().NotBeNull();
        resultError!.Message.Should().Be(exception.Message);
        resultError!.Code.Should().Be(ErrorCodes.InternalServerError);
    }
    [Fact]
    public void NotFoundError_CanBeCreatedFrom_NotFoundException()
    {
        var exception = _fixture.Create<NotFoundException>();
        var resultError = NotFoundError.CreateErrorFrom(exception);

        resultError.Should().NotBeNull();
        resultError!.Message.Should().Be(exception.Message);
        resultError!.Code.Should().Be(ErrorCodes.NotFound);
    }
    [Fact]
    public void ValidationError_CanBeCreatedFrom_ValidationException()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Age", "must be 18 or older"),
            new ValidationFailure("Age", "must be 25 or younger"),
            new ValidationFailure("Password", "must contain at least 8 characters"),
            new ValidationFailure("Password", "must contain a digit"),
            new ValidationFailure("Password", "must contain upper case letter"),
            new ValidationFailure("Password", "must contain lower case letter"),
        };
        var exception = new ValidationException(failures);
        var resultError = ValidationError.CreateErrorFrom(exception);

        resultError.Should().NotBeNull();
        resultError!.Message.Should().Be(exception.Message);
        resultError!.Code.Should().Be(ErrorCodes.ValidationFailed);
        resultError.Failures.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" }); ;

        resultError.Failures["Age"].Should().BeEquivalentTo(new string[]
        {
            "must be 25 or younger",
            "must be 18 or older",
        });

        resultError.Failures["Password"].Should().BeEquivalentTo(new string[]
        {
            "must contain lower case letter",
            "must contain upper case letter",
            "must contain at least 8 characters",
            "must contain a digit",
        });
    }
}
