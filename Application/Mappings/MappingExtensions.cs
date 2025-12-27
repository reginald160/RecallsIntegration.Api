using RecallsIntegration.Api.Application.Dtos;
using RecallsIntegration.Api.Domain.Entities;

namespace RecallsIntegration.Api.Application.Mappings;

public static class MappingExtensions
{
    public static AssetDto ToDto(this Asset a) =>
        new(a.ExternalId, a.Vin.Value, a.UnitNumber, a.Status, a.UpdatedAtUtc);

    public static RecallDto ToDto(this Recall r) =>
        new(r.RecallId, r.Vin.Value, r.CampaignCode, r.Description, r.Severity, r.Status, r.UpdatedAtUtc);

    public static RecallUpdateDto ToDto(this RecallUpdate u) =>
        new(u.RecallId, u.AssetExternalId, u.Status, u.Notes, u.OccurredAtUtc);
}
