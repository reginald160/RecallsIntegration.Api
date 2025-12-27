using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Application.Mappings;
using RecallsIntegration.Api.Domain.Entities;
using RecallsIntegration.Api.Domain.ValueObjects;

namespace RecallsIntegration.Api.Api.Endpoints;

public static class RecallsEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/recalls");

        group.MapGet("/{vin}", async (string vin, IRecallRepository repo, CancellationToken ct) =>
        {
            var recalls = await repo.GetRecallsByVinAsync(Vin.From(vin), ct);
            return Results.Ok(recalls.Select(r => r.ToDto()));
        })
        .WithName("GetRecallsByVin")
        .WithOpenApi();

        group.MapPost("/", async (UpsertRecallsRequest request, IRecallRepository repo, IUnitOfWork uow, CancellationToken ct) =>
        {
            var recalls = request.Recalls.Select(r => new Recall
            {
                RecallId = r.RecallId,
                Vin = Vin.From(r.Vin),
                CampaignCode = r.CampaignCode,
                Description = r.Description,
                Severity = r.Severity,
                Status = r.Status,
                UpdatedAtUtc = r.UpdatedAtUtc ?? DateTimeOffset.UtcNow
            }).ToList();

            await repo.UpsertRecallsAsync(recalls, ct);
            await uow.SaveChangesAsync(ct);

            return Results.Ok(new { upserted = recalls.Count });
        })
        .WithName("UpsertRecalls")
        .WithOpenApi();
    }

    public sealed record UpsertRecallsRequest(IReadOnlyList<RecallInput> Recalls);

    public sealed record RecallInput(
        string RecallId,
        string Vin,
        string CampaignCode,
        string Description,
        string? Severity,
        string? Status,
        DateTimeOffset? UpdatedAtUtc
    );
}
