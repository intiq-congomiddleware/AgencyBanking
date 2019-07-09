using AccountBlock.Entities;
using AccountBlock.Models;
using AccountBlock.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountBlock.Validators
{
    public class AccountBlockRequestValidator : AbstractValidator<Request>
    {
        private readonly IAccountBlockRepository _orclRepo;

        public AccountBlockRequestValidator(IAccountBlockRepository orclRepo)
        {
            _orclRepo = orclRepo;
            RuleFor(req => req.accountNumber)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.userName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.reason)
           .NotNull()
           .NotEmpty();          

            RuleFor(req => req.requestId)
           .NotNull()
           .NotEmpty()
           .MaximumLength(100)
           .SetValidator(new RequestIdValidator(_orclRepo));
        }
    }
}
