using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyBanking.Entities;

namespace CashDeposit.Validators
{
    public class CurrencyEnumValidator : PropertyValidator
    {

        public CurrencyEnumValidator()
            : base("Invalid {PropertyName}, {enumValue}.")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var enumString = context.PropertyValue as string;


            //Confirm valid date then check if date is greater than or equal to now.
            if (string.IsNullOrEmpty(enumString))
            {
                context.MessageFormatter.AppendArgument("enumValue", enumString);
                return false;
            }

            if (!Enum.IsDefined(typeof(currency), enumString.ToUpper()))
            {
                context.MessageFormatter.AppendArgument("enumValue", enumString);
                return false;
            }
            return true;
        }
    }
}
