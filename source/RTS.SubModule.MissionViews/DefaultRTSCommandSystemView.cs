using TaleWorlds.MountAndBlade.View.Missions;

namespace MissionLibrary.Controller.MissionBehaviors
{
    [DefaultView]
    class DefaultRTSCommandSystemView : MissionView
    {
        public override void OnCreated()
        {
            base.OnCreated();

            RTSEngineState.GetProvider<AMissionStartingManager>().OnCreated(this);
        }

        public override void OnPreMissionTick(float dt)
        {
            base.OnPreMissionTick(dt);

            RTSEngineState.GetProvider<AMissionStartingManager>().OnPreMissionTick(this, dt);

            var self = Mission.GetMissionBehavior<DefaultRTSCommandSystemView>();
            if (self == this)
            {
                Mission.RemoveMissionBehavior(self);
            }
        }
    }
}
