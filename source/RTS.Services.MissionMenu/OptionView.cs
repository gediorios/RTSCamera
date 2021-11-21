using System;
using MissionLibrary.Event;
using MissionLibrary.HotKey;
using MissionLibrary.View;
using RTS.Engine.InputSystem.Constants;
using RTS.Framework.Domain;

namespace MissionSharedLibrary.View
{
    public class OptionView : MissionMenuViewBase
    {
        public OptionView(int viewOrderPriority, Version version)
            : base(viewOrderPriority, "MissionLibrary" + nameof(OptionView) + "-" + version)
        {
        }

        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);
            var openMenuKey = AGameKeyCategoryManager.Get().GetCategory(Constants.RTSCameraHotKeyCategoryId).GetGameKeySequence((int)GeneralGameKey.OpenMenu);

            if (IsActivated)
            {
                if (openMenuKey.IsKeyPressed(GauntletLayer.Input))
                    DeactivateMenu();
            }
            else if (openMenuKey.IsKeyPressed(Input))
                ActivateMenu();
        }

        public override void OnMissionScreenFinalize()
        {
            base.OnMissionScreenFinalize();

            MissionEvents.Clear();
            AMenuManager.Get().MenuClassCollection.Clear();
        }

        protected override MissionMenuVMBase GetDataSource()
        {
            return new OptionVM(AMenuManager.Get().MenuClassCollection, OnCloseMenu);
        }
    }
}
