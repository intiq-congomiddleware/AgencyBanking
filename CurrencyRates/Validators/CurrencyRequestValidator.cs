using CurrencyRates.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRates.Validators
{
    public class CurrencyRequestValidator : AbstractValidator<CurrencyRequest>
    {
        private readonly ICurrencyRepository _orclRepo;
        public CurrencyRequestValidator(ICurrencyRepository orclRepo)
        {
            _orclRepo = orclRepo;
            RuleFor(req => req.userName)
            .NotNull()
            .NotEmpty();

            RuleFor(req => req.requestId)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
            //.SetValidator(new RequestIdValidator(_orclRepo));
        }
    }
}