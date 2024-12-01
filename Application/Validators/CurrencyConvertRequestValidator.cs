﻿using Core.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CurrencyConvertRequestValidator : AbstractValidator<CurrencyConvertRequest>
    {
        public CurrencyConvertRequestValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required")
                .Must(x => x > 0).WithMessage("Amount is negative"); ;

            RuleFor(x => x.InputCurrency)
                .NotEmpty().WithMessage("InputCurrency is required")
                .Must(x => x.Length == 3).WithMessage("InputCurrency must be 3 characters long");

            RuleFor(x => x.OutputCurrancy)
                .NotEmpty().WithMessage("OutputCurrancy is required")
                .Must(x => x.Length == 3).WithMessage("OutputCurrancy must be 3 characters long");
        }
    }
}
