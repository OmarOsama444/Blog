using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequestDto>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId is required.");
        }
    }
}