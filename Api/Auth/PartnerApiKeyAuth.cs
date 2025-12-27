namespace RecallsIntegration.Api.Api.Auth;

public static class PartnerApiKeyAuth
{
    private const string HeaderName = "X-Partner-Api-Key";

    public static bool IsAuthorized(HttpContext ctx, IConfiguration config)
    {
        var expected = config["PartnerApiKey"];
        if (string.IsNullOrWhiteSpace(expected))
            return false;

        if (!ctx.Request.Headers.TryGetValue(HeaderName, out var provided))
            return false;

        return string.Equals(provided.ToString(), expected, StringComparison.Ordinal);
    }
}
