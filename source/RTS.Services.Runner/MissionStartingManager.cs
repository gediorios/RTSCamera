﻿using MissionLibrary;
using System.Collections.Generic;
using MissionLibrary.Controller;
using MissionSharedLibrary.Provider;
using System;
using System.Linq;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using MissionLibrary.Provider;

namespace MissionSharedLibrary.Controller
{
    public class MissionStartingManager : AMissionStartingManager
    {
        private readonly List<AMissionStartingHandler> _handlers = new List<AMissionStartingHandler>();

        private readonly Dictionary<string, AMissionStartingHandler> _dictionary =
            new Dictionary<string, AMissionStartingHandler>();

        public static void AddMissionBehaviour(MissionView entranceView, MissionBehavior behaviour)
        {
            behaviour.OnAfterMissionCreated();
            entranceView.Mission.AddMissionBehavior(behaviour);
        }

        public override void OnCreated(MissionView entranceView)
        {
            foreach (var handler in GetHandlers())
            {
                handler.OnCreated(entranceView);
            }
        }

        public override void OnPreMissionTick(MissionView entranceView, float dt)
        {
            foreach (var handler in GetHandlers())
            {
                handler.OnPreMissionTick(entranceView, dt);
            }
        }

        public override void AddHandler(AMissionStartingHandler handler)
        {
            _handlers.Add(handler);
        }

        public override void AddHandler(string key, AMissionStartingHandler handler, Version version)
        {
            RTSEngineState.RegisterProvider(ObjectVersion<AMissionStartingHandler>.Create(() => handler, version), key);
        }

        private IEnumerable<AMissionStartingHandler> GetHandlers()
        {
            return _handlers.Concat(RTSEngineState.GetProviders<AMissionStartingHandler>());
        }
    }
}
