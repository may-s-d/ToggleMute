using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine.InputSystem;

namespace ToggleMute.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch {
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void PlayerControllerB_Update(PlayerControllerB __instance) {
        if ((!__instance.IsOwner || !__instance.isPlayerControlled || __instance.IsServer && !__instance.isHostPlayerObject) && !__instance.isTestingPlayer) {
            return;
        }
        if (Keyboard.current[ToggleMute.ConfigEntry.Value].wasPressedThisFrame && !__instance.isTypingChat && !__instance.inTerminalMenu) {
            IngamePlayerSettings.Instance.settings.micEnabled = !IngamePlayerSettings.Instance.settings.micEnabled;
            ToggleMute.Instance.SpeakingGameObject.SetActive(IngamePlayerSettings.Instance.settings.micEnabled);
            ToggleMute.Instance.MutedGameObject.SetActive(!IngamePlayerSettings.Instance.settings.micEnabled);
        }
    }
}