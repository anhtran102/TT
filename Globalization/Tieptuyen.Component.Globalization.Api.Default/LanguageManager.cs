using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using Tieptuyen.Components.Core.Api.Util;
using Microsoft.Practices.Unity;
using Tieptuyen.Components.Globalization.Api;
namespace Tieptuyen.Components.Globalization.Api.Default
{
    /// <summary>
    /// The default language manager.
    /// </summary>
    public class LanguageManager : DynamicObject, ILanguageManager, IDisposable
    {
        private const string LangaugesFile = "Tieptuyen.Components.Globalization.Api.Default.Languages.resources";
        private const string TranslationsFile = "Tieptuyen.Components.Globalization.Api.Default.Translations";

        private ResourceManager resourceManager;
        private ResourceSet supportedLanguages;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageManager"/> class.
        /// </summary>
        [InjectionConstructor]
        public LanguageManager()
        {
            Assembly currentAssembly = this.GetType().Assembly;
            this.resourceManager = new ResourceManager(TranslationsFile, currentAssembly);
            this.supportedLanguages = new ResourceSet(currentAssembly.GetManifestResourceStream(LangaugesFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageManager"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="supportedLanguages">The supported languages.</param>
        /// <remarks>This constructor is primarily for unit-testing purposes.  When constructing instance of the <see cref="LanguageManager"/> class, use the default constructor.</remarks>
        public LanguageManager(ResourceManager resourceManager, ResourceSet supportedLanguages)
        {
            this.resourceManager = resourceManager;
            this.supportedLanguages = supportedLanguages;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="LanguageManager"/> class.
        /// </summary>
        ~LanguageManager()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public Language[] GetSupportedLanguages()
        {
            if (!this.disposed)
            {
                IList<Language> supportedLanguages = new List<Language>();
                foreach (DictionaryEntry entry in this.supportedLanguages)
                {
                    Language language = new Language()
                    {
                        Id = (string)entry.Key,
                        Name = (string)entry.Value
                    };

                    supportedLanguages.Add(language);
                }

                return supportedLanguages.OrderBy(x => x.Name).ToArray();
            }
            else
                throw new ObjectDisposedException(this.GetType().FullName, "This language manager has already been disposed.");
        }

        /// <inheritdoc/>
        public Dictionary<string, string> GetAllTranslations(string languageId)
        {
            if (!this.disposed)
            {
                if (!string.IsNullOrEmpty(languageId))
                {
                    CultureInfo defaultCulture = new CultureInfo(Language.DefaultLanguage);
                    CultureInfo requestedCulture = new CultureInfo(languageId);
                    ResourceSet defaultTranslationsSet = this.resourceManager.GetResourceSet(defaultCulture, true, true);
                    Dictionary<string, string> defaultTranslations = MapResourceSetToDictionary(defaultTranslationsSet);

                    if (requestedCulture.Name != defaultCulture.Name)
                    {
                        ResourceSet translationsSet = this.resourceManager.GetResourceSet(requestedCulture, true, true);
                        Dictionary<string, string> translations = MapResourceSetToDictionary(translationsSet);

                        defaultTranslations.Merge(translations, MergeConflictResolution.UseDestination);
                        return translations;
                    }
                    else
                        return defaultTranslations;
                }
                else
                    throw new ArgumentException("The language has not been specified.", "languageId");
            }
            else
                throw new ObjectDisposedException(this.GetType().FullName, "This language manager has already been disposed.");
        }

        /// <inheritdoc/>
        public string GetText(string translationId, string languageId)
        {
            if (!this.disposed)
            {
                if (!string.IsNullOrEmpty(translationId) && !string.IsNullOrEmpty(languageId))
                {
                    CultureInfo requestedCulture = new CultureInfo(languageId);
                    string text = this.resourceManager.GetString(translationId, requestedCulture);
                    if (string.IsNullOrEmpty(text))
                    {
                        CultureInfo defaultCulture = new CultureInfo(Language.DefaultLanguage);
                        text = this.resourceManager.GetString(translationId, defaultCulture);
                    }

                    return text.InitializeIfNull();
                }
                else
                {
                    if (string.IsNullOrEmpty(translationId))
                        throw new ArgumentException("The translation ID has not been specified.", "translationId");
                    else
                        throw new ArgumentException("The language has not been specified.", "languageId");
                }
            }
            else
                throw new ObjectDisposedException(this.GetType().FullName, "This language manager has already been disposed.");
        }

        /// <inheritdoc/>
        public string GetText(string translationId, string languageId, params object[] values)
        {
            string text = this.GetText(translationId, languageId);
            if (!string.IsNullOrEmpty(text) && values != null && values.Length > 0)
                return string.Format(CultureInfo.InvariantCulture, text, values);
            else
                return text;
        }

        /// <inheritdoc/>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            if (args.Length > 0)
            {
                string languageId = args[0] as string;
                if (languageId != null)
                {
                    if (args.Length > 1)
                    {
                        object[] values = args.Skip(1).Where(x => x != null).ToArray();
                        result = this.GetText(binder.Name, languageId, values);
                    }
                    else
                        result = this.GetText(binder.Name, languageId);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.supportedLanguages.Dispose();

                this.disposed = true;
            }
        }

        private static Dictionary<string, string> MapResourceSetToDictionary(ResourceSet resourceSet)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (!dictionary.ContainsKey((string)entry.Key))
                    dictionary.Add((string)entry.Key, (string)entry.Value);
            }

            return dictionary;
        }        
    }
}
