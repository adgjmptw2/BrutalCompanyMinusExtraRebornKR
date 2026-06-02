using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SkullEnemy : MEvent
    {
        public override string Name() => nameof(SkullEnemy);

        public static SkullEnemy Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "해골물이 당신을 배달하러 갑니다...", "발버둥 쳐보세요, 어차피 스토커는 포기를 모르니까요." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "SkullEnemy",
                    new Scale(100.0f, 0.0f, 100.0f, 100.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f)),
            };
        }

        public override bool AddEventIfOnly() => Compatibility.SkullEnemyPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}