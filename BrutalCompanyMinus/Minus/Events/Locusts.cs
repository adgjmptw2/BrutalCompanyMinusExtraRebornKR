using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Locusts : MEvent
    {
        public override string Name() => nameof(Locusts);

        public static Locusts Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 8;
            Descriptions = new List<string>() { "메뚜기 철이 왔습니다", "메뚜기 떼", "공기가 날갯짓 소리로 가득 찹니다." };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.RoamingLocust,
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(5.0f, 0.0f, 5.0f, 5.0f),
                new Scale(8.0f, 0.0f, 8.0f, 8.0f))
            };
        }

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
