using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ShipmentFees : MEvent
    {
        public static bool Active = false;
        public override string Name() => nameof(ShipmentFees);

        public static ShipmentFees Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "회사가 이제 배송비를 부과하기 시작했습니다!", "모든 배송물에 세금이 붙을 예정입니다...", "오늘은 아무것도 사지 않는 걸 추천합니다.", "빚더미에 앉을 수도 있으니 조심하세요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.MinCut, new Scale(0.2f, 0.004f, 0.2f, 0.6f));
            ScaleList.Add(ScaleType.MaxCut, new Scale(0.3f, 0.006f, 0.3f, 0.9f));
        }

        public override void Execute() => Active = true;

        public override void OnShipLeave() => Active = false;

        public override void OnGameStart() => Active = false;
    }
}
