﻿using HarmonyLib;
using MissionLibrary;
using MissionLibrary.Controller;
using MissionLibrary.View;
using MissionSharedLibrary;
using MissionSharedLibrary.Utilities;
using MissionSharedLibrary.View;
using RTS.Framework.Domain;
using RTSCamera.CampaignGame.Behavior;
using RTSCamera.CommandSystem.Config;
using RTSCamera.Config;
using RTSCamera.MissionStartingHandler;
using RTSCamera.Patch;
using RTSCamera.Patch.Fix;
using RTSCamera.src.Patch.Fix;
using SandBox;
using SandBox.Source.Objects.SettlementObjects;
using SandBox.Source.Towns;
using System;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Missions.SiegeWeapon;
using TaleWorlds.MountAndBlade.View.Screen;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;
using Module = TaleWorlds.MountAndBlade.Module;

namespace RTS.SubModule.CommandSystem
{
    public class CommandSystemSubModule : MBSubModuleBase
    {
        private readonly Harmony _harmony = new Harmony("RTSCameraPatch");
        private bool _successPatch;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                Initialize();

                Module.CurrentModule.GlobalTextManager.LoadGameTexts(
                    ModuleHelper.GetXmlPath(Constants.ModuleId, "module_strings"));
                /*Module.CurrentModule.GlobalTextManager.LoadGameTexts(
                    ModuleHelper.GetXmlPath(Constants.ModuleId, "MissionLibrary"));*/

                _successPatch = true;
                _harmony.Patch(
                    typeof(Formation).GetMethod("LeaveDetachment", BindingFlags.Instance | BindingFlags.Public),
                    prefix: new HarmonyMethod(
                        typeof(Patch_Formation).GetMethod("LeaveDetachment_Prefix",
                            BindingFlags.Static | BindingFlags.Public)));

