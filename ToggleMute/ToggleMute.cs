using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ToggleMute {
    [BepInPlugin("togglemute", "ToggleMute", "1.0.0")]
    public class ToggleMute : BaseUnityPlugin {
        public static ToggleMute Instance;
        public GameObject SpeakingGameObject;
        public GameObject MutedGameObject;
        public static ConfigEntry<Key> ConfigEntry;
        private Harmony Harmony;

        private void Awake() {
            Logger.LogInfo("Initializing ToggleMute");
            Instance = this;

            // assets
            Assets.PopulateAssets();
            
            // keybind
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

            // patch
            Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            Harmony.PatchAll();

            Logger.LogInfo($"Bound toggle mute to {bind.Value}");
            Logger.LogInfo("Initialized ToggleMute");
        }
    }
}
