using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SoulDev : MEvent
    {
        public override string Name() => nameof(SoulDev);

        public static SoulDev Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "최악보다 더 최악이 올 거라더니... 이게 그거네요!", "이걸 마주치느니 그냥 전등이라도 핥는 게 나을 겁니다.", "제스터랑 비슷하게 생겼는데... 어라, 상자가 없네요?" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "SoulDev",
                    new Scale(1.0f, 0.05f, 0.3f, 3.0f),
                    new Scale(1.0f, 0.05f, 0.3f, 3.0f),
                    new Scale(1.0f, 0.01f, 0.0f, 1.0f),
                    new Scale(1.0f, 0.01f, 1.0f, 2.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.soulDevPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}