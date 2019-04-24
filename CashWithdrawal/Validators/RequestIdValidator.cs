using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using CashWithdrawal.Entities;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashWithdrawal.Validators
{
    public class RequestIdValidator : PropertyValidator
    {
        private readonly ICashWithdrawalRepository _orclRepo;

        public RequestIdValidator(ICashWithdrawalRepository orclRepo)
             : base("Invalid or Duplicate {PropertyName}, {IdValue}.")
        {
            _orclRepo = orclRepo;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var idString = context.PropertyValue as string;

            //Confirm valid date then check if date is greater than or equal to now.
            if (string.IsNullOrEmpty(idString))
            {
                context.MessageFormatter.AppendArgument("IdValue", idString);
                return false;
            }

            //Check Duplicate ID
            if (_orclRepo.isDuplicateID(idString))
            {
                context.MessageFormatter.AppendArgument("IdValue", idString);
                return false;
            }

            return true;
        }
    }
}
