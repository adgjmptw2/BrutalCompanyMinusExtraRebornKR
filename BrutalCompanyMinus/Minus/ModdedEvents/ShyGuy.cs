using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ShyGuy : MEvent
    {
        public override string Name() => nameof(ShyGuy);

        public static ShyGuy Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "등급: 유클리드(격리 불가)", "주의: 모든 인원은 알아서 생존하십시오.", "눈을 떼지 마세요. 물론 그러다 죽겠지만요..." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                "ShyGuyDef",
                new Scale(20.0f, 0.8f, 20.0f, 100.0f),
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.scopophobiaPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
