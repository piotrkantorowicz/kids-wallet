namespace KidsWallet.Shared.Exceptions;

public sealed class ConflictException : Exception
{
    public ConflictException(object id) : base(message: $"Entity with id {id} already exists.")
    {
    }
}