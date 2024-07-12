namespace KidsWallet.Shared.Exceptions;

public sealed class ConflictException(object id) : Exception(message: $"Entity with id {id} already exists.");