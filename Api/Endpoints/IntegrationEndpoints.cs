using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Application.UseCases.PushRecallUpdates;
using RecallsIntegration.Api.Domain.Entities;

namespace RecallsIntegration.Api.Api.Endpoints;

public static class IntegrationEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/integration");

        // Fleets / your system can enqueue recall updates to later push to AssetWorks.
        group.MapPost("/recall-updates/enqueue", async (
            EnqueueRecallUpdatesRequest request,
            IRecallRepository repo,
            IUnitOfWork uow,
            CancellationToken ct) =>
        {
            var updates = request.Updates.Select(u => new RecallUpdate
            {
                RecallId = u.RecallId,
                AssetExternalId = u.AssetExternalId,
                Status = u.Status,
                Notes = u.Notes,
                OccurredAtUtc = u.OccurredAtUtc ?? DateTimeOffset.UtcNow
            }).ToList();

            await repo.AddRecallUpdatesAsync(updates, ct);
            await uow.SaveChangesAsync(ct);

            return Results.Ok(new { enqueued = updates.Count });
        })
        .WithName("EnqueueRecallUpdates")
        .WithOpenApi();

        // Push pending recall updates to AssetWorks (stubbed).
        group.MapPost("/recall-updates/push", async (
            PushRecallUpdatesHandler handler,
            PushRecallUpdatesRequest request,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(new PushRecallUpdatesCommand(request.Take ?? 100), ct);

            return result.Success
                ? Results.Ok(new { pushed = result.Value })
                : Results.BadRequest(new { error = result.Error });
        })
        .WithName("PushRecallUpdatesToAssetWorks")
        .WithOpenApi();
    }

    public sealed record EnqueueRecallUpdatesRequest(IReadOnlyList<RecallUpdateInput> Updates);

    public sealed record RecallUpdateInput(
        string RecallId,
        string AssetExternalId,
        string Status,
        string? Notes,
        DateTimeOffset? OccurredAtUtc
    );

    public sealed record PushRecallUpdatesRequest(int? Take);
}
