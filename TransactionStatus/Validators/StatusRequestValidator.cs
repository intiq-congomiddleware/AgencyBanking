using AgencyBanking.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStatus.Entities;

namespace TransactionStatus.Validators
{
   
    public class StatusRequestValidator : AbstractValidator<StatusRequest>
    {
        private readonly AppSettings settings;
        public StatusRequestValidator(AppSettings _settings)
        {
            settings = _settings;
            RuleFor(req => req.requestId)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(100)
                    .SetValidator(new RequestIdValidator(settings));
        }
    }
}
