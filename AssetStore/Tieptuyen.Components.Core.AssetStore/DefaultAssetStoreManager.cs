using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using Tieptuyen.Components.Core.AssetStore.Configuration;

namespace Tieptuyen.Components.Core.AssetStore
{
    /// <summary>
    /// The default asset store manager.
    /// </summary>
    public sealed class DefaultAssetStoreManager : DynamicObject, IAssetStoreManager
    {
        private IDictionary<string, IAssetStore> assetStores;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultAssetStoreManager"/> class.
        /// </summary>
        public DefaultAssetStoreManager()
        {
            this.assetStores = new Dictionary<string, IAssetStore>();
        }

        /// <inheritdoc/>
        public void RegisterAssetStore(IAssetStore assetStore)
        {
            if (assetStore != null)
            {
                string alias = assetStore.Alias;
                if (!this.assetStores.ContainsKey(alias))
                {
                    this.assetStores.Add(alias, assetStore);
                    HostingEnvironment.RegisterVirtualPathProvider((VirtualPathProvider)assetStore);
                }
                else
                    throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "A resource store with alias '{0}' has already been registered.", alias));
            }
            else
                throw new ArgumentNullException("assetStore", "The resource store cannot be null.");
        }

        /// <inheritdoc/>
        public void LoadFromConfiguration()
        {
            AssetStoreConfiguration config = AssetStoreConfiguration.GetConfiguration();
            foreach (AssetStoreElement resourceStoreElement in config.Stores)
            {
                IAssetStore resourceStore = CreateResourceStore(resourceStoreElement);
                this.RegisterAssetStore(resourceStore);
            }
        }

        /// <inheritdoc/>
        public IAssetStore GetResourceStore(string alias)
        {
            if (this.assetStores.ContainsKey(alias))
                return this.assetStores[alias];
            else
                throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "No resource store has been registered with the alias '{0}'.", alias));
        }

        /// <inheritdoc/>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this.assetStores.ContainsKey(binder.Name))
            {
                result = this.assetStores[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        private static IAssetStore CreateResourceStore(AssetStoreElement resourceStoreElement)
        {
            try
            {
                IDictionary<string, object> parameters = GenerateParameters(resourceStoreElement);
                Type resourceStoreType = Type.GetType(resourceStoreElement.Type);
                if (resourceStoreType == null)
                    throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "Resource store type '{0}' could not be found.", resourceStoreElement.Type));
                ConstructorInfo[] ctors = resourceStoreType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                for (int i = 0; i < ctors.Length; i++)
                {
                    ParameterInfo[] ctorParameters = ctors[i].GetParameters();
                    if (ctorParameters.Length == parameters.Count)
                    {
                        IList<object> args = new List<object>(ctorParameters.Length);
                        for (int j = 0; j < ctorParameters.Length; j++)
                        {
                            string paramName = ctorParameters[j].Name;
                            if (parameters.ContainsKey(paramName))
                            {
                                object arg = parameters[paramName];
                                args.Add(arg);
                            }
                            else
                                throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "Required parameter '{0}' is missing from the configuration of resource store '{1}'", paramName, resourceStoreElement.Alias));
                        }

                        IAssetStore resourceStore = (IAssetStore)ctors[i].Invoke(args.ToArray());
                        return resourceStore;
                    }
                }

                throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "Unable to construct an instance of '{0}' for resource store '{1}' as an incorrect number of parameters was supplied.", resourceStoreElement.Type, resourceStoreElement.Alias));
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(AssetStoreException))
                    throw new AssetStoreException(string.Format(CultureInfo.InvariantCulture, "Unable to construct an instance of '{0}' for resource store '{1}'.", resourceStoreElement.Type, resourceStoreElement.Alias), ex);
                else
                    throw;
            }
        }

        private static IDictionary<string, object> GenerateParameters(AssetStoreElement resourceStoreElement)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("alias", resourceStoreElement.Alias);
            if (!string.IsNullOrEmpty(resourceStoreElement.Parameters))
            {
                string[] keyValuePairs = resourceStoreElement.Parameters.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < keyValuePairs.Length; i++)
                {
                    string[] keyValuePair = keyValuePairs[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (keyValuePair.Length == 2)
                    {
                        string key = keyValuePair[0];
                        string stringValue = keyValuePair[1];
                        bool boolValue;
                        int intValue;
                        float floatValue;
                        DateTime dateTimeValue;
                        if (bool.TryParse(stringValue, out boolValue))
                            parameters.Add(key, boolValue);
                        else if (int.TryParse(stringValue, out intValue))
                            parameters.Add(key, intValue);
                        else if (float.TryParse(stringValue, out floatValue))
                            parameters.Add(key, floatValue);
                        else if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeValue))
                            parameters.Add(key, dateTimeValue);
                        else
                            parameters.Add(key, stringValue);
                    }
                }
            }
                
            return parameters;
        }
    }
}
