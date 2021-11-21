using System;
using System.Collections.Generic;
using System.Linq;

namespace MissionLibrary.Provider
{
    public class DIProvider : IDIProvider
    {
        private readonly Dictionary<Type, Dictionary<string, IVersion>> _providersWithKey = new Dictionary<Type, Dictionary<string, IVersion>>();

        public void RegisterProvider<T>(IObjectVersion<T> provider, string key = "") where T : ATag<T>
        {
            if (!_providersWithKey.TryGetValue(typeof(T), out var dictionary))
            {
                _providersWithKey.Add(typeof(T), new Dictionary<string, IVersion>() {[key] = provider});
            }
            else if (!dictionary.TryGetValue(key, out var oldProvider))
            {
                dictionary.Add(key, provider);
            }
            else if (oldProvider.ProviderVersion.CompareTo(provider.ProviderVersion) <= 0)
            {
                dictionary[key] = provider;
            }
        }

        public T GetProvider<T>(string key = "") where T : ATag<T>
        {
            if (!_providersWithKey.TryGetValue(typeof(T), out var dictionary) || !dictionary.TryGetValue(key, out IVersion provider) || !(provider is IObjectVersion<T> tProvider))
            {
                return null;
            }

            return tProvider.Value;
        }

        public IEnumerable<T> GetProviders<T>() where T : ATag<T>
        {
            if (!_providersWithKey.TryGetValue(typeof(T), out var dictionary))
            {
                return Enumerable.Empty<T>();
            }

            return dictionary.Values.Where(v => v is IObjectVersion<T>).Select(v => (v as IObjectVersion<T>)?.Value);
        }

        public void InstantiateAll()
        {
            foreach (var pair in _providersWithKey)
            {
                foreach (var versionProviderPair in pair.Value)
                {
                    versionProviderPair.Value.ForceCreate();
                }
            }
        }
    }
}
