using AccountOpening.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Validators
{
    public class AccountOpeningRequestValidator : AbstractValidator<AccountOpeningRequest>
    {
        public AccountOpeningRequestValidator()
        {
            RuleFor(req => req.CUSTOMER_TYPE)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.CUSTOMER_NAME)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.SHORT_NAME)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.CUSTOMER_CATEGORY)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.CUSTOMER_PREFIX)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.FIRST_NAME)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.MIDDLE_NAME)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.LAST_NAME)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.DATE_OF_BIRTH)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.MINOR)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.SEX)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.D_ADDRESS1)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.D_ADDRESS2)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.D_ADDRESS3)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.TELEPHONE)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.E_MAIL)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.AMOUNTS_CCY)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.BRANCH_CODE)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.COUNTRY)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.NATIONALITY)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.LANGUAGE)
           .NotNull()
           .NotEmpty();

            RuleFor(req => req.ACCOUNT_CLASS)
           .NotNull()
           .NotEmpty();
        }
    }
}
