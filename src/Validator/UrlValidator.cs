using System;

namespace LinkReader.Validator
{
    public class UrlValidator : IValidator
    {
        public bool Validate(string text)
        {
            Uri uriResult;
            return Uri.TryCreate(text, UriKind.Absolute, out uriResult)
    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}