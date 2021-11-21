using System;

namespace MissionLibrary.Event
{
    public static class MissionEvents
    {
        public static event Action<bool> ToggleFreeCamera;

        public delegate void SwitchTeamDelegate();


        public static event SwitchTeamDelegate PreSwitchTeam;

        public static event SwitchTeamDelegate PostSwitchTeam;


        public static event Action OnMenuClosedEvent;


        public static void Clear()
        {
            ToggleFreeCamera = null;
            PreSwitchTeam = null;
            PostSwitchTeam = null;
            OnMenuClosedEvent = null;
        }

        public static void OnToggleFreeCamera(bool freeCamera)
        {
            ToggleFreeCamera?.Invoke(freeCamera);
        }

        public static void OnPreSwitchTeam()
        {
            PreSwitchTeam?.Invoke();
        }

        public static void OnPostSwitchTeam()
        {
            PostSwitchTeam?.Invoke();
        }

        public static void OnMissionMenuClosed()
        {
            OnMenuClosedEvent?.Invoke();
        }
    }
}
