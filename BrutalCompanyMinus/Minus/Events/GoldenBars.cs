using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class GoldenBars : MEvent
    {
        public override string Name() => nameof(GoldenBars);

        public static GoldenBars Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "블링블링!", "마치 은행을 터는 것 같군요", "이게 행운의 징조이길 바랍니다", "몸무게가 300파운드 이상 늘어날 준비는 되셨나요?" };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;

            EventsToRemove = new List<string>() { nameof(RealityShift), nameof(SID) };

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.GoldBar), 100)
            );

            ScaleList.Add(ScaleType.ScrapAmount, new Scale(1.0f, 0.005f, 1.0f, 1.5f));
        }

        public override bool AddEventIfOnly()
        {
            if (!Manager.transmuteScrap)
            {
                Manager.transmuteScrap = true;
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            Manager.scrapAmountMultiplier *= Getf(ScaleType.ScrapAmount);
            scrapTransmutationEvent.Execute();
        }
    }
}
