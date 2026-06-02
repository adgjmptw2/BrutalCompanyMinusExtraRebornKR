using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoButlers : MEvent
    {
        public override string Name() => nameof(NoButlers);

        public static NoButlers Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "집사 금지", "뽁뽁이 금지", "터뜨리기 금지", "이 시설에는 칼이 부족하네요.", "이 시설은 지저분합니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Butlers), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.Butler);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.Butler);
    }
}
