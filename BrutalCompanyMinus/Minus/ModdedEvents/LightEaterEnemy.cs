using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class LightEaterEnemy : MEvent
    {
        public override string Name() => nameof(LightEaterEnemy);

        public static LightEaterEnemy Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "빛을 선호하시나요?", "빛이 없다고 상상해 보세요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "LightEaterEnemy",
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(1.0f, 0.01f, 0.0f, 1.0f),
                    new Scale(1.0f, 0.01f, 1.0f, 2.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.lighteaterPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}