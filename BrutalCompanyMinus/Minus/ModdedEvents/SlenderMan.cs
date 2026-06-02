using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SlenderMan : MEvent
    {
        public override string Name() => nameof(SlenderMan);

        public static SlenderMan Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "추억 속의 불쾌한 골짜기", "가까이 오게 두지 마세요... 제발요", "기분 탓일까요? 아니면 당신 뒤일까요?" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                "SlendermanEnemy",
                new Scale(2.0f, 0.08f, 2.0f, 10.0f),
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(1.0f, 0.04f, 2.0f, 3.0f),
                new Scale(1.0f, 0.04f, 2.0f, 3.0f),
                new Scale(0.0f, 0.0075f, 0.0f, 1.0f),
                new Scale(0.0f, 0.02f, 0.0f, 1.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.facelessStalekerPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
