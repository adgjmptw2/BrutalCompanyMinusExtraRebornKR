using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class FacilityGhost : MEvent
    {
        public static bool Active = false;

        public override string Name() => nameof(FacilityGhost);

        public static FacilityGhost Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "시설 안에 유령이 있습니다", "누가 자꾸 전등을 끄는 거죠...", "초자연적 신호가 감지되었습니다", "위자 보드를 챙겨오세요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
        }

        public override void Execute() => Active = true;

        public override void OnShipLeave() => Active = false;

        public override void OnGameStart() => Active = false;
    }
}
