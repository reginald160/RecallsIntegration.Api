namespace RecallsIntegration.Api.Application.Dtos;

public sealed record AssetDto(
    string ExternalId,
    string Vin,
    string? UnitNumber,
    string? Status,
    DateTimeOffset UpdatedAtUtc
);
