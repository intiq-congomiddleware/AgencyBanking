using CashDeposit.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Validators
{
    public class CashDepositRequestValidator : AbstractValidator<CashDepositRequest>
    {
        public CashDepositRequestValidator()
        {

            RuleFor(req => req.cract)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(20);
            RuleFor(req => req.trnamt)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0);
            RuleFor(req => req.txnnarra)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(100);
        }
    }
}
