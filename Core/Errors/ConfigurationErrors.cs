﻿using ExchangeRate.Domain.Primitive.Result;
using FluentValidation.Results;
using System.Collections.Generic;

namespace ExchangeRate.Domain.Errors;

public static class ConfigurationErrors
{
    public static Error NotFound(string id) =>
        Error.NotFound("Configurations.NotFound", $"Configuration with Id: {id} not found");

    public static Error Conflict(string name) =>
        Error.Conflict("Configurations.Conflict", $"Configuration with Name: {name} already exists");

    public static Error CreateFailure =>
        Error.Failure("Configurations.CreateFailure", $"Something went wrong in creating configuration");

    public static Error UpdateFailure =>
        Error.Failure("Configurations.UpdateFailure", $"Something went wrong in updating configuration");

    public static Error DeleteFailure =>
        Error.Failure("Configurations.DeleteFailure", $"Something went wrong in deleting configuration");

    public static Error RequestValidationError(List<ValidationFailure> error) =>
        Error.Validation("Configurations.RequestValidationError", string.Join(", ", error));

    public static Error UnableGetPublicApiResponse =>
        Error.Failure("Configurations.UnableGetPublicApiResponse", $"Something went wrong in getting Exchange Rate Public API response.");
}
