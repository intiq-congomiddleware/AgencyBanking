using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundsTransfer.Validators
{
    public class RequestIdValidator : PropertyValidator
    {
        public RequestIdValidator()
             : base("Invalid {PropertyName}, {IdValue}.")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var idString = context.PropertyValue as string;            
            DateTime dateValue;

            //Confirm valid date then check if date is greater than or equal to now.
            if (string.IsNullOrEmpty(idString) || idString.Length < 21)
            {
                context.MessageFormatter.AppendArgument("IdValue", idString);
                return false;
            }

            string dateString = idString.Substring(0, 14);

            //confirm datetime part of id
            if (!DateTime.TryParse(dateString, out dateValue))
            {
                context.MessageFormatter.AppendArgument("IdValue", idString);
                return false;
            }

            //confirm branch
            string branchString = idString.Substring(14, 17);

            //confirm last four digits
            string randomFour = idString.Substring(idString.Length - 4);

            foreach (char c in randomFour.ToString())
            {
                if (!Char.IsDigit(c))
                {
                    context.MessageFormatter.AppendArgument("IdValue", idString);
                    return false;
                }
            }

            return true;
        }
    }
}
