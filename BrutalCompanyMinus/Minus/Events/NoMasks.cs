using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoMasks : MEvent
    {
        public override string Name() => nameof(NoMasks);

        public static NoMasks Instance;

        public override void Initalize()
        {
            Instance = this;

            EventsToRemove = new List<string>() { nameof(Hell), nameof(Masked), nameof(NutSlayer) };

            Weight = 1;
            Descriptions = new List<string>() { "친구 없음 :(", "이제 포옹은 그만", "더 이상의 불신지옥은 없습니다" };
            ColorHex = "#008000";
            Type = EventType.Remove;
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.Masked);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.Masked);
    }
}
