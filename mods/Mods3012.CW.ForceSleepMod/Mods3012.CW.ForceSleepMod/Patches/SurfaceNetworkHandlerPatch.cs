using System;
using HarmonyLib;

namespace Mods3012.CW.ForceSleepMod.Patches
{
    [HarmonyPatch(typeof(SurfaceNetworkHandler))]
    internal class SurfaceNetworkHandlerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("CheckForSleep")]
        private static void CheckForSleep_Postfix(SurfaceNetworkHandler __instance)
        {
            bool isRequiredPlayerCountAsleepReached = IsRequiredPlayerCountAsleepReached();
            
            if (isRequiredPlayerCountAsleepReached)
            {
                __instance.RequestSleep();
            }
        }

        internal static bool IsRequiredPlayerCountAsleepReached()
        {
            bool isPlayerAliveCountAboveZero = PlayerHandler.instance.playersAlive.Count > 0;
            
            if (!isPlayerAliveCountAboveZero)
                return false;

            int counter = 0;
            int bedCount = 4;
            int requiredSleepersCount = Math.Min(Math.Min(bedCount, PlayerHandler.instance.playersAlive.Count), Plugin.ConfigEntries.RequiredPlayersCountToForceSleep.Value);
            foreach (Player player in PlayerHandler.instance.playersAlive)
            {
                bool isPlayerInBed = player.data.currentBed != null;

                if (!isPlayerInBed)
                    continue;

                bool isInstantSleepActive = Plugin.ConfigEntries.InstantSleep.Value;
                if (!isInstantSleepActive)
                {
                    Plugin.Logger.LogMessage(player.data.sleepAmount.ToString() + " " + player.data.triedToSleepTime.ToString());
                    
                    bool hasPlayerSleptEnough = player.data.sleepAmount >= 0.9f;
                    if (!hasPlayerSleptEnough)
                        continue;
                }

                bool isRequiredSleepersCountReached = ++counter >= requiredSleepersCount;
                if (isRequiredSleepersCountReached)
                    return true;
            }

            return false;
        }
    }
}
