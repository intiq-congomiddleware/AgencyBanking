using AccountOpening.Entities;
using AccountOpening.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Validators
{
    public class AccountOpeningRequestValidator : AbstractValidator<Request>
    {
        public AccountOpeningRequestValidator()
        {
            RuleFor(req => req.customerType)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.customerName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.shortName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.customerCategory)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.customerPrefix)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.firstName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.middleName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.lastName)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.dateOfBirth)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.minor)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.sex)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.dAddress1)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.dAddress2)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.dAddress3)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.telephone)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.email)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.amountsCcy)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.branchCode)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.country)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.nationality)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.language)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.accountClass)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.requestId)
           .NotNull()
           .NotEmpty();
        }
    }
}
