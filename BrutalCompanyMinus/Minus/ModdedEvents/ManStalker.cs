using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ManStalker : MEvent
    {
        public override string Name() => nameof(ManStalker);

        public static ManStalker Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "그것이 뒤를 쫓습니다", "붙잡히지 않기를 빌어야겠군요", "무슨 수를 써서라도 피하세요..." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "menstalker_type",
                    new Scale(100.0f, 0.0f, 100.0f, 100.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(1.0f, 0.016f, 1.0f, 3.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f)),
            };
        }

        public override bool AddEventIfOnly() => Compatibility.ManStalkerPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}