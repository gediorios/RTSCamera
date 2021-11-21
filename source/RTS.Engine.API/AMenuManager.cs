﻿using System;
using MissionLibrary.Provider;
using TaleWorlds.MountAndBlade.View.Missions;

namespace MissionLibrary.View
{
    public abstract class AMenuManager : ATag<AMenuManager>
    {
        public static AMenuManager Get()
        {
            return RTSEngineState.GetProvider<AMenuManager>();
        }

        public event Action OnMenuClosedEvent;

        public void OnMenuClosed()
        {
            OnMenuClosedEvent?.Invoke();
        }
        
        public abstract IMenuClassCollection MenuClassCollection { get; }
        public abstract MissionView CreateMenuView();
        public abstract MissionView CreateGameKeyConfigView();
        public abstract void RequestToOpenMenu();
        public abstract void RequestToCloseMenu();
    }
}
