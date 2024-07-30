using BepInEx.Configuration;

namespace Mods3012.CW.MorePlayersMod
{
    internal class PluginConfigEntries
    {
        internal ConfigEntry<int> LobbyMaxPlayersCount { get; private set; }

        internal PluginConfigEntries(ConfigFile configFile)
        {
            LobbyMaxPlayersCount = configFile.Bind(
                "UserPreferences",
                "PlayersCount",
                16,
                "The count of allowed players in a single lobby."
            );
        }
    }
}
