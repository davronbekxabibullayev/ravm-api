namespace Devhub.Localization.Services;

using Devhub.Localization.Abstractions;
using Microsoft.Extensions.Localization;

internal class Localizer : ILocalizer
{
    private readonly ILanguageResolver _languageResolver;

    private readonly IStringLocalizer _localizer;

    public string LanguageCode => _languageResolver.GetLanguageCode() ?? Options.DefaultCulture;

    public Options.LocalizationOptions Options { get; }

    public LocalizedString this[string name] => Localize(name);

    public Localizer(ILanguageResolver languageResolver, IStringLocalizerFactory factory, Options.LocalizationOptions options)
    {
        Options = options;
        _languageResolver = languageResolver;
        _localizer = factory.Create(options.ResourceSource);
    }

    public LocalizedString Localize(string key)
    {
        return _localizer.GetString(key);
    }
}
