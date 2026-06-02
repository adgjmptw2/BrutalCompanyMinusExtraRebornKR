using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class RealityShift : MEvent
    {
        public static bool Active = false;
        public override string Name() => nameof(RealityShift);

        public static RealityShift Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "현실은 보이는 게 전부가 아닙니다", "고철이 지뢰로 변할지도 모릅니다", "울 준비나 하시는 게 좋을 겁니다", "운이 좋을 수도 있겠네요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute() => Net.Instance.SetRealityShiftActiveServerRpc(true);

        public override void OnShipLeave() => Net.Instance.SetRealityShiftActiveServerRpc(false);

        public override void OnGameStart() => Active = false;
    }
}
