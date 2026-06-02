using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using HarmonyLib;
using BrutalCompanyMinus.Minus.Handlers;
using BrutalCompanyMinus.Minus.MonoBehaviours;

namespace BrutalCompanyMinus.Minus.Events
{
    [HarmonyPatch]
    internal class ShipLightsFailure : MEvent
    {
        public override string Name() => nameof(ShipLightsFailure);

        public static ShipLightsFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3; //7
            Descriptions = new List<string>() { "조명 시스템: 오프라인", "조명이 박살 났습니다!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute()
        {
            Net.Instance.LightsOffServerRpc();
            Active = true;
            GameObject netObject = new GameObject("ShipLightsFailureEvent");
            netObject.AddComponent<ShipLightsFailureNet>();
        }

        public override void OnShipLeave()
        {
            Net.Instance.LightsOnServerRpc();
        }

        public override void OnLocalDisconnect()
        {
        }

    }
}