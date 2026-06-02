using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoSnareFleas : MEvent
    {
        public override string Name() => nameof(NoSnareFleas);

        public static NoSnareFleas Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "천장 캠핑족 금지!", "머리는 무사할 겁니다, 아마도...", "고급 식사 금지" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(SnareFleas), nameof(Worms) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.SnareFlea);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.SnareFlea);
    }
}
