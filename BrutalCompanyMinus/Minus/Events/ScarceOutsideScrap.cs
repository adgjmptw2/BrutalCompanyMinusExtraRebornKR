using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ScarceOutsideScrap : MEvent
    {
        public override string Name() => nameof(ScarceOutsideScrap);

        public static ScarceOutsideScrap Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "외부에 고철이 아주 소량 흩어져 있습니다.", "누군가 여기에 고철들을 버려둔 모양이군요...", "개코원숭이들이 가로채기 전에 얼른 챙기세요" };
            ColorHex = "#008000";
            Type = EventType.Good;

            ScaleList.Add(ScaleType.MinItemAmount, new Scale(3.0f, 0.06f, 3.0f, 9.0f));
            ScaleList.Add(ScaleType.MaxItemAmount, new Scale(4.0f, 0.08f, 4.0f, 12.0f));
        }

        public override void Execute() => Manager.Spawn.OutsideScrap(UnityEngine.Random.Range(Get(ScaleType.MinItemAmount), Get(ScaleType.MaxItemAmount) + 1));
    }
}
