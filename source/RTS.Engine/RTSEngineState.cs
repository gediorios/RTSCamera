using MissionLibrary.Provider;
using System;
using System.Collections.Generic;

namespace MissionLibrary
{
    public static class RTSEngineState
    {
        private static bool IsInitialized { get; set; }
        private static bool IsSecondInitialized { get; set; }

        private static DIProvider ProviderManager { get; set; }

        public static bool WatchMode = false;

        public static void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            ProviderManager = new DIProvider();
        }

        public static void SecondInitialize()
        {
            if (IsSecondInitialized)
                return;

            IsSecondInitialized = true;
            ProviderManager.InstantiateAll();
        }

        public static void RegisterProvider<T>(IObjectVersion<T> newProvider, string key = "") where T : ATag<T>
        {
            ProviderManager.RegisterProvider(newProvider, key);
        }

        public static T GetProvider<T>(string key = "") where T : ATag<T>
        {
            return ProviderManager.GetProvider<T>();
        }

        public static IEnumerable<T> GetProviders<T>() where T : ATag<T>
        {
            return ProviderManager.GetProviders<T>();
        }

        public static void Clear()
        {
            if (!IsInitialized)
                return;

            IsInitialized = false;
            IsSecondInitialized = false;
            ProviderManager = null;
            WatchMode = false;
        }
    }
}
