using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoWorm : MEvent
    {
        public override string Name() => nameof(NoWorm);

        public static NoWorm Instance;

        public override void Initalize()
        {
            Instance = this;

            EventsToRemove = new List<string>() { nameof(Hell), nameof(Worms) };

            Weight = 1;
            Descriptions = new List<string>() { "바닥벌레 없음", "레비아탄 없음", "대지는 안전합니다", "별미 없음" };
            ColorHex = "#008000";
            Type = EventType.Remove;
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.EarthLeviathan);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.EarthLeviathan);
    }
}
