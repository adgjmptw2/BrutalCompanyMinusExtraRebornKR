using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class PlentyOutsideScrap : MEvent
    {
        public override string Name() => nameof(PlentyOutsideScrap);

        public static PlentyOutsideScrap Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "외부에서도 고철을 찾을 수 있습니다.", "이 시설은 폐기물 처리 규정을 전혀 안 지키나 보군요", "와아, 밖에도 고철이 있네요" };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;

            EventsToRemove = new List<string>() { nameof(ScarceOutsideScrap) };

            ScaleList.Add(ScaleType.MinItemAmount, new Scale(9.0f, 0.09f, 9.0f, 18.0f));
            ScaleList.Add(ScaleType.MaxItemAmount, new Scale(12.0f, 0.12f, 12.0f, 24.0f));
        }

        public override void Execute() => Manager.Spawn.OutsideScrap(UnityEngine.Random.Range(Get(ScaleType.MinItemAmount), Get(ScaleType.MaxItemAmount) + 1));
    }
}
