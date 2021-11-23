using System;
using MissionLibrary.HotKey;
using MissionSharedLibrary.Config.HotKey;
using MissionSharedLibrary.HotKey.Category;
using System.Collections.Generic;
using TaleWorlds.InputSystem;
using MissionSharedLibrary.HotKey;
using RTS.Framework.Domain;

namespace RTSCamera.Config.HotKey
{
    public class RTSCameraGameKeyCategory
    {
        public static AGameKeyCategory Category => AGameKeyCategoryManager.Get().GetCategory(Constants.DefaultHotKeyCategoryId);

        public static IGameKeySequence GetKey(Constants.GameKeyEnum key)
        {
            return Category?.GetGameKeySequence((int)key);
        }
    }
}
