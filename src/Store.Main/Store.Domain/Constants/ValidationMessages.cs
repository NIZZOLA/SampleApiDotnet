namespace Store.Domain.Constants;
public static class ValidationMessages
{
    public const string IdRequired = "Id is required.";
    public const string IdInvalid = "Id must be alphanumeric and can contain hyphens.";

    public const string NameRequired = "Name is required.";
    public const string NameTooLong = "Name must be up to 50 characters long.";

    public const string PhoneRequired = "Phone is required.";
    public const string PhoneTooLong = "Phone must be up to 15 characters long.";

    public const string EmailRequired = "Email is required.";
    public const string EmailTooLong = "Email must be up to 100 characters long.";
    public const string EmailInvalid = "Email must be a valid email address.";
}