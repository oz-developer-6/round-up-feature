namespace RoundUpFeature.Api.Helpers;

public static class Validators
{
    public static bool IsValidDate(string dateTimeString)
    {
        var isValid = DateTime.TryParse(dateTimeString, out _);

        return isValid;
    }
}