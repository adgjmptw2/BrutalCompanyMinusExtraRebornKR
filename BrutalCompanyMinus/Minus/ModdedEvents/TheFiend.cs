using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class TheFiend : MEvent
    {
        public override string Name() => nameof(TheFiend);

        public static TheFiend Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "악마가 이미 시설 안에 침투했습니다", "주의: 심장 마비를 유발할 수 있음", "플래시 비추지 마세요... 자극해서 좋을 거 없거든요" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                "TheFiend",
                new Scale(8.0f, 0.4f, 8.0f, 32.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(0.0f, 0.022f, 0.0f, 1.0f),
                new Scale(0.0f, 0.034f, 0.0f, 2.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.theFiendPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
