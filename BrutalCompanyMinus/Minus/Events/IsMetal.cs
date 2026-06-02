
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
    public class IsMetal : MEvent
    {
        public override string Name() => nameof(IsMetal);

        public static IsMetal Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "어쩐 일인지 모든 물건이 번개의 영향을 받네요?", "폭풍의 신들에게 저주받았네요!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(MetalSwitch), nameof(NotMetal) };
        }

        public override void Execute()
        {

            // Declare the Active state to true globally
            Net.Instance.SetMetalOnNetServerRpc(true);

            // Bind the FlashLightFailure to an GameObject
            GameObject IsMetalObj = new GameObject("IsMetalObj");

            // Add the FlashlightItemChargerPatches component to the GameObject
            IsMetalObj.AddComponent<IsMetalNet>();
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
