using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestDtoValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
        }
    }
}