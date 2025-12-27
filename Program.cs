using RecallsIntegration.Api.Api.Auth;
using RecallsIntegration.Api.Api.Endpoints;
using RecallsIntegration.Api.Api.Middleware;
using RecallsIntegration.Api.Application.Abstractions;
using RecallsIntegration.Api.Application.UseCases.PullAssets;
using RecallsIntegration.Api.Application.UseCases.PushRecallUpdates;
using RecallsIntegration.Api.Infrastructure.AssetWorks;
using RecallsIntegration.Api.Infrastructure.Persistence;
using RecallsIntegration.Api.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AssetWorksOptions>(builder.Configuration.GetSection("AssetWorks"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddSingleton<IRecallRepository, InMemoryRecallRepository>();
builder.Services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();

// Stubbed AssetWorks client (replace with real HttpClient implementation once API access is confirmed)
builder.Services.AddSingleton<IAssetWorksClient, AssetWorksClientStub>();

builder.Services.AddScoped<PullAssetsHandler>();
builder.Services.AddScoped<PushRecallUpdatesHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Guard /integration routes with a partner API key
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Path.StartsWithSegments("/integration"))
    {
        if (!PartnerApiKeyAuth.IsAuthorized(ctx, builder.Configuration))
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await ctx.Response.WriteAsync("Unauthorized");
            return;
        }
    }

    await next();
});

app.MapGet("/", () => Results.Ok(new { service = "RecallsIntegration.Api", status = "ok" }));

AssetsEndpoints.Map(app);
RecallsEndpoints.Map(app);
IntegrationEndpoints.Map(app);

app.Run();
