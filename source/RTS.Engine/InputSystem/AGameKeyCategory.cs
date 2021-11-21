using MissionLibrary.Provider;
using System.Collections.Generic;

namespace MissionLibrary.HotKey
{
    public abstract class AGameKeyCategory : ATag<AGameKeyCategory>
    {
        public abstract string GameKeyCategoryId { get; }

        public abstract List<IGameKeySequence> GameKeySequences { get; }

        public abstract IGameKeySequence GetGameKeySequence(int i);

        public abstract void Save();

        public abstract void Load();

    }
}
