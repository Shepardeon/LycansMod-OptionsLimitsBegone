using System.Linq;

namespace OptionLimitsBegone.Patchs
{
    internal class GameManagerPatch : IPatch
    {
        public void Patch()
        {
            On.GameManager.Start += OnGameManagerStart;
        }

        public void Unpatch()
        {
            On.GameManager.Start -= OnGameManagerStart;
        }

        private void OnGameManagerStart(On.GameManager.orig_Start orig, GameManager self)
        {
            FillInGameUI();
            orig(self);
        }

        private void FillInGameUI()
        {
            OptionLimitsBegone.Log.LogMessage("Filling option dropdowns...");

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

            OptionLimitsBegone.Log.LogMessage("Done!");
        }
    }
}