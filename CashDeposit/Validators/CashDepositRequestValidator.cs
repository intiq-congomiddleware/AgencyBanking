using CashDeposit.Entities;
using FluentValidation;
using CashDeposit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace CashDeposit.Validators
{
    public class CashDepositRequestValidator : AbstractValidator<Request>
    {
        private readonly ICashDepositRepository _orclRepo;

        public CashDepositRequestValidator(ICashDepositRepository orclRepo)
        {
            _orclRepo = orclRepo;
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

            RuleFor(req => req.branchCode)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);

            RuleFor(req => req.userName)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);

            RuleFor(req => req.requestId)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100)
                  .SetValidator(new RequestIdValidator(_orclRepo));

            RuleFor(req => req.debitAccount)
                   .NotNull()
                   .NotEmpty()
                   .MaximumLength(20);

            RuleFor(req => req.currency)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .SetValidator(new CurrencyEnumValidator());
        }
    }
}
