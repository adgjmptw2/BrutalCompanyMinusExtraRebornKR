using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoLizards : MEvent
    {
        public override string Name() => nameof(NoLizards);

        public static NoLizards Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "도마뱀 없음", "이유는 모르겠지만 불사신도 없음", "가스 가스 가스 금지" };
            ColorHex = "#008000";
            Type = EventType.Remove;
            EventsToRemove = new List<string>() { nameof(Lizard) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.SporeLizard);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.SporeLizard);
    }
}
