namespace Store.Application.Validators;
public static class ErrorMessageHelpers
{
    public static string CreateErrorMessage(string message, Guid id, IList<IError> errors)
    {
        return $"{message} {id} - reason: {string.Join(",", errors)}";
    }
}
