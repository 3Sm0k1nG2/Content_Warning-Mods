using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Mods3012.CW.ForceSleepMod
{
    [ContentWarningPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_VERSION, PluginInfo.PLUGIN_VANILLA_COMPATIBLE)]
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess(ProcessInfo.PROCESS_NAME)]
    internal class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }

        internal static PluginConfigEntries ConfigEntries { get; private set; }
        
        private Harmony harmony;
        
        private void Awake()
        {
            Logger = base.Logger;
            
            ConfigEntries = new PluginConfigEntries(base.Config);
            
            harmony = new Harmony(base.Info.Metadata.GUID);
            harmony.PatchAll();
            
            Logger.LogMessage("Plugged-in successfully");
        }
        
        private void OnDestroy()
        {
            harmony?.UnpatchSelf();

            Logger.LogMessage("Unplugged successfully");
            harmony = null;
            Logger = null;
            ConfigEntries = null;
        }
    }
}
