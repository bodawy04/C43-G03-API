﻿namespace Domain.Exceptions;

public sealed class ProductNotFoundException(int id)
    : NotFoundException($"Product with id: {id} not found.");
