using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CourseCatalog.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(ValidationResult validationResult)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors) ValdationErrors.Add(validationError.ErrorMessage);
        }

        public List<string> ValdationErrors { get; set; }
    }
}