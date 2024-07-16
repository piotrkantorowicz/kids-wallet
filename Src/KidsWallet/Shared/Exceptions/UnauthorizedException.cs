namespace KidsWallet.Shared.Exceptions;

public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message: message)
    {
    }
}