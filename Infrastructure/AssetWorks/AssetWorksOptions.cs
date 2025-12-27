namespace RecallsIntegration.Api.Infrastructure.AssetWorks;

public sealed class AssetWorksOptions
{
    public string BaseUrl { get; set; } = "";
    public string ApiKey { get; set; } = "";
    public string? Tenant { get; set; }
}
