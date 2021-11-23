namespace RTS.Framework.Domain
{
    public static class Constants
    {
        public enum GameKeyEnum
        {
            OpenMenu,
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

        public const string ModuleId = "RTSCommandSystem";


        public const string DefaultHotKeyCategoryId = "DefaultCommandSystemHotKeys";

        public const string FreeCameraHotKeyCategoryId = "FreeCameraHotKey";

        public const string FormationCommandsHotKeyCategoryId = "FormationCommandsHotKey";
    }
}
