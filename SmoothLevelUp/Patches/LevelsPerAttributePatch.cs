using System;

using TaleWorlds.CampaignSystem.SandBox.GameComponents;

using HarmonyLib;

namespace SmoothLevelUp.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
    public static class PatchLevelsPerAttributePoint
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.LevelsPerAttribute;
        }
    }
}
