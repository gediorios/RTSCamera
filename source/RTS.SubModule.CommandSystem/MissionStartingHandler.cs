using MissionLibrary;
using MissionLibrary.Controller;
using MissionLibrary.View;
using MissionSharedLibrary.Controller;
using MissionSharedLibrary.View;
using MissionSharedLibrary.View.HotKey;
using RTSCamera.CommandSystem.Config;
using RTSCamera.CommandSystem.Logic;
using RTSCamera.CommandSystem.Patch;
using RTSCamera.CommandSystem.View;
using RTSCamera.Config;
using RTSCamera.Logic;
using RTSCamera.View;
using RTSCameraAgentComponent;
using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using static MissionLibrary.Controller.AMissionStartingManager;

namespace RTSCamera.MissionStartingHandler
{
    public class MissionStartingHandlerAdder : ADefaultMissionStartingHandlerAdder
    {
        public MissionStartingHandlerAdder()
        {
            RTSEngineState.GetProvider<AMissionStartingManager>().AddHandler(new MissionStartingHandler());
        }
    }

    public class MissionStartingHandler : AMissionStartingHandler
    {
        public override void OnCreated(MissionView entranceView)
        {

            List<MissionBehavior> list = new List<MissionBehavior>
            {
                new RTSCameraSelectCharacterView(),
                new RTSCameraLogic(),

                new OptionView(24, new Version(1, 1, 0)),
                new GameKeyConfigView(),
                new ComponentAdder(),

                new HideHUDView(),
                new FlyCameraMissionView(),

                new CommandSystemLogic(),
                new CommandSystemOrderTroopPlacer(),
                new DragWhenCommandView()
            };


            //MissionStartingManager.AddMissionBehaviour(entranceView, AMenuManager.Get().CreateMenuView());
            //MissionStartingManager.AddMissionBehaviour(entranceView, AMenuManager.Get().CreateGameKeyConfigView());
            //MissionStartingManager.AddMissionBehaviour(entranceView, new ComponentAdder());

            var config = RTSCameraConfig.Get();

            if (config.AttackSpecificFormation)
                PatchChargeToFormation.Patch();

            foreach (var missionBehaviour in list)
            {
                MissionStartingManager.AddMissionBehaviour(entranceView, missionBehaviour);
            }

            /*foreach (var extension in RTSCameraExtension.Extensions)
            {
                foreach (var missionBehaviour in extension.CreateMissionBehaviours(entranceView.Mission))
                {
                    MissionStartingManager.AddMissionBehaviour(entranceView, missionBehaviour);
                }
            }

            foreach (var extension in MissionExtensionCollection.Extensions)
            {
                foreach (var missionBehaviour in extension.CreateMissionBehaviours(entranceView.Mission))
                {
                    MissionStartingManager.AddMissionBehaviour(entranceView, missionBehaviour);
                }
            }*/
        }

        public override void OnPreMissionTick(MissionView entranceView, float dt)
        {
        }
    }
}
