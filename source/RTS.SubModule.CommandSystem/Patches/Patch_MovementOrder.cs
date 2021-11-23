﻿using MissionSharedLibrary.Utilities;
using RTSCamera.CommandSystem.Config;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.MovementOrder;

namespace RTSCamera.CommandSystem.Patch
{
    //[HarmonyLib.HarmonyPatch(typeof(MovementOrder), "Tick")]
    public class Patch_MovementOrder
    {
        public static bool GetSubstituteOrder_Prefix(MovementOrder __instance, ref MovementOrderEnum movementOrderEnum, ref MovementOrder __result,
            Formation formation)
        {
            if (__instance.OrderType == OrderType.ChargeWithTarget && CommandSystemConfig.Get().AttackSpecificFormation)
            {
                var position = formation.QuerySystem.MedianPosition;
                position.SetVec2(formation.CurrentPosition);
                if (formation.Team == Mission.Current.PlayerTeam)
                {
                    Utility.DisplayFormationReadyMessage(formation);
                }
                __result = MovementOrder.MovementOrderMove(position);
                return false;
            }

            return true;
        }

        public static bool SetChargeBehaviorValues_Prefix(Agent unit)
        {
            if (Utility.ShouldChargeToFormation(unit))
            {
                Utility.SetUnitAIBehaviorWhenChargeToFormation(unit);
                return false;
            }

            return true;
        }
    }
}
