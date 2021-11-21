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
        public static AGameKeyCategory Category => AGameKeyCategoryManager.Get().GetCategory(Constants.RTSCameraHotKeyCategoryId);

        public static void RegisterGameKeyCategory()
        {
            AGameKeyCategoryManager.Get()?.AddCategory(CreateCategory, new Version(1, 0));
        }

        public static GameKeyCategory CreateCategory()
        {
            var categoryId = Constants.RTSCameraHotKeyCategoryId;
            var nativeCategory = HotKeyManager.GetCategory("CombatHotKeyCategory");
            var result = new GameKeyCategory(Constants.RTSCameraHotKeyCategoryId, (int)Constants.GameKeyEnum.NumberOfGameKeyEnums, GameKeyConfig.Get());

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.Pause, nameof(Constants.GameKeyEnum.Pause),
                categoryId, new List<InputKey>
                {
                    InputKey.OpenBraces
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.SlowMotion,
                nameof(Constants.GameKeyEnum.SlowMotion), categoryId, new List<InputKey>
                {
                    InputKey.Apostrophe
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.FreeCamera,
                nameof(Constants.GameKeyEnum.FreeCamera), categoryId, new List<InputKey>
                {
                    InputKey.F10
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.DisableDeath,
                nameof(Constants.GameKeyEnum.DisableDeath), categoryId, new List<InputKey>
                {
                    InputKey.End
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.ControlTroop,
                nameof(Constants.GameKeyEnum.ControlTroop), categoryId, new List<InputKey>
                {
                    InputKey.F
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.ToggleHUD,
                nameof(Constants.GameKeyEnum.ToggleHUD), categoryId, new List<InputKey>
                {
                    InputKey.CloseBraces
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.SwitchTeam,
                nameof(Constants.GameKeyEnum.SwitchTeam), categoryId, new List<InputKey>
                {
                    InputKey.F11
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.SelectCharacter,
                nameof(Constants.GameKeyEnum.SelectCharacter), categoryId, new List<InputKey>
                {
                    InputKey.SemiColon
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveForward,
                nameof(Constants.GameKeyEnum.CameraMoveForward), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Up")?.KeyboardKey?.InputKey ?? InputKey.W
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveBackward,
                nameof(Constants.GameKeyEnum.CameraMoveBackward), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Down")?.KeyboardKey?.InputKey ?? InputKey.S
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveLeft,
                nameof(Constants.GameKeyEnum.CameraMoveLeft), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Left")?.KeyboardKey?.InputKey ?? InputKey.A
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveRight,
                nameof(Constants.GameKeyEnum.CameraMoveRight), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Right")?.KeyboardKey?.InputKey ?? InputKey.D
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveUp,
                nameof(Constants.GameKeyEnum.CameraMoveUp), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Jump")?.KeyboardKey?.InputKey ?? InputKey.Space
                }));

            result.AddGameKeySequence(new GameKeySequence((int)Constants.GameKeyEnum.CameraMoveDown,
                nameof(Constants.GameKeyEnum.CameraMoveDown), categoryId, new List<InputKey>
                {
                    nativeCategory?.GetGameKey("Crouch")?.KeyboardKey?.InputKey ?? InputKey.X
                }));

            return result;
        }

        public static IGameKeySequence GetKey(Constants.GameKeyEnum key)
        {
            return Category?.GetGameKeySequence((int)key);
        }
    }
}
