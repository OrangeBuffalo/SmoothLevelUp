using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using HarmonyLib;

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

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeXpRequiredForSkillLevel")]
    static class PatchXPRequiredForSkillLevel
    {
        public static bool Prefix(ref int[] ____xpRequiredForSkillLevel)
        {
            int num = 1000;
            float k = 0.5f;
            ____xpRequiredForSkillLevel = new int[1024];
            ____xpRequiredForSkillLevel[0] = num;
            for (int i = 1; i < 1024; i++)
            {
                if (i >= 300)
                {
                    k = 1.0f;
                }
                double num2 = Math.Round((10 + i) * k);
                num += (int)num2;
                ____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i - 1] + num;
            }
            return false;
        }
    }
}