                _harmony.Patch(
                    typeof(RangedSiegeWeaponView).GetMethod("HandleUserInput",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(
                        typeof(Patch_RangedSiegeWeaponView).GetMethod("HandleUserInput_Prefix",
                            BindingFlags.Static | BindingFlags.Public)));

                _harmony.Patch(
                    typeof(CommonVillagersCampaignBehavior).GetMethod("CheckIfConversationAgentIsEscortingThePlayer",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(typeof(Patch_CommonVillagersCampaignBehavior).GetMethod(
                        "CheckIfConversationAgentIsEscortingThePlayer_Prefix",
                        BindingFlags.Static | BindingFlags.Public)));

                _harmony.Patch(
                    typeof(ArenaPracticeFightMissionController).GetMethod("StartPractice",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(
                        typeof(Patch_ArenaPracticeFightMissionController).GetMethod("StartPractice_Prefix",
                            BindingFlags.Static | BindingFlags.Public)));
                _harmony.Patch(
                    typeof(PassageUsePoint).GetMethod(nameof(PassageUsePoint.IsDisabledForAgent),
                        BindingFlags.Instance | BindingFlags.Public),
                    new HarmonyMethod(typeof(Patch_PassageUsePoint).GetMethod(
                        nameof(Patch_PassageUsePoint.IsDisabledForAgent_Prefix),
                        BindingFlags.Static | BindingFlags.Public)));
                _harmony.Patch(
                    typeof(TeamAIComponent).GetMethod("TickOccasionally",
                        BindingFlags.Instance | BindingFlags.Public),
                    prefix: new HarmonyMethod(typeof(Patch_TeamAIComponent).GetMethod(
                        nameof(Patch_TeamAIComponent.TickOccasionally_Prefix),
                        BindingFlags.Static | BindingFlags.Public)));

                _harmony.Patch(
                    typeof(MissionAgentLabelView).GetMethod("IsAllyInAllyTeam",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(typeof(Patch_MissionAgentLabelView).GetMethod("IsAllyInAllyTeam_Prefix",
                        BindingFlags.Static | BindingFlags.Public)));
                _harmony.Patch(
                    typeof(MissionBoundaryCrossingHandler).GetMethod("TickForMainAgent",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(
                        typeof(Patch_MissionBoundaryCrossingHandler).GetMethod("TickForMainAgent_Prefix",
                            BindingFlags.Static | BindingFlags.Public)));
                _harmony.Patch(
                    typeof(MissionFormationMarkerVM).GetMethod("RefreshFormationPositions",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(typeof(Patch_MissionFormationMarkerVM).GetMethod(
                        nameof(Patch_MissionFormationMarkerVM.RefreshFormationPositions_Prefix),
                        BindingFlags.Static | BindingFlags.Public)));

                // Use Patch to add game menu
                WatchBattleBehavior.Patch(_harmony);

                var missionListenerOnMissionModeChange = typeof(IMissionListener).GetMethod("OnMissionModeChange", BindingFlags.Instance | BindingFlags.Public);

                var mapping = typeof(MissionScreen).GetInterfaceMap(missionListenerOnMissionModeChange.DeclaringType);
                var index = Array.IndexOf(mapping.InterfaceMethods, missionListenerOnMissionModeChange);
                _harmony.Patch(
                     mapping.TargetMethods[index],
                    prefix: new HarmonyMethod(typeof(Patch_MissionScreen).GetMethod("OnMissionModeChange_Prefix",
                        BindingFlags.Static | BindingFlags.Public)));
            }
            catch (Exception e)
            {
                _successPatch = false;
                MBDebug.ConsolePrint(e.ToString());
            }
        }

        private void Initialize()
        {
            if (!Initializer.Initialize(Constants.ModuleId))
                return;

            Initializer.RegisterProvider(() => new MenuManager(), new Version(1, 1));
            Initializer.RegisterProvider(() => new MissionStartingHandlerAdder(), new Version(1, 0));

            //RTSCameraExtension.Clear();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            if (!SecondInitialize())
                return;

            if (!_successPatch)
            {
                InformationManager.DisplayMessage(new InformationMessage("RTS Camera: patch failed"));
            }

            Patch_MissionOrderGauntletUIHandler.Patch();
            Patch_MissionGauntletCrosshair.Patch(_harmony);
            Utility.ShouldDisplayMessage = RTSCameraConfig.Get().DisplayMessage;
            Utility.PrintUsageHint();
        }

        private bool SecondInitialize()
        {
            if (!Initializer.SecondInitialize())
                return false;

            //RTSCameraGameKeyCategory.RegisterGameKeyCategory();

            /*RTSEngineState.RegisterProvider(
                ObjectVersion<AMissionStartingHandler>.Create(() => new RTSCameraAgentComponent.MissionStartingHandler(),
                    new Version(1, 0, 0)), "RTSCameraAgentComponent.MissionStartingHandler");*/

            //RTSEngineState.GetProvider<AMissionStartingManager>().AddHandler(new MissionStartingHandler.MissionStartingHandler());

            var menuClassCollection = AMenuManager.Get().MenuClassCollection;

            AMenuManager.Get().OnMenuClosedEvent += RTSCameraConfig.OnMenuClosed;
            menuClassCollection.AddOptionClass(RTSCameraOptionClassFactory.CreateOptionClassProvider(menuClassCollection));

            //CommandSystemGameKeyCategory.RegisterGameKeyCategory();

            //AMenuManager.Get().OnMenuClosedEvent += CommandSystemConfig.OnMenuClosed;
            //var menuClassCollection = AMenuManager.Get().MenuClassCollection;
            menuClassCollection.AddOptionClass(CommandSystemOptionClassFactory.CreateOptionClassProvider(menuClassCollection));

            //Global.GetProvider<AMissionStartingManager>().AddHandler(new CommandSystemMissionStartingHandler());
            /*Global.RegisterProvider(
                VersionProviderCreator.Create(() => new RTSCameraAgentComponent.MissionStartingHandler(),
                    new Version(1, 0, 0)), "RTSCameraAgentComponent.MissionStartingHandler");*/

            return true;
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(Constants.ModuleId, "module_strings"));
            //game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(Constants.ModuleId, "MissionLibrary"));
            //AddCampaignBehavior(gameStarterObject);
        }

        private void AddCampaignBehavior(object gameStarter)
        {
            //if (gameStarter is CampaignGameStarter campaignGameStarter)
            //{
            //    campaignGameStarter.AddBehavior(new WatchBattleBehavior());
            //}
        }


        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
            //RTSCameraExtension.Clear();
            //MissionExtensionCollection.Clear();

            _harmony.UnpatchAll(_harmony.Id);
            Initializer.Clear();
        }
    }
}
