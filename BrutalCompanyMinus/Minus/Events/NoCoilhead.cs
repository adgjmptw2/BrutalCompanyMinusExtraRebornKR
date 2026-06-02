using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoCoilhead : MEvent
    {
        public override string Name() => nameof(NoCoilhead);

        public static NoCoilhead Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "오늘은 빤히 쳐다볼 게 없네요", "오늘은 목이 잘릴 일은 없겠군요, 제발요", "이제 스프링은 그만." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Coilhead), nameof(AntiCoilhead), nameof(ToilHead), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.CoilHead);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.CoilHead);
    }
}
