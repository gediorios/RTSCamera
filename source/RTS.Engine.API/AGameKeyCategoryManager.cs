using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MissionLibrary.Config.HotKey;
using MissionLibrary.Provider;
using MissionSharedLibrary.Config.HotKey;
using MissionSharedLibrary.HotKey.Category;
using MissionSharedLibrary.Provider;
using RTS.Engine.InputSystem.Constants;
using TaleWorlds.InputSystem;

namespace MissionLibrary.HotKey
{
    public abstract class AGameKeyCategoryManager : ATag<AGameKeyCategoryManager>
    {
        public const string CategoryId = nameof(MissionLibrary) + nameof(GeneralGameKey);

        public static AGameKeyCategoryManager Get()
        {
            return RTSEngineState.GetProvider<AGameKeyCategoryManager>();
        }

        public static AGameKeyCategory GeneralGameKeyCategory => Get()?.GetCategory(CategoryId);

        [NotNull]
        public static AGameKeyCategory CreateGeneralGameKeyCategory()
        {
            var result = new GameKeyCategory(CategoryId, (int)GeneralGameKey.NumberOfGameKeyEnums, GeneralGameKeyConfig.Get());

            result.AddGameKeySequence(new GameKeySequence((int)GeneralGameKey.OpenMenu,
                nameof(GeneralGameKey.OpenMenu),
                CategoryId, new List<InputKey>()
                {
                    InputKey.L
                }, true));

            return result;
        }

        public static void RegisterGameKeyCategory()
        {
            Get()?.AddCategory(ObjectVersion<AGameKeyCategory>.Create(() => CreateGeneralGameKeyCategory(), new Version(1, 0)));
        }

        public static IGameKeySequence GetKey(GeneralGameKey key)
        {
            return GeneralGameKeyCategory.GetGameKeySequence((int)key);
        }


        public abstract Dictionary<string, IObjectVersion<AGameKeyCategory>> Categories { get; }
        public abstract void AddCategory(IObjectVersion<AGameKeyCategory> category, bool addOnlyWhenMissing = true);
        public abstract AGameKeyCategory GetCategory(string categoryId);

        public abstract T GetCategory<T>(string categoryId) where T : AGameKeyCategory;
        public abstract void Save();
    }
}
