using System;

using TaleWorlds.CampaignSystem.SandBox.GameComponents;

using HarmonyLib;

namespace SmoothLevelUp.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeSkillsRequiredForLevel")]
    static class SkillsRequiredForLevelPatch
    {
        public static bool Prefix(ref int[] ____skillsRequiredForLevel)
        {
            float a;
            float b;

            switch (Settings.Instance.LevelProgression.SelectedValue)
            {
                case "Smooth":
                    a = 2000f;
                    b = 3.6f;
                    break;
                case "Slower":
                    a = 3000f;
                    b = 3.7f;
                    break;
                case "Faster":
                    a = 1000f;
                    b = 3.5f;
                    break;
                default:
                    a = 2000f;
                    b = 3.6f;
                    break;
            }

            ____skillsRequiredForLevel = new int[1024];
            ____skillsRequiredForLevel[0] = 0;
            ____skillsRequiredForLevel[1] = 1;
            int cumulativeSp = 1;
            for (int i = 1; i < 1024; i++)
            {
                double sp = a * i + Math.Pow(i, b);
                cumulativeSp += (int)(Math.Round(sp));
                ____skillsRequiredForLevel[i] = cumulativeSp;
            }
            return false;
        }
    }
}
