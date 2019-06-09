using FluentValidation;
using StatementGeneration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionStatus.Validators
{
    public class StatementRequestValidator : AbstractValidator<StatementRequest>
    {
        public StatementRequestValidator()
        {

            RuleFor(req => req.accountNumber)
                    .NotNull()
                    .NotEmpty();
                    //.MaximumLength(100);
        }
    }
}
