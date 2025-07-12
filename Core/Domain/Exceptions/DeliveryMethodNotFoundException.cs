namespace Domain.Exceptions;

public sealed class DeliveryMethodNotFoundException(int id)
    : NotFoundException($"Deliver Method with id: {id} not found.");
