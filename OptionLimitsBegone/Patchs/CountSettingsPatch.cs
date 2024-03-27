namespace OptionLimitsBegone.Patchs
{
    internal class CountSettingsPatch : IPatch
    {
        public void Patch()
        {
            On.GameManager.UpdateAlchemistsCountSetting += OnAlchemistsCountSettingChanged;
            On.GameManager.UpdateHuntersCountSetting += OnHuntersCountSettingChanged;
            On.GameManager.UpdateWolvesCountSetting += OnWolvesCountSettingChanged;
        }

        public void Unpatch()
        {
            On.GameManager.UpdateAlchemistsCountSetting -= OnAlchemistsCountSettingChanged;
            On.GameManager.UpdateHuntersCountSetting -= OnHuntersCountSettingChanged;
            On.GameManager.UpdateWolvesCountSetting -= OnWolvesCountSettingChanged;
        }

        private void OnAlchemistsCountSettingChanged(On.GameManager.orig_UpdateAlchemistsCountSetting orig, GameManager self, int value)
        {
            if (self.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 2)
            {
                self.AlchemistsCount = value;
                return;
            }

            orig(self, value);
        }

        private void OnHuntersCountSettingChanged(On.GameManager.orig_UpdateHuntersCountSetting orig, GameManager self, int value)
        {
            if (self.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 2)
            {
                self.HuntersCount = value;
                return;
            }

            orig(self, value);
        }

        private void OnWolvesCountSettingChanged(On.GameManager.orig_UpdateWolvesCountSetting orig, GameManager self, int value)
        {
            if (self.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 3)
            {
                self.AlchemistsCount = value;
                return;
            }

            orig(self, value);
        }
    }
}