namespace Domain.Exceptions;

public sealed class UnauthorizedException(string Message = "Invalid Email Or Password") : Exception(Message);