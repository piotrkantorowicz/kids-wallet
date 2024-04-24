namespace KidsWallet.Shared.Abstraction;

public interface IClock
{
    DateTime UtcNow { get; }
}