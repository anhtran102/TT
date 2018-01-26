using System.Collections.Generic;
using System.Dynamic;

namespace Tieptuyen.Components.Globalization.Api
{
    /// <summary>
    /// A manager for managing languages within the system.
    /// </summary>
    public interface ILanguageManager : IDynamicMetaObjectProvider
    {
        /// <summary>
        /// Gets an array of the currently-supported languages.
        /// </summary>
        /// <returns>An array of <see cref="Language"/>objects.</returns>
        Language[] GetSupportedLanguages();

        /// <summary>
        /// Gets all the translations for the specified language.
        /// </summary>
        /// <param name="languageId">The language ID.</param>
        /// <returns>A Dictionary{int, string} of all the translations against their IDs</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="languageId" /> is <c>null</c> or empty.</exception>
        Dictionary<string, string> GetAllTranslations(string languageId);

        /// <summary>
        /// Gets the text for a particular translation ID.
        /// </summary>
        /// <param name="translationId">The translation ID.</param>
        /// <param name="languageId">The language ID.</param>
        /// <returns>The text as a <see cref="System.String" />.  If there is no translation for <paramref name="translationId" />, an empty string is returned.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="languageId" /> is <c>null</c> or empty.</exception>
        string GetText(string translationId, string languageId);

        /// <summary>
        /// Gets the text for a particular translation ID, substituting any placeholders for the values specified.
        /// </summary>
        /// <param name="translationId">The translation ID.</param>
        /// <param name="languageId">The language ID.</param>
        /// <param name="values">The values to substitute in.</param>
        /// <returns>The text as a <see cref="System.String" />.  If there is no translation for <paramref name="translationId" />, an empty string is returned.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="languageId" /> is <c>null</c> or empty.</exception>
        string GetText(string translationId, string languageId, params object[] values);
    }
}
