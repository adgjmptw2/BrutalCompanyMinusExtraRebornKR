using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoThumpers : MEvent
    {
        public override string Name() => nameof(NoThumpers);

        public static NoThumpers Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "덤퍼 없음", "드리프트 금지", "더 이상 도망칠 필요 없습니다", "상어는 이제 그만", "다리 없는 것들 없음" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Thumpers), nameof(Hell), nameof(NutSlayer) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.Thumper);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.Thumper);
    }
}
