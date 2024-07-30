using BepInEx.Configuration;

namespace Mods3012.CW.ForceSleepMod
{
    internal class PluginConfigEntries
    {
        internal ConfigEntry<bool> InstantSleep { get; private set; }

        internal ConfigEntry<int> RequiredPlayersCountToForceSleep { get; private set; }
        
        internal PluginConfigEntries(ConfigFile configFile)
        {
            InstantSleep = configFile.Bind<bool>("UserPreferences", "InstantSleep", true, "Skips the delay when falling asleep.");
            RequiredPlayersCountToForceSleep = configFile.Bind<int>("UserPreferences", "RequiredPlayersCountToForceSleep", 1, "How many players are required to trigger force sleep. (Min 1, Max 4).");
        }
    }
}
