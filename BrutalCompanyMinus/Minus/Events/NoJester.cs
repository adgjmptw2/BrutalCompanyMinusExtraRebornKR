using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoJester : MEvent
    {
        public override string Name() => nameof(NoJester);

        public static NoJester Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "더 이상의 제스터는 없습니다", "태엽 감기 금지", "오늘은 집에 안 가셔도 됩니다" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Jester), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.Jester);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.Jester);
    }
}
