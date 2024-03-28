using BepInEx;
using BepInEx.Logging;
using OptionLimitsBegone.Patchs;

namespace OptionLimitsBegone
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("Lycans.exe")]
    public class OptionLimitsBegone : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "fr.shepardeon.plugins.optionlimitsbegone";
        public const string PLUGIN_NAME = "OptionsLimitsBegone";
        public const string PLUGIN_VERSION = "2.1.1";

        internal static ManualLogSource Log { get; private set; }

        private CountSettingsPatch _countSettingPatch;
        private GameManagerPatch _gameManagerPatch;
        private GameUIPatch _gameUIPatch;

        private void Awake()
        {
            Log = Logger;

            Logger.LogMessage($"{PLUGIN_NAME} is initializing...");

            _countSettingPatch = new();
            _gameManagerPatch = new();
            _gameUIPatch = new();

            _countSettingPatch.Patch();
            _gameManagerPatch.Patch();
            _gameUIPatch.Patch();

            Logger.LogMessage($"{PLUGIN_NAME}'s initialization done!");
        }

        private void OnDestroy()
        {
            Logger.LogMessage("Destroyed - Unpatching...");

            _countSettingPatch.Unpatch();
            _gameManagerPatch.Unpatch();
            _gameUIPatch.Unpatch();

            Logger.LogMessage("Done!");
        }
    }
}
