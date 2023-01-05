namespace CarRental.Identity.Domain.ValueObjects;

public sealed record Email(string Username, string Domain)
{
    public override string ToString()
    {
        return $"{Username}@{Domain}";
    }
}