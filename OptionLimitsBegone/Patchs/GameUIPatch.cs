using System.Linq;

namespace OptionLimitsBegone.Patchs
{
    internal class GameUIPatch : IPatch
    {
        public void Patch()
        {
            On.GameState.Spawned += OnGameStateSpawned;
        }

        public void Unpatch()
        {
            On.GameState.Spawned -= OnGameStateSpawned;
        }

        private void OnGameStateSpawned(On.GameState.orig_Spawned orig, GameState self)
        {
            orig(self);

            self.StateMachine[GameState.EGameState.Play].onEnter += (previousState) =>
            {
                if (!self.Runner.IsPlayer) return;

                var localPlayer = PlayerController.Local;
                if (localPlayer == null) return;

                if (previousState == GameState.EGameState.Pregame)
                {
                    var wolves = PlayerRegistry.Where(p => p.Role == PlayerController.PlayerRole.Wolf);
                    var others = wolves.Where(p => p != localPlayer);

                    if (others.Any() && localPlayer.Role == PlayerController.PlayerRole.Wolf)
                        GameManager.Instance.gameUI.UpdateAlly(string.Join(", ", others.Select(p => p.PlayerData.Username)));
                    GameManager.Instance.gameUI.UpdateWolvesRecap(string.Join(" / ", wolves.Select(p => p.PlayerData.Username)));
                }
            };
        }
    }
}