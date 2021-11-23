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
        public static AGameKeyCategory Category => AGameKeyCategoryManager.Get().GetCategory(Constants.DefaultHotKeyCategoryId/*Constants.FormationCommandsHotKeyCategoryId*/);

        public static IGameKeySequence GetKey(GameKeyEnum key)
        {
            return Category?.GetGameKeySequence((int)key);
        }
    }
}
