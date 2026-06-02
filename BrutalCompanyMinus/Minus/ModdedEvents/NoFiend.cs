using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoFiend : MEvent
    {
        public override string Name() => nameof(NoFiend);

        public static NoFiend Instance;

        public override void Initalize()
        {
            Instance = this;

            EventsToRemove = new List<string>() { nameof(Hell), nameof(NoFiend) };

            Weight = 1;
            Descriptions = new List<string>() { "악마 없음", "갑툭튀 없음... 아마도요", "아무것도 없음", "이 달에서는 광질이 허용됩니다!!!" };
            ColorHex = "#008000";
            Type = EventType.Remove;
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists("TheFiend") && Compatibility.theFiendPresent;

        public override void Execute() => Manager.RemoveSpawn("TheFiend");
    }
}
