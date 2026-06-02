using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoImmortalSnails : MEvent
    {
        public override string Name() => nameof(NoImmortalSnails);

        public static NoImmortalSnails Instance;

        public override void Initalize()
        {
            Instance = this;

            EventsToRemove = new List<string>() { nameof(Hell), nameof(RollingGiants) };

            Weight = 1;
            Descriptions = new List<string>() { "느릿느릿 움직이는 것들 없음", "불사신 달팽이 없음", "여기에 열핵폭탄 같은 건 없어요..." };
            ColorHex = "#008000";
            Type = EventType.Remove;
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists("ImmortalSnail.EnemyType") && Compatibility.immortalSnailPresent;

        public override void Execute() => Manager.RemoveSpawn("ImmortalSnail.EnemyType");
    }
}
