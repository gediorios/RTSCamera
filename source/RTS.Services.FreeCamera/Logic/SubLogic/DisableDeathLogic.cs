using MissionLibrary.HotKey;
using MissionSharedLibrary.Utilities;
using RTS.Framework.Domain;
using RTSCamera.Config;
using RTSCamera.Config.HotKey;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RTSCamera.Logic.SubLogic
{
    public class DisableDeathLogic
    {
        private readonly RTSCameraLogic _logic;

        private readonly RTSCameraConfig _config = RTSCameraConfig.Get();

        public Mission Mission => _logic.Mission;

        public DisableDeathLogic(RTSCameraLogic logic)
        {
            _logic = logic;
        }

        public void AfterStart()
        {
            SetDisableDeath(_config.DisableDeath, true);
        }

        public void OnMissionTick(float dt)
        {
            if (!NativeConfig.CheatMode)
                return;

            var disableDeathKey = AGameKeyCategoryManager.Get().GetCategory(Constants.RTSCameraHotKeyCategoryId).GetGameKeySequence((int)Constants.GameKeyEnum.DisableDeath);

            if (disableDeathKey.IsKeyPressed(Mission.InputManager))
            {
                _config.DisableDeath = !_config.DisableDeath;
                SetDisableDeath(_config.DisableDeath);
            }
        }

        public void SetDisableDeath(bool disableDeath, bool atStart = false)
        {
            if (!NativeConfig.CheatMode)
                return;

            Mission.DisableDying = disableDeath;
            if (atStart && !disableDeath)
                return;

            PrintDeathStatus(disableDeath);
        }

        private void PrintDeathStatus(bool disableDeath)
        {
            Utility.DisplayLocalizedText(disableDeath ? "str_rts_camera_death_disabled" : "str_rts_camera_death_enabled");
        }
    }
}
