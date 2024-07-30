using BepInEx.Configuration;

namespace Mods3012.OnlineFix.OnlineFixPatchMod
{
    internal class PluginConfigEntries
    {
        internal ConfigEntry<bool> Active { get; private set; }

        internal PluginConfigEntries(ConfigFile configFile)
        {
            Active = configFile.Bind(
                "UserPreferences",
                "Active",
                true,
                "Changes AppID to Spacewar. Requires game restart."
            );
        }
    }
}
