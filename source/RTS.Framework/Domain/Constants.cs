namespace RTS.Framework.Domain
{
    public static class Constants
    {
        public enum GameKeyEnum
        {
            Pause,
            SlowMotion,
            FreeCamera,
            DisableDeath,
            ControlTroop,
            ToggleHUD,
            SwitchTeam,
            SelectCharacter,
            CameraMoveForward,
            CameraMoveBackward,
            CameraMoveLeft,
            CameraMoveRight,
            CameraMoveUp,
            CameraMoveDown,
            SelectFormation,
            NumberOfGameKeyEnums
        }

        public const string ModuleId = "RTSCamera";

        public const string RTSCameraHotKeyCategoryId = "RTSCameraHotKey";

    }
}
