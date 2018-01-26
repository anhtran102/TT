using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Tieptuyen.Components.Globalization.Api
{
    /// <summary>
    /// Provides extension methods for sub-classes of the <see cref="ApiController"/> class.
    /// </summary>
    public static class ApiControllerExtensions
    {
         /// <summary>
        /// Gets the identifier for the preferred language from the HTTP request headers.
        /// </summary>
        /// <param name="apiController">The API controller.</param>
        /// <returns>The language identifier as a string.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="apiController"/> is <c>null</c>.</exception>
        /// <remarks>
        /// If the <c>locale</c> HTTP header is either missing or empty, the default language is returned. 
        /// </remarks>
        public static string GetPreferredLanguageId(this ApiController apiController)
        {
            if (apiController != null)
            {
                IEnumerable<string> values;
                if (apiController.Request.Headers.TryGetValues("locale", out values))
                {
                    string locale = values.FirstOrDefault(x => !string.IsNullOrEmpty(x));
                    return !string.IsNullOrEmpty(locale) ? locale : Language.DefaultLanguage;
                }
                else
                    return Language.DefaultLanguage;
            }
            else
                throw new ArgumentNullException("apiController", "The API controller cannot be null.");
        }
    }
}
