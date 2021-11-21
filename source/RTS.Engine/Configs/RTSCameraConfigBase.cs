﻿using System.IO;
using MissionSharedLibrary.Config;

namespace RTSCamera.Config
{
    /*public static class OldConfigPath
    {
        public static string OldSavePath { get; } = Path.Combine(ConfigPath.ConfigDir, RTSCameraSubModule.OldModuleId);
    }*/

    public abstract class RTSCameraConfigBase<T> : MissionConfigBase<T> where T : RTSCameraConfigBase<T>
    {

    }
}
