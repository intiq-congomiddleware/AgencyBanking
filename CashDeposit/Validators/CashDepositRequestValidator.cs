using CashDeposit.Entities;
using FluentValidation;
using CashDeposit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDeposit.Validators
{
    public class CashDepositRequestValidator : AbstractValidator<Request>
    {
        public CashDepositRequestValidator()
        {

            RuleFor(req => req.creditAccount)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(20);
            RuleFor(req => req.amount)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0);
            RuleFor(req => req.narration)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(100);
        }
    }
}
