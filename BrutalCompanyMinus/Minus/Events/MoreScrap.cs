using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class MoreScrap : MEvent
    {
        public override string Name() => nameof(MoreScrap);

        public static MoreScrap Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "시설 내부에 고철이 아주 조금 더 많아졌네요!", "이 시설은 다른 곳보다 생산성이 약간 더 높았습니다.", "고철, 그런데 이제 좀 더 많은." };
            ColorHex = "#008000";
            Type = EventType.Good;

            ScaleList.Add(ScaleType.ScrapAmount, new Scale(1.1f, 0.007f, 1.1f, 1.8f));
        }

        public override void Execute() => Manager.scrapAmountMultiplier *= Getf(ScaleType.ScrapAmount);
    }
}
