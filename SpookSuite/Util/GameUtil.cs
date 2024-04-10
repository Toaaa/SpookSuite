﻿using HarmonyLib;
using Photon.Pun;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpookSuite.Util
{
    public class GameUtil
    {
        private static bool OverridingPhotonLocalPlayer = false;


        public static void SpawnItem(byte itemId, bool equip = false) => SpawnItem(itemId, Player.localPlayer.data.groundPos, equip);
        public static void SpawnItem(byte itemId, Vector3 spawnPos, bool equip = false)
        {
            spawnPos += Player.localPlayer.transform.forward;
            spawnPos.y += 1;

            Pickup component = PhotonNetwork.Instantiate("PickupHolder", spawnPos, Random.rotation, 0, null).GetComponent<Pickup>();
            component.ConfigurePickup(itemId, new ItemInstanceData(Guid.NewGuid()));

            if (equip) component.Interact(Player.localPlayer);
        }

        public static bool IsOverridingPhotonLocalPlayer()
        {
            return OverridingPhotonLocalPlayer;
        }

        public static void ToggleOverridePhotonLocalPlayer()
        {
            OverridingPhotonLocalPlayer = !OverridingPhotonLocalPlayer;
        }

    }
}
