using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class FlowerSnake : MEvent
    {
        public override string Name() => nameof(FlowerSnake);

        public static FlowerSnake Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "몸무게가 조금 더 나가면 도움이 될 겁니다", "이게 당신의 머리를 날려버릴지도 모릅니다", "꽃뱀이다!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.FlowerSnake,
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(3.0f, 0.06f, 3.0f, 9.0f),
                new Scale(4.0f, 0.08f, 4.0f, 12.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
                new Scale(3.0f, 0.06f, 3.0f, 9.0f))
            };

            ScaleList.Add(ScaleType.DaytimeEnemyRarity, new Scale(20.0f, 0.8f, 20.0f, 100.0f));
        }

        public override void Execute()
        {
            if (Configuration.enforceEscapeModChecks.Value && !Compatibility.StarLancereNemyEscapePresent)
            {
                Instance.monstersToSpawn[0].minInside = new Scale(0f, 0f, 0f, 0f);
                Instance.monstersToSpawn[0].maxInside = new Scale(0f, 0f, 0f, 0f);
                Instance.monstersToSpawn[0].insideSpawnRarity = new Scale(0f, 0f, 0f, 0f);
            }

            ExecuteAllMonsterEvents();
            Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.DaytimeEnemies, Assets.EnemyName.FlowerSnake, Get(ScaleType.DaytimeEnemyRarity));
        }
    }
}
