using AccountEnquiry.Entities;
using FluentValidation;


namespace AccountEnquiry.Validators
{
    public class AccountEnquiryRequestValidator : AbstractValidator<CustomerEnquiryRequest>
    {
        public AccountEnquiryRequestValidator()
        {

            RuleFor(req => req.customerNumber)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(20);
        }
    }
}
