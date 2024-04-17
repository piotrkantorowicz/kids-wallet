namespace KidsWallet.Shared.Abstraction;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}