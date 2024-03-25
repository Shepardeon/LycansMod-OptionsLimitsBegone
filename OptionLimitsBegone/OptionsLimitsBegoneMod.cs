using MelonLoader;
using OptionLimitsBegone.Patchs;
using UnityEngine;

namespace OptionLimitsBegone
{
    public class OptionsLimitsBegoneMod : MelonMod
    {
        public const string GAME = "Lycans";
        public const string DEVELOPER = "OnuGames";

        public const string NAME = "Option Limits Begone";
        public const string AUTHOR = "Shepardeon";
        public const string VERSION = "1.0.0";

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            GameSettingsUI ui = GameObject.Find("GameUI").GetComponent<GameSettingsUI>();

            HunterCountPatch.FillGameUI(ui);
            WolvesCountPatch.FillGameUI(ui);
            AlchemistsCountPatch.FillGameUI(ui);
        }
    }
}