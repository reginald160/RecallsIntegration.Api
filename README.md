# RecallsIntegration.Api (sample)

Single-project ASP.NET Core (.NET 8) Web API using Clean Architecture-style folders.

## Run
```bash
dotnet restore
dotnet run
```

Swagger is enabled in Development.

## Integration auth
Integration endpoints under `/integration/*` require:

Header: `X-Partner-Api-Key: change-me`

Configure the key in `appsettings.json` (`PartnerApiKey`).

## Endpoints (sample)
- POST `/assets/pull` body: `{ "sinceUtc": "2025-01-01T00:00:00Z" }`
- GET `/recalls/{vin}`
- POST `/recalls` body: `{ "recalls": [ ... ] }`
- POST `/integration/recall-updates/enqueue` body: `{ "updates": [ ... ] }`
- POST `/integration/recall-updates/push` body: `{ "take": 100 }`
