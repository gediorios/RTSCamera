using MissionLibrary;
using MissionLibrary.HotKey;
using MissionLibrary.Provider;
using MissionSharedLibrary.Controller;
using MissionSharedLibrary.Controller.Camera;
using MissionSharedLibrary.HotKey;
using MissionSharedLibrary.HotKey.Category;
using MissionSharedLibrary.Provider;
using MissionSharedLibrary.Utilities;
using MissionSharedLibrary.View;
using RTSCamera.CommandSystem.Config.HotKey;
using RTSCamera.Config.HotKey;
using System;

namespace MissionSharedLibrary
{
    public static class Initializer
    {
        public static bool IsInitialized { get; private set; }
        public static bool IsSecondInitialized { get; private set; }

        public static bool Initialize(string moduleId)
        {
            if (IsInitialized)
                return false;

            IsInitialized = true;
            Utility.ModuleId = moduleId;

            RTSEngineState.Initialize();
            RegisterProviders();

            return true;
        }

        public static bool SecondInitialize()
        {
            if (IsSecondInitialized)
                return false;

            IsSecondInitialized = true;

            RTSEngineState.SecondInitialize();
            AGameKeyCategoryManager.RegisterGameKeys();
            //RTSCameraGameKeyCategory.RegisterGameKeyCategory();
            //CommandSystemGameKeyCategory.RegisterGameKeyCategory();

            return true;
        }

        public static void Clear()
        {
            RTSEngineState.Clear();
        }

        private static void RegisterProviders()
        {
            RegisterProvider(() => new GameKeyCategoryManager(), new Version(1, 0));
            RegisterProvider(() => new CameraControllerManager(), new Version(1, 0));
            //RegisterProvider(() => new InputControllerFactory(), new Version(1, 0));
            RegisterProvider(() => new MissionStartingManager(), new Version(1, 1));
            RegisterProvider(() => new DefaultMissionStartingHandlerAdder(), new Version(1, 0));
           //RegisterProvider(() => new MenuManager(), new Version(1, 1));
        }

        public static void RegisterProvider<T>(Func<ATag<T>> creator, Version providerVersion, string key = "") where T : ATag<T>
        {
            RTSEngineState.RegisterProvider(ObjectVersion<T>.Create(creator, providerVersion));
        }

    }
}
