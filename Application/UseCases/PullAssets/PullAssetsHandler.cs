using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Domain.Common;
using RecallsIntegration.Api.Domain.Entities;

namespace RecallsIntegration.Api.Application.UseCases.PullAssets;

public sealed class PullAssetsHandler
{
    private readonly IAssetWorksClient _assetWorks;
    private readonly IRecallRepository _repo;
    private readonly IUnitOfWork _uow;

    public PullAssetsHandler(IAssetWorksClient assetWorks, IRecallRepository repo, IUnitOfWork uow)
    {
        _assetWorks = assetWorks;
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<IReadOnlyList<Asset>>> HandleAsync(PullAssetsQuery query, CancellationToken ct)
    {
        var assets = await _assetWorks.GetAssetsAsync(query.SinceUtc, ct);

        await _repo.UpsertAssetsAsync(assets, ct);
        await _uow.SaveChangesAsync(ct);

        return Result<IReadOnlyList<Asset>>.Ok(assets);
    }
}
