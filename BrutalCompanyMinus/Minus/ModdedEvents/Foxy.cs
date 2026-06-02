using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Foxy : MEvent
    {
        public override string Name() => nameof(Foxy);

        public static Foxy Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "야호~ 해적이다!", "다섯 밤...?", "악몽 같은 시간이 될 겁니다" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>()
            {
                new MonsterEvent(
                    "Foxy",
                    new Scale(100.0f, 0.0f, 100.0f, 100.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                    new Scale(0.0f, 0.0f, 0.0f, 0.0f)),
            };
        }

        public override bool AddEventIfOnly() => Compatibility.FoxyPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}