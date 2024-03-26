using BepInEx;
using HarmonyLib;
using OptionLimitsBegone.Patchs;
using System.Linq;

namespace OptionLimitsBegone
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("Lycans.exe")]
    public class OptionLimitsBegone : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "fr.shepardeon.plugins.optionlimitsbegone";
        public const string PLUGIN_NAME = "OptionsLimitsBegone";
        public const string PLUGIN_VERSION = "2.0.0";

        private void Awake()
        {
            Logger.LogMessage($"{PLUGIN_NAME} is initializing...");
            Harmony.CreateAndPatchAll(typeof(CountSettingsPatchs), PLUGIN_GUID);
            Logger.LogMessage($"{PLUGIN_NAME}'s initialization done!");
        }

        private void Start()
        {
            Logger.LogMessage("Filling option dropdowns...");

            var ui = GameManager.Instance.gameUI.GetComponent<GameSettingsUI>();

            ui.alchemistsCountDropdown.AddOptions(Enumerable.Range(2, 4).Select(i => new TMPro.TMP_Dropdown.OptionData
            {
                text = i.ToString(),
            }).ToList());

            ui.huntersCountDropdown.AddOptions(Enumerable.Range(2, 4).Select(i => new TMPro.TMP_Dropdown.OptionData
            {
                text = i.ToString(),
            }).ToList());

            ui.wolvesCountDropdown.AddOptions(Enumerable.Range(3, 3).Select(i => new TMPro.TMP_Dropdown.OptionData
            {
                text = i.ToString(),
            }).ToList());

            Logger.LogMessage("Done!");
        }

        private void OnDestroy()
        {
            Logger.LogMessage("Destroyed - Unpatching...");
            Harmony.UnpatchID(PLUGIN_GUID);
            Logger.LogMessage("Done!");
        }
    }
}
