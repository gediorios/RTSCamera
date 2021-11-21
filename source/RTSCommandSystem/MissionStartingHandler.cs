using MissionLibrary;
using MissionLibrary.Controller;
using MissionSharedLibrary.Controller;
using RTSCamera.CommandSystem.Config;
using RTSCamera.CommandSystem.Logic;
using RTSCamera.CommandSystem.Patch;
using RTSCamera.CommandSystem.View;
using RTSCamera.Logic;
using RTSCamera.View;
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

            List<MissionBehaviour> list = new List<MissionBehaviour>
            {
                new RTSCameraSelectCharacterView(),
                new RTSCameraLogic(),

                new HideHUDView(),
                new FlyCameraMissionView(),

                new CommandSystemLogic(),
                new CommandSystemOrderTroopPlacer(),
                new DragWhenCommandView()
            };

            var config = CommandSystemConfig.Get();

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
