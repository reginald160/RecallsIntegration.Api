using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Domain.Entities;
using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Infrastructure.Persistence;

public sealed class InMemoryRecallRepository : IRecallRepository
{
    private readonly Dictionary<string, Asset> _assetsByExternalId = new();
    private readonly Dictionary<string, List<Recall>> _recallsByVin = new();
    private readonly List<RecallUpdate> _pendingUpdates = new();

    public Task UpsertAssetsAsync(IReadOnlyList<Asset> assets, CancellationToken ct)
    {
        foreach (var a in assets)
            _assetsByExternalId[a.ExternalId] = a;

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Asset>> GetAssetsByVinsAsync(IReadOnlyList<Vin> vins, CancellationToken ct)
    {
        var vinSet = new HashSet<string>(vins.Select(v => v.Value));
        var result = _assetsByExternalId.Values.Where(a => vinSet.Contains(a.Vin.Value)).ToList();
        return Task.FromResult<IReadOnlyList<Asset>>(result);
    }

    public Task UpsertRecallsAsync(IReadOnlyList<Recall> recalls, CancellationToken ct)
    {
        foreach (var r in recalls)
        {
            if (!_recallsByVin.TryGetValue(r.Vin.Value, out var list))
            {
                list = new List<Recall>();
                _recallsByVin[r.Vin.Value] = list;
            }

            var existingIndex = list.FindIndex(x => x.RecallId == r.RecallId);
            if (existingIndex >= 0) list[existingIndex] = r;
            else list.Add(r);
        }
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Recall>> GetRecallsByVinAsync(Vin vin, CancellationToken ct)
    {
        _recallsByVin.TryGetValue(vin.Value, out var list);
        return Task.FromResult<IReadOnlyList<Recall>>(list ?? new List<Recall>());
    }

    public Task AddRecallUpdatesAsync(IReadOnlyList<RecallUpdate> updates, CancellationToken ct)
    {
        _pendingUpdates.AddRange(updates);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<RecallUpdate>> GetPendingRecallUpdatesAsync(int take, CancellationToken ct)
    {
        var result = _pendingUpdates.Take(take).ToList();
        return Task.FromResult<IReadOnlyList<RecallUpdate>>(result);
    }

    public Task MarkRecallUpdatesAsSentAsync(IReadOnlyList<RecallUpdate> sent, CancellationToken ct)
    {
        foreach (var s in sent)
        {
            var idx = _pendingUpdates.FindIndex(x =>
                x.RecallId == s.RecallId &&
                x.AssetExternalId == s.AssetExternalId &&
                x.OccurredAtUtc == s.OccurredAtUtc);

            if (idx >= 0) _pendingUpdates.RemoveAt(idx);
        }
        return Task.CompletedTask;
    }
}
