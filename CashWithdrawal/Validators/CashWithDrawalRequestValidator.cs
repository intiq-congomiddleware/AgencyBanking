﻿using CashWithdrawal.Entities;
using FluentValidation;
using CashWithdrawal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashWithdrawal.Validators
{
    public class CashWithDrawalRequestValidator : AbstractValidator<Request>
    {
        public CashWithDrawalRequestValidator()
        {
            RuleFor(req => req.debitAccount)
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
            RuleFor(req => req.branch_code)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);
            RuleFor(req => req.user_name)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);
            RuleFor(req => req.requestId)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);
            RuleFor(req => req.creditAccount)
                   .NotNull()
                   .NotEmpty()
                   .MaximumLength(20);
        }
    }
}
