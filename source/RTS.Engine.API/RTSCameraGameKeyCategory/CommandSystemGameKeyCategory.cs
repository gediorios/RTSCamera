using System;
using MissionLibrary.HotKey;
using MissionSharedLibrary.Config.HotKey;
using MissionSharedLibrary.HotKey.Category;
using System.Collections.Generic;
using TaleWorlds.InputSystem;
using MissionSharedLibrary.HotKey;
using static RTS.Framework.Domain.Constants;
using RTS.Framework.Domain;

namespace RTSCamera.CommandSystem.Config.HotKey
{
    public class CommandSystemGameKeyCategory
    {
        //public const string CategoryId = "RTSCameraCommandSystemHotKey";

        public static AGameKeyCategory Category => AGameKeyCategoryManager.Get().GetCategory(Constants.RTSCameraHotKeyCategoryId);

        /*public static void RegisterGameKeyCategory()
        {
            AGameKeyCategoryManager.Get()?.AddCategory(CreateCategory, new Version(1, 0));
        }*/


        public static IGameKeySequence GetKey(GameKeyEnum key)
        {
            return Category?.GetGameKeySequence((int)key);
        }
    }
}
