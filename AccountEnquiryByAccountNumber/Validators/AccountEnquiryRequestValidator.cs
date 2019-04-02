using AccountEnquiry.Entities;
using FluentValidation;


namespace AccountEnquiry.Validators
{
    public class AccountEnquiryRequestValidator : AbstractValidator<AccountEnquiryRequest>
    {
        public AccountEnquiryRequestValidator()
        {

            RuleFor(req => req.accountNumber)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(20);
        }
    }
}
