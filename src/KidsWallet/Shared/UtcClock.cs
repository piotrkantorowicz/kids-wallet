using KidsWallet.Shared.Abstraction;

namespace KidsWallet.Shared;

public sealed class UtcClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}