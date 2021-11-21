﻿using MissionLibrary.Provider;
using System;
using TaleWorlds.MountAndBlade.View.Missions;

namespace MissionLibrary.Controller
{
    public abstract class AMissionStartingManager : ATag<AMissionStartingManager>
    {
        public abstract class AMissionStartingHandler : ATag<AMissionStartingHandler>
        {
            public abstract void OnCreated(MissionView entranceView);

            public abstract void OnPreMissionTick(MissionView entranceView, float dt);
        }

        public abstract void OnCreated(MissionView entranceView);

        public abstract void OnPreMissionTick(MissionView entranceView, float dt);

        public abstract void AddHandler(AMissionStartingHandler handler);

        public abstract void AddHandler(string key, AMissionStartingHandler handler, Version version);

    }
}
