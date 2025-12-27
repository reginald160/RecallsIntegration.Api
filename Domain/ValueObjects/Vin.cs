namespace RecallsIntegration.Api.Domain.ValueObjects;

public readonly record struct Vin(string Value)
{
    public static Vin From(string vin)
    {
        vin = (vin ?? string.Empty).Trim().ToUpperInvariant();

        if (vin.Length != 17)
            throw new ArgumentException("VIN must be 17 characters.", nameof(vin));

        return new Vin(vin);
    }

    public override string ToString() => Value;
}
