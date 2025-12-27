using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Domain.Common;

namespace RecallsIntegration.Api.Application.UseCases.PushRecallUpdates;

public sealed class PushRecallUpdatesHandler
{
    private readonly IAssetWorksClient _assetWorks;
    private readonly IRecallRepository _repo;
    private readonly IUnitOfWork _uow;

    public PushRecallUpdatesHandler(IAssetWorksClient assetWorks, IRecallRepository repo, IUnitOfWork uow)
    {
        _assetWorks = assetWorks;
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<int>> HandleAsync(PushRecallUpdatesCommand cmd, CancellationToken ct)
    {
        var pending = await _repo.GetPendingRecallUpdatesAsync(cmd.Take, ct);

        if (pending.Count == 0)
            return Result<int>.Ok(0);

        await _assetWorks.PushRecallUpdatesAsync(pending, ct);
        await _repo.MarkRecallUpdatesAsSentAsync(pending, ct);
        await _uow.SaveChangesAsync(ct);

        return Result<int>.Ok(pending.Count);
    }
}
