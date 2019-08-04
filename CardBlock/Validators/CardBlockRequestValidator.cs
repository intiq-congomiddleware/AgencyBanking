using AccountBlock.Validators;
using CardBlock.Entities;
using CardBlock.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardBlock.Validators
{
    public class CardBlockRequestValidator : AbstractValidator<Request>
    {
        private readonly ICardBlockRepository _orclRepo;

        public CardBlockRequestValidator(ICardBlockRepository orclRepo)
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
