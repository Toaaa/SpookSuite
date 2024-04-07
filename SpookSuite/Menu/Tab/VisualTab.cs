﻿using Photon.Pun;
using SpookSuite.Cheats;
using SpookSuite.Cheats.Core;
using SpookSuite.Menu.Core;
using System;
using UnityEngine;

namespace SpookSuite.Menu.Tab
{
    internal class VisualTab : MenuTab
    {
        public VisualTab() : base("Visual") { }
        byte selectedid;
        string pid;
        string sname;
        private Vector2 scrollPos = Vector2.zero;
        public override void Draw()
        {
            GUILayout.BeginVertical(GUILayout.Width(SpookSuiteMenu.Instance.contentWidth * 0.5f - SpookSuiteMenu.Instance.spaceFromLeft));
            VisualContent();
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(SpookSuiteMenu.Instance.contentWidth * 0.5f - SpookSuiteMenu.Instance.spaceFromLeft));
            ESPContent();
            GUILayout.EndVertical();
        }

        private void VisualContent()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            UI.CheatToggleSlider(Cheat.Instance<FOV>(), "FOV", Cheats.FOV.Value.ToString(), ref Cheats.FOV.Value, 1, 170);

            foreach (Item i in UnityEngine.Object.FindObjectsOfType<Item>())
            {
                if(GUILayout.Button($"{i.displayName}, id: {i.id}, pid: {i.persistentID}"))
                {
                    pid = i.persistentID;
                    selectedid = i.id;
                    name = i.displayName;
                }
            }


            GUILayout.EndScrollView();
        }

        private void ESPContent()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Label($"{selectedid}");
            GUILayout.Label(sname);
            GUILayout.Label(pid);
            if (GUILayout.Button("Spawn " + sname))
            {
                Pickup component = PhotonNetwork.Instantiate("PickupHolder", Player.localPlayer.transform.position, UnityEngine.Random.rotation, 0, null).GetComponent<Pickup>();
                component.ConfigurePickup(selectedid, new ItemInstanceData(Guid.NewGuid()));
            }


            GUILayout.Label("Player ESP");
            //UI.Checkbox("Enabled", ref Cheats.PlayerESP.enabled);
            //UI.Checkbox("Skeleton", ref Cheats.PlayerESP.skeletonESP); //Player.refs.ik bones
            //UI.Checkbox("Box", ref Cheats.PlayerESP.box);
            //UI.Checkbox("Looking Radius", ref Cheats.PlayerESP.LookingRadius);//make a semicircle based off their rotation and field of view in front of em on the ground.
            GUILayout.Label("Enemy ESP");

            GUILayout.EndScrollView();
        }
    }
}
