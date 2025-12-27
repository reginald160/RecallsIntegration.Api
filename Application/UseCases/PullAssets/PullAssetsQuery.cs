namespace RecallsIntegration.Api.Application.UseCases.PullAssets;

public sealed record PullAssetsQuery(DateTimeOffset? SinceUtc);
