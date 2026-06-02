using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using static BrutalCompanyMinus.Net;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SeveredBits : MEvent
    {
        public override string Name() => nameof(SeveredBits);

        public static SeveredBits Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "키라 요시카게", "자잘한 모든 조각들", "회사의 취향이 참 독특하군요", "이게 진짜가 아니길 바랍니다" };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;

            EventsToRemove = new List<string>() { nameof(SID) };

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredBone), 13),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredBoneRib), 12),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredEar), 12),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredFoot), 12),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredHand), 13),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredHeart), 13),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredThigh), 12),
                new SpawnableItemWithRarity(Assets.GetItem(Assets.ItemName.SeveredTongue), 13)
            );

            ScaleList.Add(ScaleType.ScrapAmount, new Scale(1.0f, 0.005f, 1.0f, 1.1f));
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
