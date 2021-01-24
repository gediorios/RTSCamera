﻿using MissionLibrary.Provider;
using MissionLibrary.View;
using MissionSharedLibrary.Provider;
using MissionSharedLibrary.View.ViewModelCollection;
using MissionSharedLibrary.View.ViewModelCollection.Options;
using MissionSharedLibrary.View.ViewModelCollection.Options.Selection;
using RTSCamera.Logic;
using RTSCamera.View;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RTSCamera.Config
{
    public class RTSCameraOptionClassFactory
    {
        public static IIdProvider<AOptionClass> CreateOptionClassProvider(IMenuClassCollection menuClassCollection)
        {
            return IdProviderCreator.Create(() =>
            {
                var optionClass = new OptionClass(RTSCameraSubModule.ModuleId,
                    GameTexts.FindText("str_rts_camera_option_class"), menuClassCollection);
                var rtsCameraLogic = Mission.Current.GetMissionBehaviour<RTSCameraLogic>();
                var selectCharacterView = Mission.Current.GetMissionBehaviour<RTSCameraSelectCharacterView>();
                var hideHudView = Mission.Current.GetMissionBehaviour<HideHUDView>();
                var missionScreen = selectCharacterView.MissionScreen;
                var menuManager = AMenuManager.Get();

                var cameraOptionCategory = new OptionCategory("Camera", GameTexts.FindText("str_rts_camera_camera_options"));
                cameraOptionCategory.AddOption(new ActionOptionViewModel(
                    GameTexts.FindText("str_rts_camera_switch_free_camera"),
                    GameTexts.FindText("str_rts_camera_switch_free_camera_hint"),
                    () =>
                    {
                        rtsCameraLogic.SwitchFreeCameraLogic.SwitchCamera();
                        menuManager.RequestToCloseMenu();
                    }));
                cameraOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_use_free_camera_by_default"),
                    GameTexts.FindText("str_rts_camera_use_free_camera_by_default_hint"),
                    () => RTSCameraConfig.Get().UseFreeCameraByDefault,
                    b => RTSCameraConfig.Get().UseFreeCameraByDefault = b));
                cameraOptionCategory.AddOption(new NumericOptionViewModel(
                    GameTexts.FindText("str_rts_camera_raised_height_after_switching_to_free_camera"),
                    GameTexts.FindText("str_rts_camera_raised_height_hint"), () =>
                    {
                        return RTSCameraConfig.Get().RaisedHeight;
                    },
                    f =>
                    {
                        RTSCameraConfig.Get().RaisedHeight = f;
                    }, 0, 50, true, true));
                cameraOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_constant_speed"),
                    GameTexts.FindText("str_rts_camera_constant_speed_hint"), () => RTSCameraConfig.Get().ConstantSpeed,
                    b =>
                    {
                        RTSCameraConfig.Get().ConstantSpeed = b;
                        var view = Mission.Current.GetMissionBehaviour<FlyCameraMissionView>();
                        if (view != null)
                            view.ConstantSpeed = b;
                    }));
                cameraOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_ignore_terrain"),
                    GameTexts.FindText("str_rts_camera_ignore_terrain_hint"), () => RTSCameraConfig.Get().IgnoreTerrain,
                    b =>
                    {
                        RTSCameraConfig.Get().IgnoreTerrain = b;
                        var view = Mission.Current.GetMissionBehaviour<FlyCameraMissionView>();
                        if (view != null)
                            view.IgnoreTerrain = b;
                    }));
                cameraOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_ignore_boundaries"),
                        GameTexts.FindText("str_rts_camera_ignore_boundaries_hint"),
                        () => RTSCameraConfig.Get().IgnoreBoundaries,
                        b =>
                        {
                            RTSCameraConfig.Get().IgnoreBoundaries = b;
                            var view = Mission.Current.GetMissionBehaviour<FlyCameraMissionView>();
                            if (view != null)
                                view.IgnoreBoundaries = b;
                        }));
                optionClass.AddOptionCategory(0, cameraOptionCategory);

                var controlOptionCategory = new OptionCategory("Control",
                    GameTexts.FindText("str_rts_camera_control_options"));
                controlOptionCategory.AddOption(new SelectionOptionViewModel(
                    GameTexts.FindText("str_rts_camera_player_formation"),
                    GameTexts.FindText("str_rts_camera_player_formation_hint"), new SelectionOptionData(
                        i =>
                        {
                            var config = RTSCameraConfig.Get();
                            if ((i != config.PlayerFormation || config.AlwaysSetPlayerFormation) &&
                                i >= 0 && i < (int) FormationClass.NumberOfAllFormations)
                            {
                                config.PlayerFormation = i;
                                rtsCameraLogic.SwitchFreeCameraLogic.CurrentPlayerFormation = (FormationClass) i;
                                Utility.SetPlayerFormation((FormationClass) i);
                            }
                        }, () => RTSCameraConfig.Get().PlayerFormation,
                        (int) FormationClass.NumberOfRegularFormations, new[]
                        {
                            new SelectionItem(true, "str_troop_group_name", "0"),
                            new SelectionItem(true, "str_troop_group_name", "1"),
                            new SelectionItem(true, "str_troop_group_name", "2"),
                            new SelectionItem(true, "str_troop_group_name", "3"),
                            new SelectionItem(true, "str_troop_group_name", "4"),
                            new SelectionItem(true, "str_troop_group_name", "5"),
                            new SelectionItem(true, "str_troop_group_name", "6"),
                            new SelectionItem(true, "str_troop_group_name", "7")
                        }), true));
                controlOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_always_set_player_formation"),
                    GameTexts.FindText("str_rts_camera_always_set_player_formation_hint"),
                    () => RTSCameraConfig.Get().AlwaysSetPlayerFormation,
                    b =>
                    {
                        var config = RTSCameraConfig.Get();
                        config.AlwaysSetPlayerFormation = b;
                        var formationClass = (FormationClass) config.PlayerFormation;
                        rtsCameraLogic.SwitchFreeCameraLogic.CurrentPlayerFormation = formationClass;
                        Utility.SetPlayerFormation(formationClass);
                    }));
                controlOptionCategory.AddOption(new SelectionOptionViewModel(
                    GameTexts.FindText("str_rts_camera_watch_another_hero"),
                    GameTexts.FindText("str_rts_camera_watch_another_hero_hint"),
                    new WatchAgentSelectionData(missionScreen).SelectionOptionData, true));
                controlOptionCategory.AddOption(new ActionOptionViewModel(
                    GameTexts.FindText("str_rts_camera_select_character"),
                    GameTexts.FindText("str_rts_camera_select_character_hint"),
                    () =>
                    {
                        selectCharacterView.IsSelectingCharacter = true;
                        menuManager.RequestToCloseMenu();
                    }));
                controlOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_control_ally_after_death"),
                    GameTexts.FindText("str_rts_camera_control_ally_after_death_hint"),
                    () => RTSCameraConfig.Get().ControlAllyAfterDeath,
                    b => RTSCameraConfig.Get().ControlAllyAfterDeath = b));
                controlOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_prefer_to_control_companions"),
                    GameTexts.FindText("str_rts_camera_prefer_to_control_companions_hint"),
                    () => RTSCameraConfig.Get().PreferToControlCompanions,
                    b => RTSCameraConfig.Get().PreferToControlCompanions = b));
                controlOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_control_troops_in_player_party_only"),
                    GameTexts.FindText("str_rts_camera_control_troops_in_player_party_only_hint"),
                    () => RTSCameraConfig.Get().ControlTroopsInPlayerPartyOnly,
                    b => RTSCameraConfig.Get().ControlTroopsInPlayerPartyOnly = b));
                optionClass.AddOptionCategory(0, controlOptionCategory);

                var miscellaneousOptionCategory = new OptionCategory("Miscellaneous",
                    GameTexts.FindText("str_rts_camera_miscellaneous_options"));
                miscellaneousOptionCategory.AddOption(new ActionOptionViewModel(GameTexts.FindText("str_rts_camera_toggle_pause"), GameTexts.FindText("str_rts_camera_toggle_pause_hint"),
                    () =>
                    {
                        rtsCameraLogic.MissionSpeedLogic?.TogglePause();
                        menuManager.RequestToCloseMenu();
                    }));
                miscellaneousOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_slow_motion_mode"),
                    GameTexts.FindText("str_rts_camera_slow_motion_hint"), () => Mission.Current.Scene.SlowMotionMode,
                    b => rtsCameraLogic.MissionSpeedLogic.SetSlowMotionMode(b)));
                miscellaneousOptionCategory.AddOption(new NumericOptionViewModel(
                    GameTexts.FindText("str_rts_camera_slow_motion_factor"),
                    GameTexts.FindText("str_rts_camera_slow_motion_factor_hint"),
                    () => Mission.Current.Scene.SlowMotionFactor,
                    f => rtsCameraLogic.MissionSpeedLogic.SetSlowMotionFactor(f), 0, 3, false, true));
                miscellaneousOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_display_mod_message"),
                    GameTexts.FindText("str_rts_camera_display_message_hint"),
                    () => RTSCameraConfig.Get().DisplayMessage, b =>
                    {
                        RTSCameraConfig.Get().DisplayMessage = b;
                        MissionSharedLibrary.Utility.ShouldDisplayMessage = b;
                    }));
                miscellaneousOptionCategory.AddOption(new ActionOptionViewModel(GameTexts.FindText("str_rts_camera_toggle_ui"), GameTexts.FindText("str_rts_camera_toggle_ui_hint"),
                    () =>
                    {
                        hideHudView?.ToggleUI();
                        menuManager.RequestToCloseMenu();
                    }));
                optionClass.AddOptionCategory(1, miscellaneousOptionCategory);

                if (NativeConfig.CheatMode)
                {
                    var cheatOptionCategory = new OptionCategory("Cheat",
                        GameTexts.FindText("str_rts_camera_unbalanced_options_description"));
                    cheatOptionCategory.AddOption(new BoolOptionViewModel(
                        GameTexts.FindText("str_rts_camera_all_invulnerable"),
                        GameTexts.FindText("str_rts_camera_all_invulnerable_hint"),
                        () => RTSCameraConfig.Get().DisableDeath,
                        b =>
                        {
                            RTSCameraConfig.Get().DisableDeath = b;
                            rtsCameraLogic.DisableDeathLogic.SetDisableDeath(b);
                        }));
                    cheatOptionCategory.AddOption(new BoolOptionViewModel(
                        GameTexts.FindText("str_rts_camera_enable_all_invulnerable_hotkey"), null,
                        () => RTSCameraConfig.Get().DisableDeathHotkeyEnabled,
                        b => RTSCameraConfig.Get().DisableDeathHotkeyEnabled = b));
                    cheatOptionCategory.AddOption(new ActionOptionViewModel(GameTexts.FindText("str_rts_camera_switch_team"), GameTexts.FindText("str_rts_camera_switch_team_hint"),
                        () =>
                        {
                            rtsCameraLogic.SwitchTeamLogic.SwapTeam();
                            menuManager.RequestToCloseMenu();
                        }));
                    cheatOptionCategory.AddOption(new BoolOptionViewModel(
                        GameTexts.FindText("str_rts_camera_enable_switch_team_hotkey"), null,
                        () => RTSCameraConfig.Get().SwitchTeamHotkeyEnabled,
                        b => RTSCameraConfig.Get().SwitchTeamHotkeyEnabled = b));
                    optionClass.AddOptionCategory(1, cheatOptionCategory);
                }

                return optionClass;
            }, RTSCameraSubModule.ModuleId);
        }
    }
}
