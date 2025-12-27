using RecallsIntegration.Api.Application.Abstractions;

namespace RecallsIntegration.Api.Infrastructure.Persistence;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
}
