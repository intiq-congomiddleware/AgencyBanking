using BalanceEnquiry.Entities;
using FluentValidation;


namespace BalanceEnquiry.Validators
{
    public class BalanceEnquiryRequestValidator : AbstractValidator<BalanceEnquiryRequest>
    {
        public BalanceEnquiryRequestValidator()
        {

            RuleFor(req => req.accountnumber)
                  .NotNull()
                  .NotEmpty()
                  .MaximumLength(20);
        }
    }
}
