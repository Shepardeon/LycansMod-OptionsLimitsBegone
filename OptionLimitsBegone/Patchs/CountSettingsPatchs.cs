using HarmonyLib;

namespace OptionLimitsBegone.Patchs
{
    internal static class CountSettingsPatchs
    {
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.UpdateAlchemistsCountSetting))]
        [HarmonyPrefix]
        private static bool AlchemistsCountSettingPrefix(int value)
        {
            if (GameManager.Instance.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 2)
            {
                GameManager.Instance.AlchemistsCount = value;
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.UpdateHuntersCountSetting))]
        [HarmonyPrefix]
        private static bool HuntersCountSettingPrefix(int value)
        {
            if (GameManager.Instance.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 2)
            {
                GameManager.Instance.HuntersCount = value;
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.UpdateWolvesCountSetting))]
        [HarmonyPrefix]
        private static bool WolvesCountSettingPrefix(int value)
        {
            if (GameManager.Instance.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 3)
            {
                GameManager.Instance.WolvesCount = value;
                return false;
            }

            return true;
        }
    }
}