using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Linq;
namespace ToggleMute.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch {
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void PlayerControllerB_Update(PlayerControllerB __instance) {
        if ((!__instance.IsOwner || __instance.IsServer && !__instance.isHostPlayerObject) && !__instance.isTestingPlayer) {
            return;
        }
        if (Keyboard.current[ToggleMute.ConfigEntry.Value].wasPressedThisFrame && !__instance.isTypingChat && !__instance.inTerminalMenu) {
            IngamePlayerSettings.Instance.settings.micEnabled = !IngamePlayerSettings.Instance.settings.micEnabled;
            IngamePlayerSettings.Instance.SetMicrophoneEnabled();
            SettingsOption speakerBtn = System.Array.Find(UnityEngine.Object.FindObjectsOfType<SettingsOption>(includeInactive: true), (SettingsOption x) => ((Object)x).name == "SpeakerButton");
            speakerBtn.ToggleEnabledImage(4);

            ToggleMute.Instance.SpeakingGameObject.SetActive(IngamePlayerSettings.Instance.settings.micEnabled);
            ToggleMute.Instance.MutedGameObject.SetActive(!IngamePlayerSettings.Instance.settings.micEnabled);
        }
    }
}