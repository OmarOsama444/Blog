using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class ExpiredException(string code, params object[] args) : LocalizedHttpException(code, StatusCodes.Status410Gone, args)
{
    public string Code { get; } = code;
}
