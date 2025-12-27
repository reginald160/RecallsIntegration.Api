using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Domain.Entities;

public sealed class Recall
{
    public required string RecallId { get; init; } // internal recall id
    public required Vin Vin { get; init; }
    public required string CampaignCode { get; init; }
    public required string Description { get; init; }

    public string? Severity { get; set; }
    public string? Status { get; set; }   // Open/In Progress/Completed
    public DateTimeOffset UpdatedAtUtc { get; set; }
}
