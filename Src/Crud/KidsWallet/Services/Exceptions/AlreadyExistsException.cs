namespace KidsWallet.Services.Exceptions;

public sealed class AlreadyExistsException(object id) : Exception($"Entity with id {id} already exists.");