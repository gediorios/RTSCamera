﻿using MissionLibrary;
using MissionLibrary.Controller;
using MissionLibrary.View;
using RTSCameraAgentComponent;
using TaleWorlds.MountAndBlade.View.Missions;

namespace MissionSharedLibrary.Controller
{
    public class DefaultMissionStartingHandlerAdder : ADefaultMissionStartingHandlerAdder
    {
        public DefaultMissionStartingHandlerAdder()
        {
            RTSEngineState.GetProvider<AMissionStartingManager>().AddHandler(new DefaultMissionStartingHandler());
        }
    }

    public class DefaultMissionStartingHandler : AMissionStartingManager.AMissionStartingHandler
    {
        public override void OnCreated(MissionView entranceView)
        {
            AddMissionLibraryMissionBehaviors(entranceView);
        }

        public override void OnPreMissionTick(MissionView entranceView, float dt)
        {

        }

        private void AddMissionLibraryMissionBehaviors(MissionView entranceView)
        {
            //MissionStartingManager.AddMissionBehaviour(entranceView,
            //Global.GetProvider<AInputControllerFactory>().CreateInputController(entranceView.Mission));

            MissionStartingManager.AddMissionBehaviour(entranceView, AMenuManager.Get().CreateMenuView());
            MissionStartingManager.AddMissionBehaviour(entranceView, AMenuManager.Get().CreateGameKeyConfigView());
            MissionStartingManager.AddMissionBehaviour(entranceView, new ComponentAdder());
        }
    }
}
