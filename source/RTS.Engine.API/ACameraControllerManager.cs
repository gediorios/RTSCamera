using MissionLibrary.Provider;

namespace MissionLibrary.Controller.Camera
{
    public abstract class ACameraControllerManager : ATag<ACameraControllerManager>
    {
        public static ACameraControllerManager Get()
        {
            return RTSEngineState.GetProvider<ACameraControllerManager>();
        }

        public abstract ICameraController Instance { get; set; }
        public abstract void Clear();
    }
}
