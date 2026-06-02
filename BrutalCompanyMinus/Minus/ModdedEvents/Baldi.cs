using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Baldi : MEvent
    {
        public override string Name() => nameof(Baldi);

        public static Baldi Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "도망칠 수 있을 때 당장 나가세요!!!", "근접 경보: 자(Ruler)가 감지되었습니다.", "찰싹!", "수학 소리가 들려요!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "Baldi",
                    new Scale(100.0f, 0.0f, 100.0f, 100.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f)),
            };
        }

        public override bool AddEventIfOnly() => Compatibility.BaldiPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}