using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class BadProduce : MEvent
    {
        public override string Name() => nameof(BadProduce);

        public static BadProduce Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "이곳의 모든 건 싸구려로 만들어졌습니다...", "누가 이런 쓰레기를 만든 거야...", "예산 맞춤형 고물들...", "질보다 양" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            ScaleList.Add(ScaleType.ScrapValue, new Scale(0.6f, 0.0f, 0.6f, 0.6f));
            ScaleList.Add(ScaleType.ScrapAmount, new Scale(2.0f, 0.0f, 2.0f, 2.0f));
        }

        public override void Execute()
        {
            Manager.scrapValueMultiplier *= Getf(ScaleType.ScrapValue);
            Manager.scrapAmountMultiplier *= Getf(ScaleType.ScrapAmount);
        }
    }
}
