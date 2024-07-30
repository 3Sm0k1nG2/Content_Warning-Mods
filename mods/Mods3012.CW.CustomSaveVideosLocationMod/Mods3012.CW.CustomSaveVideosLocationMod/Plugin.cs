using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Mods3012.CW.CustomSaveVideosLocationMod
{
    [ContentWarningPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_VERSION, PluginInfo.PLUGIN_VANILLA_COMPATIBLE)]
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess(ProcessInfo.PROCESS_NAME)]
    public class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }
        internal static PluginConfigEntries ConfigEntries { get; private set; }

        private Harmony harmony;

        private void Awake()
        {
            Logger = base.Logger;
            ConfigEntries = new PluginConfigEntries(base.Config);
            harmony = new Harmony("Mods3012.CW.CustomSaveVideosLocationMod");
            harmony.PatchAll();
            Logger.LogInfo("Plugged-in successfully.");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
            
            Logger.LogInfo("Unplugged succesfully");
            Logger = null;
            ConfigEntries = null;
            harmony = null;
        }
    }
}
