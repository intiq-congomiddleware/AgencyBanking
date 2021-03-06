﻿using AccountOpening.Entities;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOpening.Validators
{
    public class RequestIdValidator : PropertyValidator
    {
        private readonly IAccountOpeningRepository _orclRepo;

        public RequestIdValidator(IAccountOpeningRepository orclRepo)
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
