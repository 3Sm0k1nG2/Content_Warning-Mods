using HarmonyLib;
using System;
using Steamworks;

namespace Mods3012.CW.MorePlayersMod.Patches
{
    [HarmonyPatch(typeof(SteamLobbyHandler))]
    internal class SteamLobbyHandlerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SteamLobbyHandler.HostMatch))]
        public static void HostMatch(
            Action<ulong> _action, bool privateMatch,
            ref CallResult<LobbyCreated_t> ___m_OnLobbyCreatedCallBack,
            ref int ___m_MaxPlayers
        // Uncomment if in prefix
        //ref bool ___m_isHostingPrivate,
        //ref bool ___m_Joined,
        //ref Action<ulong> ___OnHostedAction
        )
        {
            ___m_MaxPlayers = Plugin.ConfigEntries.LobbyMaxPlayersCount.Value;
            Plugin.Logger.LogMessage($"Updated Max Players to {___m_MaxPlayers}.");

            // Uncomment if in prefix
            //___m_isHostingPrivate = privateMatch;
            //___m_Joined = false;
            //___OnHostedAction = _action;

            SteamAPICall_t steamAPICall_t = SteamMatchmaking.CreateLobby(privateMatch ? ELobbyType.k_ELobbyTypeFriendsOnly : ELobbyType.k_ELobbyTypePrivate, ___m_MaxPlayers);
            ___m_OnLobbyCreatedCallBack.Set(steamAPICall_t, null);

            Plugin.Logger.LogInfo($"SteamLobbyHandler Hosting With Max Players: {___m_MaxPlayers}");
        }
    }
}
