using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Domain.Entities;
using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Infrastructure.AssetWorks;

// Stub for feasibility/demo.
// Replace with an HttpClient-based implementation once you have AssetWorks API access.
public sealed class AssetWorksClientStub : IAssetWorksClient
{
    private static readonly List<Asset> _assets =
    [
        new Asset
        {
            ExternalId = "AW-1001",
            Vin = Vin.From("1HGCM82633A004352"),
            UnitNumber = "UNIT-01",
            Status = "Active",
            UpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-2)
        },
        new Asset
        {
            ExternalId = "AW-1002",
            Vin = Vin.From("2T1BURHE5FC409999"),
            UnitNumber = "UNIT-02",
            Status = "Active",
            UpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-1)
        }
    ];

    public Task<IReadOnlyList<Asset>> GetAssetsAsync(DateTimeOffset? sinceUtc, CancellationToken ct)
    {
        IReadOnlyList<Asset> result = sinceUtc is null
            ? _assets
            : _assets.Where(a => a.UpdatedAtUtc >= sinceUtc.Value).ToList();

        return Task.FromResult(result);
    }

    public Task PushRecallUpdatesAsync(IReadOnlyList<RecallUpdate> updates, CancellationToken ct)
    {
        // In the real version: POST/PATCH to AssetWorks endpoints for work orders/maintenance/notes, depending on capabilities.
        // Here: pretend it succeeded.
        return Task.CompletedTask;
    }
}
