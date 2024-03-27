using System.Linq;

namespace OptionLimitsBegone.Patchs
{
    internal class GameUIPatch : IPatch
    {
        public void Patch()
        {
            On.GameUI.UpdateAlly += OnAllyUpdated;
            On.GameUI.UpdateWolvesRecap += OnWolvesRecapUpdated;
        }

        public void Unpatch()
        {
            On.GameUI.UpdateAlly -= OnAllyUpdated;
            On.GameUI.UpdateWolvesRecap -= OnWolvesRecapUpdated;
        }

        private void OnAllyUpdated(On.GameUI.orig_UpdateAlly orig, GameUI self, string username)
        {
            var localPlayer = PlayerController.Local;
            var wolves = PlayerRegistry.Where(p => p.Role == PlayerController.PlayerRole.Wolf && p != localPlayer);

            username = string.Join(", ", wolves.Select(p => p.PlayerData.Username));

            orig(self, username);
        }

        private void OnWolvesRecapUpdated(On.GameUI.orig_UpdateWolvesRecap orig, GameUI self, string value)
        {
            var localPlayer = PlayerController.Local;
            var wolves = PlayerRegistry.Where(p => p.Role == PlayerController.PlayerRole.Wolf);

            value = string.Join(" / ", wolves.Select((p) => p.PlayerData.Username));

            orig(self, value);
        }
    }
}