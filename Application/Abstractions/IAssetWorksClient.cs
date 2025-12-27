using RecallsIntegration.Api.Domain.Entities;

namespace RecallsIntegration.Api.Application.Abstractions;

public interface IAssetWorksClient
{
    Task<IReadOnlyList<Asset>> GetAssetsAsync(DateTimeOffset? sinceUtc, CancellationToken ct);
    Task PushRecallUpdatesAsync(IReadOnlyList<RecallUpdate> updates, CancellationToken ct);
}
