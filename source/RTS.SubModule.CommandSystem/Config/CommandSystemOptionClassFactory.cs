using MissionLibrary.Provider;
using MissionLibrary.View;
using MissionSharedLibrary.Provider;
using MissionSharedLibrary.View.ViewModelCollection;
using MissionSharedLibrary.View.ViewModelCollection.Options;
using RTS.Framework.Domain;
using RTSCamera.CommandSystem.Logic;
using RTSCamera.CommandSystem.Patch;
using RTSCamera.CommandSystem.View;
using RTSCamera.Config;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace RTSCamera.CommandSystem.Config
{
    public class CommandSystemOptionClassFactory
    {
        public static IObjectIdentitfication<AOptionClass> CreateOptionClassProvider(IMenuClassCollection menuClassCollection)
        {
            var optName = "CommandsOptionClass";

            return ObjectIdentitfication<AOptionClass>.Create(() =>
            {
                var contourView = Mission.Current.GetMissionBehavior<CommandSystemLogic>().FormationColorSubLogic;

                var optionClass = new OptionClass(optName, GameTexts.FindText("str_rts_camera_command_system_option_class"), menuClassCollection);
                var commandOptionCategory = new OptionCategory("Command", GameTexts.FindText("str_rts_camera_command_system_command_system_options"));

                commandOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_command_system_click_to_select_formation"),
                    GameTexts.FindText("str_rts_camera_command_system_click_to_select_formation_hint"),
                    () => RTSCameraConfig.Get().ClickToSelectFormation, b =>
                    {
                        RTSCameraConfig.Get().ClickToSelectFormation = b;
                        contourView?.SetEnableContourForSelectedFormation(b);
                    }));

                commandOptionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_rts_camera_command_system_attack_specific_formation"),
                    GameTexts.FindText("str_rts_camera_command_system_attack_specific_formation_hint"),
                    () => RTSCameraConfig.Get().AttackSpecificFormation, b =>
                    {
                        RTSCameraConfig.Get().AttackSpecificFormation = b;

                        if (b)
                            PatchChargeToFormation.Patch();
                    }));

                optionClass.AddOptionCategory(0, commandOptionCategory);

                return optionClass;
            }, optName);
        }
    }
}
