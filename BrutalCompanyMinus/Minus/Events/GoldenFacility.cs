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
    internal class GoldenFacility : MEvent
    {
        public override string Name() => nameof(GoldenFacility);

        public static GoldenFacility Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "고철이 반짝거리네요", "앞에 가치 있는 고철이 있습니다", "이 시설은 노다지군요" };
            ColorHex = "#008000";
            Type = EventType.Good;

            EventsToRemove = new List<string>() { nameof(RealityShift), nameof(SID) };

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.GoldenCup), 25),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.Ring), 20),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.GoldBar), 1),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.FancyLamp), 10),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.PerfumeBottle), 26),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.Painting), 15),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.CashRegister), 3)
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
