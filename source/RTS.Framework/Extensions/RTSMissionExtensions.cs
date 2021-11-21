using System.Collections.Generic;

namespace MissionLibrary.Extension
{
    public static class RTSMissionExtensions
    {
        public static List<IRTSMissionExtension> Extensions { get; } = new List<IRTSMissionExtension>();

        public static void AddExtension(IRTSMissionExtension extension)
        {
            Extensions.Add(extension);
        }

        public static void Clear()
        {
            Extensions.Clear();
        }
    }
}
