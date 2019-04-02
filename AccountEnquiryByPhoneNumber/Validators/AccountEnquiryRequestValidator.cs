using AccountEnquiry.Entities;
using FluentValidation;


namespace AccountEnquiry.Validators
{
    public class AccountEnquiryRequestValidator : AbstractValidator<PhoneEnquiryRequest>
    {
        public AccountEnquiryRequestValidator()
        {
            RuleFor(req => req.phoneNumber)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(20);
        }
    }
}
