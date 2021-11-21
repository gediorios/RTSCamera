﻿using System;
using System.IO;
using System.Xml.Serialization;
using MissionLibrary;
using MissionSharedLibrary.Config;
using MissionSharedLibrary.Utilities;
using RTS.Framework.Domain;
using TaleWorlds.MountAndBlade;

namespace RTSCamera.Config
{
    public class RTSCameraConfig : RTSCameraConfigBase<RTSCameraConfig>
    {
        protected static Version BinaryVersion => new Version(1, 4);

        protected override void UpgradeToCurrentVersion()
        {
            switch (ConfigVersion)
            {
                default:
                    Utility.DisplayLocalizedText("str_rts_camera_config_incompatible");
                    ResetToDefault();
                    Serialize();
                    goto case "1.0";
                case "1.0":
                    ConstantSpeed = false;
                    IgnoreTerrain = false;
                    IgnoreBoundaries = false;
                    goto case "1.4";
                case "1.1":
                case "1.2":
                case "1.3":
                case "1.4":
                    break;
            }

            ConfigVersion = BinaryVersion.ToString(2);
        }

        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool UseFreeCameraByDefault;

        public float RaisedHeight = 10;

        public int PlayerControllerInFreeCamera = (int)Agent.ControllerType.AI;

        public Agent.ControllerType GetPlayerControllerInFreeCamera()
        {
            if (RTSEngineState.WatchMode)
                return Agent.ControllerType.AI;

            return (Agent.ControllerType) PlayerControllerInFreeCamera;
        }

        public int PlayerFormation = 4;

        public bool AlwaysSetPlayerFormation;

        public bool ConstantSpeed;

        public bool IgnoreTerrain;

        public bool IgnoreBoundaries;

        public bool SlowMotionMode;

        public float SlowMotionFactor = 0.2f;

        public bool DisplayMessage = true;

        public bool ControlAllyAfterDeath;

        public bool PreferToControlCompanions;

        public bool ControlTroopsInPlayerPartyOnly = true;

        public bool DisableDeath;

        public bool DisableDeathHotkeyEnabled;

        public bool SwitchTeamHotkeyEnabled;

        public static void OnMenuClosed()
        {
            Get().Serialize();
        }

        protected override void CopyFrom(RTSCameraConfig other)
        {
            ConfigVersion = other.ConfigVersion;
            UseFreeCameraByDefault = other.UseFreeCameraByDefault;
            RaisedHeight = other.RaisedHeight;
            PlayerControllerInFreeCamera = other.PlayerControllerInFreeCamera;
            PlayerFormation = other.PlayerFormation;
            AlwaysSetPlayerFormation = other.AlwaysSetPlayerFormation;
            ConstantSpeed = other.ConstantSpeed;
            IgnoreTerrain = other.IgnoreTerrain;
            IgnoreBoundaries = other.IgnoreBoundaries;
            SlowMotionMode = other.SlowMotionMode;
            SlowMotionFactor = other.SlowMotionFactor;
            DisplayMessage = other.DisplayMessage;
            ControlAllyAfterDeath = other.ControlAllyAfterDeath;
            PreferToControlCompanions = other.PreferToControlCompanions;
            ControlTroopsInPlayerPartyOnly = other.ControlTroopsInPlayerPartyOnly;
            DisableDeath = other.DisableDeath;
            DisableDeathHotkeyEnabled = other.DisableDeathHotkeyEnabled;
            SwitchTeamHotkeyEnabled = other.SwitchTeamHotkeyEnabled;
        }

        [XmlIgnore]
        protected override string SaveName => Path.Combine(ConfigPath.ConfigDir, Constants.ModuleId, nameof(RTSCameraConfig) + ".xml");
    }
}