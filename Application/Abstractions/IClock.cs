namespace RecallsIntegration.Api.Application.Abstractions;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
