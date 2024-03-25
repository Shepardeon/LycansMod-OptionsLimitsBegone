using HarmonyLib;
using System;
using System.Reflection;
using TMPro;

namespace OptionLimitsBegone.Patchs
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.UpdateAlchemistsCountSetting), new Type[] { typeof(int) })]
    internal static class AlchemistsCountPatch
    {
        private static void Prefix(int value)
        {
            var inst = GameManager.Instance;
            var type = inst.GetType();
            var prop = type.GetProperty("AlchemistsCount");

            if (inst.Runner.IsServer && GameManager.State.Current == GameState.EGameState.Pregame && value >= 0)
            {
                prop.SetValue(inst, value);
            }
        }

        internal static void FillGameUI(GameSettingsUI ui)
        {
            var type = ui.GetType();
            var field = type.GetField("alchemistsCountDropdown", BindingFlags.NonPublic | BindingFlags.Instance);
            var dropdown = field.GetValue(ui) as TMP_Dropdown;

            for (int i = 2; i < 6; i++)
            {
                dropdown?.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
            }
        }
    }
}