
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BrutalCompanyMinus.Minus.Handlers;
using GameNetcodeStuff;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    public class NotMetal : MEvent
    {
        public override string Name() => nameof(NotMetal);

        public static NotMetal Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "물건들이 번개의 영향을 받지 않습니다!", "폭풍의 신들에게 축복받았습니다!" };
            ColorHex = "#008000";
            Type = EventType.Good;

            EventsToRemove = new List<string>() { nameof(MetalSwitch), nameof(IsMetal) };
        }

        public override void Execute()
        {

            // Declare the Active state to true globally
            Net.Instance.SetMetalOffNetServerRpc(true);

            // Bind the FlashLightFailure to an GameObject
            GameObject NotMetalObj = new GameObject("NotMetal");

            // Add the FlashlightItemChargerPatches component to the GameObject
            NotMetalObj.AddComponent<NotMetalNet>();
        }
        public override void OnShipLeave()
        {

            // Reset the Active state
            AllMetalPatches.RestoreOriginalStates();
            Active = false;
        }
        public override void OnGameStart()
        {
            // Reset the Active state
            Active = false;
        }

        public override void OnLocalDisconnect()
        {
            try
            {
                AllMetalPatches.RestoreOriginalStates();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error restoring original states on disconnect: {ex.Message}");
            }
    }
}
}
