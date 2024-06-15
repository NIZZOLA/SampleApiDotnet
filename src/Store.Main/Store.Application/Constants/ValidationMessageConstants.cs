namespace Store.Application.Constants;
public static class ValidationMessageConstants
{
    public const string IdRequired = "Id is required.";
    public const string StoreNameRequired = "StoreName is required.";
    public const string StoreNameMaxLength = "StoreName must be less than or equal to {0} characters.";
    public const string PhoneRequired = "Phone is required.";
    public const string PhoneMaxLength = "Phone must be less than or equal to 15 characters.";
    public const string EmailRequired = "Email is required.";
    public const string EmailMaxLength = "Email must be less than or equal to 100 characters.";
}
