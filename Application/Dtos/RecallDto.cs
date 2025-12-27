namespace RecallsIntegration.Api.Application.Dtos;

public sealed record RecallDto(
    string RecallId,
    string Vin,
    string CampaignCode,
    string Description,
    string? Severity,
    string? Status,
    DateTimeOffset UpdatedAtUtc
);
