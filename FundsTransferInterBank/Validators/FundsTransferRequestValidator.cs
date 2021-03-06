﻿using AgencyBanking.Entities;
using FluentValidation;
using FundsTransfer.Entities;
using FundsTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Validators
{  
    public class FundsTransferRequestValidator : AbstractValidator<Request>
    {
        private readonly IFundsTransferRepository _orclRepo;

        public FundsTransferRequestValidator(IFundsTransferRepository orclRepo)
        {
            _orclRepo = orclRepo;
            RuleFor(req => req.debitAccount)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(20);

            RuleFor(req => req.creditAccount)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(20);

            RuleFor(req => req.amount)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0);

            RuleFor(req => req.paymentPurpose)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(100);

            RuleFor(req => req.requestId)
                   .NotNull()
                   .NotEmpty()
                   .MaximumLength(100)
                   .SetValidator(new RequestIdValidator(_orclRepo));

            RuleFor(req => req.beneficiaryBank)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);

            RuleFor(req => req.userName)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(100);

            RuleFor(req => req.currency)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(100)
                 .SetValidator(new CurrencyEnumValidator());
        }
    }
}
