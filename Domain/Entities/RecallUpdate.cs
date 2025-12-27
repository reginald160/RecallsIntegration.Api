namespace RecallsIntegration.Api.Domain.Entities;

public sealed class RecallUpdate
{
    public required string RecallId { get; init; }
    public required string AssetExternalId { get; init; }
    public required string Status { get; init; }
    public string? Notes { get; init; }
    public DateTimeOffset OccurredAtUtc { get; init; }
}
