using FluentResults;
using Store.Application.Validators;

namespace Store.Application.Tests;
public class ErrorMessageHelpersTests
{
    [Fact]
    public void CreateErrorMessage_ShouldReturnCorrectMessage()
    {
        // Arrange
        var message = "Error occurred";
        var id = Guid.NewGuid();
        var errors = new List<IError>
        {
            new Error("First error"),
            new Error("Second error")
        };

        var expectedMessage = $"{message} {id} - reason: First error,Second error";

        // Act
        var result = ErrorMessageHelpers.CreateErrorMessage(message, id, errors);

        // Assert
        Assert.Equal( result,expectedMessage);
    }
}
