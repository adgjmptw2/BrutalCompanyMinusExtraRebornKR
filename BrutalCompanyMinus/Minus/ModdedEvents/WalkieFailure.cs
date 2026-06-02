using System.Collections.Generic;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class WalkieFailure : MEvent
    {
        public override string Name() => nameof(WalkieFailure);

        public static WalkieFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 4;
            Descriptions = new List<string>() { "무전 시스템: 오프라인(OFFLINE)", "무전기가 먹통입니다. 이제 비명도 안 들리겠네요." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute()
        {
            Active = true;
            GameObject netObject = new GameObject("WalkeFailureEvent");
            netObject.AddComponent<WalkieFailureNet>();
        }
        public override void OnShipLeave() //Patch to reset the network int value
        {
            Active = false;
        }
        public override void OnGameStart() //Patch to reset the network int value
        {
            Active = false;
        }
    }
}
