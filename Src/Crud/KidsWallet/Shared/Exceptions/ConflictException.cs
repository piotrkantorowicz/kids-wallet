namespace KidsWallet.Shared.Exceptions;

public sealed class ConflictException(object id) : Exception($"Entity with id {id} already exists.");