using System.Collections.Generic;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class TeleporterFailure : MEvent
    {
        public override string Name() => nameof(TeleporterFailure);

        public static TeleporterFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 4;
            Descriptions = new List<string>() { "텔레포트 시스템: 응답 없음(ERROR)", "텔레포터 오작동: 행운을 빕니다, 조각나지 않기를!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(TargetingFailureEvent) };
        }

        public override void Execute()
        {
            Active = true;
            GameObject netObject = new GameObject("TeleporterFailureEvent");
            netObject.AddComponent<TeleporterFailureNet>();
        }
        public override void OnShipLeave()
        {
            Active = false;
        }
        public override void OnGameStart()
        {
            Active = false;
        }
    }
}
