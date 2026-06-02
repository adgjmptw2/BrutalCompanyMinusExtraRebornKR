
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
    public class MetalSwitch : MEvent
    {
        public override string Name() => nameof(MetalSwitch);

        public static MetalSwitch Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "번개의 전도 방향이 바뀌었습니다!", "금속 물건은 이제 금속이 아니고, 비금속 물건은 이제 금속이 되었습니다!" };
            ColorHex = "#CF9FFF";
            Type = EventType.Neutral;

            EventsToRemove = new List<string>() { nameof(IsMetal), nameof(NotMetal) };
        }

        public override void Execute()
        {



            // Declare the Active state to true globally
            Net.Instance.SetMetalSwitchNetServerRpc(true);

            // Bind the FlashLightFailure to an GameObject
            GameObject MetalSwitch = new GameObject("MetalSwitch");

            // Add the FlashlightItemChargerPatches component to the GameObject
            MetalSwitch.AddComponent<MetalSwitchNet>();
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
