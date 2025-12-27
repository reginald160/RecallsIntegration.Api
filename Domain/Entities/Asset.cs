using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Domain.Entities;

public sealed class Asset
{
    public required string ExternalId { get; init; } // AssetWorks ID
    public required Vin Vin { get; init; }
    public string? UnitNumber { get; init; }
    public string? Status { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}
