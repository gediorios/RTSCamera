using MissionLibrary.HotKey;
using MissionSharedLibrary.Utilities;
using RTS.Engine.InputSystem.Constants;
using TaleWorlds.MountAndBlade;

namespace MissionSharedLibrary.Controller.MissionBehaviors
{
    public class InputController : MissionLogic
    {
        public override void OnMissionTick(float dt)
        {
            base.OnMissionTick(dt);

            if (AGameKeyCategoryManager.GetKey(GeneralGameKey.OpenMenu).IsKeyPressed(Mission.InputManager))
            {
                Utility.DisplayMessage("L pressed.");
            }
        }
    }
}
