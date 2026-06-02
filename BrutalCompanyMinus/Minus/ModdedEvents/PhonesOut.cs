using System.Collections.Generic;
using UnityEngine;
using BrutalCompanyMinus.Minus.Handlers.Modded;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class PhonesOut : MEvent
    {
        public override string Name() => nameof(PhonesOut);

        public static PhonesOut Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "삐 소리 후에 메시지를 남겨주세요..", "수신 상태 불량", "전화선이 끊겼습니다", "전화기가 고장 난 것 같아요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

        }

        public override bool AddEventIfOnly() => Compatibility.LethalPhonesPresent;

        public override void Execute()
        {

            // Declare the Active state to true globally
            Net.Instance.SetPhonesOutServerRpc(true);

            // Bind the PhonesOut to an GameObject
            GameObject PhonesOutObject = new GameObject("PhonesOutFailureObject");

            // Add the PhonesOutPatches component to the GameObject
            PhonesOutObject.AddComponent<PhonesOutPatches>();
        }
        public override void OnShipLeave()
        {

            // Reset the Active state
            Active = false;
        }
        public override void OnGameStart()
        {
            // Reset the Active state
            Active = false;
        }

        public override void OnLocalDisconnect()
        {
        }
    }
}