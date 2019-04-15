using CashWithdrawal.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Validators
{
    public class CashWithDrawalRequestValidator : AbstractValidator<CashWithdrawalRequest>
    {
        public CashWithDrawalRequestValidator()
        {
            RuleFor(req => req.dract)
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
            RuleFor(req => req.prate)
                   .NotNull()
                   .NotEmpty()
                   .GreaterThan(0);
                   //.When(x => x.with_charges);
        }
    }
}
