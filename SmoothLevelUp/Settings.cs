using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;
using MCM.Abstractions.Dropdown;

namespace SmoothLevelUp
{
    class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "SmoothLevelUp";
        public override string DisplayName => "Smooth Level Up";
        public override string FolderName => "SmoothLevelUp";
        public override string FormatType => "json2";

        [SettingPropertyDropdown("Skill Progression", RequireRestart = false, HintText = "The rate at which skills increase (Default=Smooth).")]
        [SettingPropertyGroup("Smooth Level Up")]
        public DropdownDefault<string> SkillProgression { get; set; } = new DropdownDefault<string>(new string[]
        {
            "Smooth",
            "Slower",
            "Faster"
        }, 0);

        [SettingPropertyDropdown("Level Progression", RequireRestart = false, HintText = "The rate at which character level increases (Default=Smooth).")]
        [SettingPropertyGroup("Smooth Level Up")]
        public DropdownDefault<string> LevelProgression { get; set; } = new DropdownDefault<string>(new string[]
        {
            "Smooth",
            "Slower",
            "Faster"
        }, 0);

        [SettingPropertyInteger("Levels per Attribute", 1, 4, "0 Level(s)", Order = 1, RequireRestart = false, HintText = "The frequence at which you gain attribute points (Default=4).")]
        [SettingPropertyGroup("Smooth Level Up")]
        public int LevelsPerAttribute { get; set; } = 4;
    }
}