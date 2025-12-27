namespace RecallsIntegration.Api.Application.Dtos;

public sealed record RecallUpdateDto(
    string RecallId,
    string AssetExternalId,
    string Status,
    string? Notes,
    DateTimeOffset OccurredAtUtc
);
