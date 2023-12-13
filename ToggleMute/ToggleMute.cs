using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine.InputSystem;

namespace ToggleMute {
    [BepInPlugin("togglemute", "ToggleMute", "1.0.0")]
    public class ToggleMute : BaseUnityPlugin {
        public static ToggleMute Instance;
        public static ConfigEntry<Key> ConfigEntry;
        private Harmony Harmony;

        private void Awake() {
            Logger.LogInfo("Initializing ToggleMute");
            
            var id = "ToggleMute";
            var shortcut = Key.T;
            var description = "Hotkey to toggle mute";

            var bind = Config.Bind(
                    "Bindings",
                    id,
                    shortcut,
                    description
            );

            ConfigEntry = bind;

            Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            Harmony.PatchAll();

            Logger.LogInfo($"Bound toggle mute to {bind.Value}");
            Logger.LogInfo("Initialized ToggleMute");
        }
    }
}
