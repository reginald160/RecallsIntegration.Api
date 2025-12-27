using RecallsIntegration.Api.Application.Mappings;
using RecallsIntegration.Api.Application.UseCases.PullAssets;

namespace RecallsIntegration.Api.Api.Endpoints;

public static class AssetsEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/assets");

        group.MapPost("/pull", async (
            PullAssetsHandler handler,
            PullAssetsRequest request,
            CancellationToken ct) =>
        {
            var since = request.SinceUtc;
            var result = await handler.HandleAsync(new PullAssetsQuery(since), ct);

            return result.Success
                ? Results.Ok(result.Value!.Select(a => a.ToDto()))
                : Results.BadRequest(new { error = result.Error });
        })
        .WithName("PullAssetsFromAssetWorks")
        .WithOpenApi();
    }

    public sealed record PullAssetsRequest(DateTimeOffset? SinceUtc);
}
