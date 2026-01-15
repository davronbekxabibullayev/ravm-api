namespace Devhub.Localization.Abstractions;

using Microsoft.Extensions.Localization;

public interface ILocalizer
{
    string LanguageCode { get; }
    public Options.LocalizationOptions Options { get; }
    LocalizedString this[string name] { get; }
    LocalizedString Localize(string key);
}
