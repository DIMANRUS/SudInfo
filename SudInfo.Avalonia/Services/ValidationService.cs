namespace SudInfo.Avalonia.Services;
public class ValidationService : IValidationService
{
    public bool ValidationIp4(string ip4) =>
        Regex.IsMatch(ip4, @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
}