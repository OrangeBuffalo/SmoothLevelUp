using System;

using TaleWorlds.CampaignSystem.SandBox.GameComponents;

using HarmonyLib;

namespace SmoothLevelUp.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeXpRequiredForSkillLevel")]
    static class XpRequiredForSkillLevelPatch
    {
        public static bool Prefix(ref int[] ____xpRequiredForSkillLevel)
        {
            float a;
            float b;

            switch (Settings.Instance.SkillProgression.SelectedValue)
            {
                case "Smooth":
                    a = 500f;
                    b = 1.75f;
                    break;
                case "Slower":
                    a = 1000f;
                    b = 1.825f;
                    break;
                case "Faster":
                    a = 250f;
                    b = 1.6f;
                    break;
                default:
                    a = 500f;
                    b = 1.75f;
                    break;
            }

            ____xpRequiredForSkillLevel = new int[1024];
            ____xpRequiredForSkillLevel[0] = (int)a;

            for (int i = 1; i < 1024; i++)
            {
                ____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i-1] + (int)(Math.Round(a + Math.Pow(i, b)));
            }

            return false;
        }
    }
}
