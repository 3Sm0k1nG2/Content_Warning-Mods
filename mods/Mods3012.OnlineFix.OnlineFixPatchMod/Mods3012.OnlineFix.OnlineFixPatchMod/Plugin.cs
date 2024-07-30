using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Mods3012.OnlineFix.OnlineFixPatchMod.Patchers;

namespace Mods3012.OnlineFix.OnlineFixPatchMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }

        internal static PluginConfigEntries ConfigEntries { get; private set; }


        private Harmony harmony;

        private void Awake()
        {
            Logger = base.Logger;

            harmony = new Harmony(Info.Metadata.GUID);
            harmony.PatchAll();

            ConfigEntries = new PluginConfigEntries(Config);

            OnlineFixPatcher.Patch();

            Logger.LogMessage("Plugged-in successfully");
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();

            Logger.LogMessage("Unplugged successfully");

            harmony = null;
            Logger = null;
        }
    }
}
