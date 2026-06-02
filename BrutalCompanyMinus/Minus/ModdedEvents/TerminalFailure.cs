using System.Collections.Generic;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class TerminalFailure : MEvent
    {
        public override string Name() => nameof(TerminalFailure);

        public static TerminalFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 4;
            Descriptions = new List<string>() { "터미널 치명적 오류", "터미널 콘솔: 오프라인" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute()
        {
            Active = true;
            GameObject netObject = new GameObject("TerminalFailureEvent");
            netObject.AddComponent<TerminalFailureNet>();
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
