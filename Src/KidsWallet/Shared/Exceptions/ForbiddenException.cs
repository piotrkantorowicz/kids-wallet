namespace KidsWallet.Shared.Exceptions;

public sealed class ForbiddenException(string message) : Exception(message: message);