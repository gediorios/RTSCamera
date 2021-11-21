using MissionLibrary.HotKey;
using MissionSharedLibrary.Config.HotKey;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;

namespace MissionSharedLibrary.HotKey.Category
{
    public class GameKeyCategory : AGameKeyCategory
    {
        public override List<IGameKeySequence> GameKeySequences { get; }

        public override string GameKeyCategoryId { get; }

        private readonly IGameKeyConfig _config;

        public override IGameKeySequence GetGameKeySequence(int i)
        {
            return GetKeySequence(i);
        }

        public IGameKeySequence GetKeySequence(int i)
        {
            if (GameKeySequences == null || i < 0 || i >= GameKeySequences.Count)
            {
                return new GameKeySequence(0, "", "", new List<InputKey>());
            }

            return GameKeySequences[i];
        }

        public SerializedGameKeyCategory ToSerializedGameKeyCategory()
        {
            return new SerializedGameKeyCategory
            {
                CategoryId = GameKeyCategoryId,
                GameKeySequences = GameKeySequences.Select(sequence => sequence.ToSerializedGameKeySequence()).ToList()
            };
        }

        public void FromSerializedGameKeyCategory(SerializedGameKeyCategory category)
        {
            var dictionary = category.GameKeySequences.ToDictionary(serializedGameKey => serializedGameKey.StringId);
            for (var i = 0; i < category.GameKeySequences.Count; i++)
            {
                var gameKeySequence = GameKeySequences[i];
                if (dictionary.TryGetValue(gameKeySequence.StringId, out SerializedGameKeySequence serializedGameKeySequence))
                {
                    GameKeySequences[i].SetGameKeys(serializedGameKeySequence.KeyboardKeys);
                }
            }
        }

        public override void Save()
        {
            _config.Category = ToSerializedGameKeyCategory();
            _config.Serialize();
        }

        public override void Load()
        {
            _config.Deserialize();
            FromSerializedGameKeyCategory(_config.Category);
        }

        public GameKeyCategory(string categoryId, int gameKeysCount, IGameKeyConfig config)
        {
            GameKeyCategoryId = categoryId;
            _config = config;

            GameKeySequences = new List<IGameKeySequence>(gameKeysCount);
            for (int index = 0; index < gameKeysCount; ++index)
                GameKeySequences.Add(null);
        }

        public void AddGameKeySequence(GameKeySequence gameKeySequence)
        {
            if (gameKeySequence.Id < 0 || gameKeySequence.Id >= GameKeySequences.Count)
                return;

            GameKeySequences[gameKeySequence.Id] = gameKeySequence;
        }
    }
}
