﻿using MissionLibrary;
using MissionLibrary.Controller;
using MissionLibrary.View;
using MissionSharedLibrary;
using MissionSharedLibrary.Provider;
using RTSCamera.CommandSystem.Config;
using RTSCamera.CommandSystem.Config.HotKey;
using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;

namespace RTSCamera.CommandSystem
{
    public class CommandSystemSubModule : MBSubModuleBase
    {
        public static readonly string ModuleId = "RTSCamera.CommandSystem";
        public static bool EnableChargeToFormationForInfantry = true;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            Initialize();
            EnableChargeToFormationForInfantry =
                TaleWorlds.Engine.Utilities.GetModulesNames().Select(ModuleHelper.GetModuleInfo).FirstOrDefault(info => info.Id == "RealisticBattleAiModule") == null;
            Module.CurrentModule.GlobalTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(ModuleId, "module_strings"));
        }

        private void Initialize()
        {
            if (!Initializer.Initialize(ModuleId))
                return;
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();


            if (!SecondInitialize())
                return;
        }

        private bool SecondInitialize()
        {
            if (!Initializer.SecondInitialize())
                return false;

            CommandSystemGameKeyCategory.RegisterGameKeyCategory();
            AMenuManager.Get().OnMenuClosedEvent += CommandSystemConfig.OnMenuClosed;
            var menuClassCollection = AMenuManager.Get().MenuClassCollection;
            menuClassCollection.AddOptionClass(CommandSystemOptionClassFactory.CreateOptionClassProvider(menuClassCollection));
            Global.GetProvider<AMissionStartingManager>().AddHandler(new CommandSystemMissionStartingHandler());
            Global.RegisterProvider(
                VersionProviderCreator.Create(() => new RTSCameraAgentComponent.MissionStartingHandler(),
                    new Version(1, 0, 0)), "RTSCameraAgentComponent.MissionStartingHandler");
            return true;
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(ModuleId, "module_strings"));
            game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(ModuleId, "MissionLibrary"));
        }
    }
}
