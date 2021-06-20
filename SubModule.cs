using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using HarmonyLib;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;
using MCM.Abstractions.Dropdown;

namespace SmoothLevelUp
{
    public class SmoothLevelUp_SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            var harmony = new Harmony("LevelUp");
            harmony.PatchAll();
        }
    }

    class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "SmoothLevelUp";
        public override string DisplayName => "Smooth Level Up";
        public override string FolderName => "SmoothLevelUp";
        public override string FormatType => "json2";

        [SettingPropertyDropdown("Skill Progression", RequireRestart = false, HintText = "Choose between Smooth and Smoother (easier) progression presets.")]
        [SettingPropertyGroup("Smooth Level Up")]
        public DropdownDefault<string> SkillProgression { get; set; } = new DropdownDefault<string>(new string[]
        {
            "Smooth",
            "Smoother"
        }, 0);

        [SettingPropertyInteger("Levels per Attribute", 1, 4, "0 Level(s)", Order = 1, RequireRestart = false, HintText = "The frequence at which you gain attribute points.")]
        [SettingPropertyGroup("Smooth Level Up")]
        public int LevelsPerAttribute { get; set; } = 3;
    }

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeXpRequiredForSkillLevel")]
    static class PatchXPRequiredForSkillLevel
    {
        public static bool Prefix(ref int[] ____xpRequiredForSkillLevel)
        {

            int num;
            float k1;
            float k2;

            switch (Settings.Instance.SkillProgression.SelectedValue)
            {
                case "Smooth":
                    num = 1000;
                    k1 = 0.5f;
                    k2 = 1.0f;
                    break;
                case "Smoother":
                    num = 1000;
                    k1 = 0.25f;
                    k2 = 0.5f;
                    break;
                default:
                    num = 1000;
                    k1 = 0.5f;
                    k2 = 1.0f;
                    break;
            }

            ____xpRequiredForSkillLevel = new int[1024];
            ____xpRequiredForSkillLevel[0] = num;
            float k = k1;
            for (int i = 1; i < 1024; i++)
            {
                if (i >= 275)
                {
                    k = k2;
                }
                double num2 = Math.Round((10 + i) * k);
                num += (int)num2;
                ____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i - 1] + num;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
    public static class PatchLevelsPerAttributePoint
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.LevelsPerAttribute;
        }
    }
}
