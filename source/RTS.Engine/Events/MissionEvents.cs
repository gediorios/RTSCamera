using System;
using TaleWorlds.MountAndBlade;

namespace RTS.Engine.Events
{
    public static class MissionEvents
    {
        public static event Action<Agent> MainAgentWillBeChangedToAnotherOne;

        public static void Clear()
        {
            MainAgentWillBeChangedToAnotherOne = null;
        }

        public static void OnMainAgentWillBeChangedToAnotherOne(Agent newAgent)
        {
            MainAgentWillBeChangedToAnotherOne?.Invoke(newAgent);
        }
    }
}
