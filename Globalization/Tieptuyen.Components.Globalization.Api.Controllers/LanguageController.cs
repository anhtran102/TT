using System.Collections.Generic;
using System.Web.Http;

using Tieptuyen.Components.Globalization.Api;

namespace Tieptuyen.Components.Globalization.Api.Controllers
{
    /// <summary>
    /// A controller for delivering language functionality.
    /// </summary>
    public class LanguageController : ApiController
    {
        private ILanguageManager languageManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageController"/> class.
        /// </summary>
        /// <param name="languageManager">The language manager.</param>
        public LanguageController(ILanguageManager languageManager)
        {
            this.languageManager = languageManager;
        }

        /// <summary>
        /// Gets an array of the currently-supported languages.
        /// </summary>
        /// <returns>An array of <see cref="Language"/> objects.</returns>
        [HttpGet]
        public Language[] GetSupportedLanguages()
        {
            return this.languageManager.GetSupportedLanguages();
        }

        /// <summary>
        /// Gets all the translations for the specified language.
        /// </summary>
        /// <param name="languageId">The language ID.</param>
        /// <returns>A Dictionary of all the translations against their IDs</returns>
        [HttpGet]
        public Dictionary<string, string> GetAllTranslations(string languageId)
        {
            return this.languageManager.GetAllTranslations(languageId);
        }

        /// <summary>
        /// Gets an item translated text.
        /// </summary>
        /// <param name="translationId">The translation ID.</param>
        /// <param name="languageId">The language ID.</param>
        /// <returns>The translated text as a <see cref="string"/>; or an empty string if no translation exist.</returns>
        [HttpGet]
        public string GetTranslatedText(string translationId, string languageId)
        {
            return this.languageManager.GetText(translationId, languageId);
        }
    }
}
