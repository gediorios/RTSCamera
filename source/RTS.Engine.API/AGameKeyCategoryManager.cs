using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MissionLibrary.Config.HotKey;
using MissionLibrary.Provider;
using MissionSharedLibrary.Config.HotKey;
using MissionSharedLibrary.HotKey.Category;
using MissionSharedLibrary.Provider;
using RTS.Framework.Domain;
using RTSCamera.CommandSystem.Config.HotKey;
using RTSCamera.Config.HotKey;
using TaleWorlds.InputSystem;
using static RTS.Framework.Domain.Constants;

namespace MissionLibrary.HotKey
{
    public abstract class AGameKeyCategoryManager : ATag<AGameKeyCategoryManager>
    {
        public const string CategoryId = Constants.DefaultHotKeyCategoryId;//nameof(MissionLibrary) + nameof(GeneralGameKey);

        public static AGameKeyCategoryManager Get()
        {
            return RTSEngineState.GetProvider<AGameKeyCategoryManager>();
        }

        public static AGameKeyCategory GeneralGameKeyCategory => Get()?.GetCategory(CategoryId);

        public static GameKeyCategory CreateGameKeysCategory()
        {
            var categoryId = Constants.DefaultHotKeyCategoryId;
            var nativeCategory = HotKeyManager.GetCategory("CombatHotKeyCategory");
            var result = new GameKeyCategory(Constants.DefaultHotKeyCategoryId, (int)Constants.GameKeyEnum.NumberOfGameKeyEnums, GameKeyConfig.Get());

            result.AddGameKeySequence(new GameKeySequence((int)GameKeyEnum.OpenMenu,
                nameof(GameKeyEnum.OpenMenu),
                categoryId, new List<InputKey>()
                {
                    InputKey.L
                }, true));

            result.AddGameKeySequence(new GameKeySequence((int)GameKeyEnum.SelectFormation,
                nameof(GameKeyEnum.SelectFormation),
                categoryId, new List<InputKey>
                {
                    InputKey.MiddleMouseButton
                }));

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

        public static void RegisterGameKeys()
        {
            var AGameKeyCategoryManager = Get();

            if (AGameKeyCategoryManager != null) 
                AGameKeyCategoryManager.AddCategory(ObjectVersion<AGameKeyCategory>.Create(() => CreateGameKeysCategory(), new Version(1, 0)));
        }

        public abstract Dictionary<string, IObjectVersion<AGameKeyCategory>> Categories { get; }
        public abstract void AddCategory(IObjectVersion<AGameKeyCategory> category, bool addOnlyWhenMissing = true);
        public abstract AGameKeyCategory GetCategory(string categoryId);

        public abstract T GetCategory<T>(string categoryId) where T : AGameKeyCategory;
        public abstract void Save();
    }
}
