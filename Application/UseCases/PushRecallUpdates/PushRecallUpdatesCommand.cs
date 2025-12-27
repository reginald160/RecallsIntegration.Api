namespace RecallsIntegration.Api.Application.UseCases.PushRecallUpdates;

public sealed record PushRecallUpdatesCommand(int Take = 100);
