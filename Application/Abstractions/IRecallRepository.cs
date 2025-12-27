using RecallsIntegration.Api.Domain.Entities;
using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Application.Abstractions;

public interface IRecallRepository
{
    Task UpsertAssetsAsync(IReadOnlyList<Asset> assets, CancellationToken ct);
    Task<IReadOnlyList<Asset>> GetAssetsByVinsAsync(IReadOnlyList<Vin> vins, CancellationToken ct);

    Task UpsertRecallsAsync(IReadOnlyList<Recall> recalls, CancellationToken ct);
    Task<IReadOnlyList<Recall>> GetRecallsByVinAsync(Vin vin, CancellationToken ct);

    Task AddRecallUpdatesAsync(IReadOnlyList<RecallUpdate> updates, CancellationToken ct);
    Task<IReadOnlyList<RecallUpdate>> GetPendingRecallUpdatesAsync(int take, CancellationToken ct);
    Task MarkRecallUpdatesAsSentAsync(IReadOnlyList<RecallUpdate> sent, CancellationToken ct);
}
