using BepInEx.Configuration;

namespace Mods3012.CW.CustomSaveVideosLocationMod
{
    internal class PluginConfigEntries
    {
        internal ConfigEntry<string> CustomSaveVideosLocation { get; private set; }

        internal PluginConfigEntries(ConfigFile configFile)
        {
            this.CustomSaveVideosLocation = configFile.Bind<string>("UserPreferences", "CustomSaveVideosLocation", "./_SavedVideos", "Directory in which to save the videos.");
        }
    }
}
