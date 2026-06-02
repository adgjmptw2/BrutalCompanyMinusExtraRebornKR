using BrutalCompanyMinus.Minus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Bonus : MEvent
    {
        public override string Name() => nameof(Bonus);

        public static Bonus Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "오늘은 회사의 기분이 좋은 모양입니다.", "기업이 당신의 존재에 대한 대가로 크레딧을 지급합니다", "■ ■ ■", "절대로 충분하지 않습니다." };
            ColorHex = "#008000";
            Type = EventType.Good;

            ScaleList.Add(ScaleType.MinCash, new Scale(75.0f, 2.25f, 75.0f, 300.0f));
            ScaleList.Add(ScaleType.MaxCash, new Scale(125.0f, 3.75f, 125.0f, 400.0f));
        }

        public override void Execute() => Manager.PayCredits(UnityEngine.Random.Range(Get(ScaleType.MinCash), Get(ScaleType.MaxCash) + 1));
    }
}
