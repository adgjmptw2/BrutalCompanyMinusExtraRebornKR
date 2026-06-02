using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class BlackFriday : MEvent
    {
        public override string Name() => nameof(BlackFriday);

        public static BlackFriday Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "모든 품목 세일 중!!!!", "할인 열기로 시장이 뜨겁게 달아올랐습니다", "이 기회를 놓치지 마세요!" };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;

            EventsToRemove = new List<string>() { nameof(ShipmentFees) };

            ScaleList.Add(ScaleType.MinPercentageCut, new Scale(25.0f, 0.7f, 25.0f, 95.0f));
            ScaleList.Add(ScaleType.MaxPercentageCut, new Scale(55.0f, 0.7f, 55.0f, 95.0f));
        }

        public override void Execute() => Net.Instance.BlackFridayServerRpc(Get(ScaleType.MinPercentageCut), Get(ScaleType.MaxPercentageCut));
    }
}
