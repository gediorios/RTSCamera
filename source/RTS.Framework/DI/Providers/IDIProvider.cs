using System.Collections.Generic;

namespace MissionLibrary.Provider
{
    public interface IDIProvider
    {
        void RegisterProvider<T>(IObjectVersion<T> provider, string key = "") where T : ATag<T>;


        T GetProvider<T>(string key = "") where T : ATag<T>;

        IEnumerable<T> GetProviders<T>() where T : ATag<T>;


        void InstantiateAll();
    }
}
