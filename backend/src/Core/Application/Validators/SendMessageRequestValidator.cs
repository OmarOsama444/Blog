using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageRequestValidator()
        {
            RuleSet("REQUIRED_TO_NUMBER", () =>
            {
                RuleFor(x => x.ToId).NotEmpty();
            });
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}