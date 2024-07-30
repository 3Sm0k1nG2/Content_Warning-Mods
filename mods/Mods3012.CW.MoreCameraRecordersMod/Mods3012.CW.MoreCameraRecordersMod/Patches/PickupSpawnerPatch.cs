using System;
using HarmonyLib;
using UnityEngine;

namespace Mods3012.CW.MoreCameraRecordersMod.Patches
{
    [HarmonyPatch(typeof(PickupSpawner))]
    internal class PickupSpawnerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(PickupSpawner.SpawnMe))]
        internal static void SpawnMe(
            bool force,
            ref Item ___m_ItemToSpawn, ref Transform ___m_Transform
        )
        {
            PickupHandler.CreatePickup(___m_ItemToSpawn.id, new ItemInstanceData(Guid.NewGuid()), ___m_Transform.position, ___m_Transform.rotation);
            PickupHandler.CreatePickup(___m_ItemToSpawn.id, new ItemInstanceData(Guid.NewGuid()), ___m_Transform.position, ___m_Transform.rotation);
            PickupHandler.CreatePickup(___m_ItemToSpawn.id, new ItemInstanceData(Guid.NewGuid()), ___m_Transform.position, ___m_Transform.rotation);
        }
    }
}
