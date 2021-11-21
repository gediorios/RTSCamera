using System;
using System.Collections.Generic;
using MissionLibrary.HotKey;
using MissionLibrary.Provider;
using MissionSharedLibrary.Provider;

namespace MissionSharedLibrary.HotKey
{
    public static class AGameKeyCategoryManagerExtension
    {
        public static void AddCategory(this AGameKeyCategoryManager categoryManager, Func<AGameKeyCategory> creator, Version version, bool addOnlyWhenMissing = true)
        {
            categoryManager.AddCategory(new ObjectVersion<AGameKeyCategory>(creator, version), addOnlyWhenMissing);
        }

    }
    public class GameKeyCategoryManager : AGameKeyCategoryManager
    {

        public override Dictionary<string, IObjectVersion<AGameKeyCategory>> Categories { get; } = new Dictionary<string, IObjectVersion<AGameKeyCategory>>();

        public override void AddCategory(IObjectVersion<AGameKeyCategory> provider, bool addOnlyWhenMissing = true)
        {
            if (Categories.TryGetValue(provider.Value.GameKeyCategoryId, out IObjectVersion<AGameKeyCategory> existingProvider))
            {
                if (existingProvider.ProviderVersion == provider.ProviderVersion && addOnlyWhenMissing ||
                    existingProvider.ProviderVersion > provider.ProviderVersion)
                    return;

                Categories[provider.Value.GameKeyCategoryId] = provider;
            }

            Categories.Add(provider.Value.GameKeyCategoryId, provider);

            provider.Value.Load();
            provider.Value.Save();
        }

        public override AGameKeyCategory GetCategory(string categoryId)
        {
            if (Categories.TryGetValue(categoryId, out IObjectVersion<AGameKeyCategory> provider))
            {
                return provider.Value;
            }

            return null;
        }

        public override T GetCategory<T>(string categoryId)
        {
            if (Categories.TryGetValue(categoryId, out IObjectVersion<AGameKeyCategory> provider) && provider.Value is T t)
            {
                return t;
            }

            return null;
        }

        public override void Save()
        {
            foreach(var pair in Categories)
            {
                pair.Value.Value.Save();
            }
        }
    }
}
