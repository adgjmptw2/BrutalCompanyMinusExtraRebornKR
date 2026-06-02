using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoSlimes : MEvent
    {
        public override string Name() => nameof(NoSlimes);

        public static NoSlimes Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "끈적이 없음", "슬라임 없음", "신비로운 힘이 슬라임을 밀어내고 있습니다", "슬라임이 사라지자 이 행성에 평화가 찾아왔습니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Slimes), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.Hygrodere);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.Hygrodere);
    }
}
