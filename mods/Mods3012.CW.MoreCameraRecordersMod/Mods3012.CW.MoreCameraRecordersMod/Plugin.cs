using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Mods3012.CW.MoreCameraRecordersMod
{
    [ContentWarningPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_VERSION, PluginInfo.PLUGIN_VANILLA_COMPATIBLE)]
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess(ProcessInfo.PROCESS_NAME)]
    internal class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger { get; private set; }

        internal static PluginConfigEntries ConfigEntries { get; private set; }

        private Harmony harmony;

        private void Awake()
        {
            Logger = base.Logger;

            ConfigEntries = new PluginConfigEntries(Config);

            harmony = new Harmony(Info.Metadata.GUID);
            harmony.PatchAll();

            Logger.LogMessage("Plugged-in successfully");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();

            Logger.LogMessage("Unplugged successfully");

            Logger = null;
            harmony = null;
        }
    }
}
