using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class MaskItem : MEvent
    {
        public override string Name() => nameof(MaskItem);

        public static MaskItem Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "웃긴 얼굴들이 사방에 있네요", "쓰지만 않는다면 괜찮아요", "익살스러운 가면들, 코믹한 재미 보장", "이걸로 '그들' 중 하나가 되지만 마세요..." };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;

            EventsToRemove = new List<string>() { nameof(SID), nameof(RealityShift) };

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.Comedy), 50),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.Tragedy), 50)
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
