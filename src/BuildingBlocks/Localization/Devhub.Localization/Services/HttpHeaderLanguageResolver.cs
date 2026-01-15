namespace Devhub.Localization.Services;

using Devhub.Localization.Abstractions;
using Microsoft.AspNetCore.Http;

internal class HttpHeaderLanguageResolver : ILanguageResolver
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpHeaderLanguageResolver(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetLanguageCode()
    {
        return _contextAccessor.HttpContext
            .Request
            .GetTypedHeaders()
            .AcceptLanguage
            .FirstOrDefault()?.Value
            .Value;
    }
}
