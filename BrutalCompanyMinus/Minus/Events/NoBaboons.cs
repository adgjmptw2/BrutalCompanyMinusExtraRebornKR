using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoBaboons : MEvent
    {
        public override string Name() => nameof(NoBaboons);

        public static NoBaboons Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "패거리 금지", "절도 금지", "이건 이제 제 겁니다" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(BaboonHorde), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.BaboonHawk);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.BaboonHawk);
    }
}
