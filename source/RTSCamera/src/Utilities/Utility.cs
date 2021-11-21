﻿using RTSCamera.Config.HotKey;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace RTSCamera.Utilities
{
    public class Utility
    {
        public static void PrintUsageHint()
        {
            var keyName = RTSCameraGameKeyCategory.GetKey(GameKeyEnum.FreeCamera).ToSequenceString();
            var hint = Module.CurrentModule.GlobalTextManager.FindText("str_rts_camera_switch_camera_hint").SetTextVariable("KeyName", keyName).ToString();
            MissionSharedLibrary.Utilities.Utility.DisplayMessageForced(hint);
        }

    }
}
