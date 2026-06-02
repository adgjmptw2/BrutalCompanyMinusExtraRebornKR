using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Slimes : MEvent
    {
        public override string Name() => nameof(Slimes);

        public static Slimes Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "바닥이 끈적거립니다", "때리지 않으면 정말 느릿느릿하군요", "소스 속에 파묻히지 않게 조심하세요", "대부분은 물과 고통으로 이루어져 있습니다", "액체 괴물" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.Hygrodere,
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(5.0f, 0.2f, 5.0f, 25.0f),
                new Scale(1.0f, 0.03f, 1.0f, 4.0f),
                new Scale(1.0f, 0.06f, 1.0f, 7.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f))
            };
        }

        public override void Execute()
        {
            Instance.monstersToSpawn[0].minOutside = new Scale(0f, 0f, 0f, 0f);
            Instance.monstersToSpawn[0].maxOutside = new Scale(0f, 0f, 0f, 0f);
            Instance.monstersToSpawn[0].outsideSpawnRarity = new Scale(0f, 0f, 0f, 0f);

            ExecuteAllMonsterEvents();
        }
    }
}
