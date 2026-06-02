using System;
using System.Collections.Generic;
using BrutalCompanyMinus.Minus.Handlers;
using BrutalCompanyMinus.Minus.Handlers.Modded;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class CruiserFailure : MEvent
    {
        public override string Name() => nameof(CruiserFailure);

        public static CruiserFailure Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "연장 보증에 가입하셨어야죠", "크루저 시스템 오류", "크루저가 고장 났습니다", "요즘 탈것들은 믿을 게 못 되네요!", "시동 구독 서비스가 만료되었습니다", "연장 보증 상담을 위해 방문했습니다" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute()
        {
            Net.Instance.SetCruiserOfflineServerRpc();

            // Declare the event active
            Active = true;

            if (Compatibility.ScanVanPresent)
            {
                GameObject netObject = new GameObject("ScanVanFailure");
                //Add the TimeChaosEvent component to it
                netObject.AddComponent<ScanVanNet>();
            }

        }

        public override void OnShipLeave() //Patch to reset the network int value
        {
            // Reset the Active state
            Active = false;
        }

        public override void OnGameStart() //Patch to reset the network int value
        {
            // Reset the Active state
            Active = false;
        }
    }
}
