﻿using System.Collections.Generic;
using TaleWorlds.MountAndBlade;

namespace MissionLibrary.Extension
{
    public interface IMissionExtension : IRTSMissionExtension
    {
        void OpenExtensionMenu(Mission mission);

        string ExtensionName { get; }
        string ButtonName { get; }

        List<MissionBehavior> CreateMissionBehaviours(Mission mission);
    }
}
