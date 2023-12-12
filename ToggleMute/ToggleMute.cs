using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine.InputSystem;

namespace ToggleMute
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class ToggleMute : BaseUnityPlugin
    {
        public static ConfigEntry<Key> configEntry;
        private Harmony harmony;

        private void Awake()
        {
            Logger.LogInfo("Initializing ToggleMute");
            
            var id = "ToggleMute";
            var shortcut = Key.T;
            var description = "Toggle mute";

            var bind = Config.Bind(
                    "Bindings",
                    id,
                    shortcut,
                    description
            );

            configEntry = bind;

            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            Logger.LogInfo($"Bound toggle mute to {shortcut}");
            Logger.LogInfo("Initialized ToggleMute");
        }
    }
}
