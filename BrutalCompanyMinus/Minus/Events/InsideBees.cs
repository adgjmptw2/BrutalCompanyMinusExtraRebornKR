using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class InsideBees : MEvent
    {
        public override string Name() => nameof(InsideBees);

        public static InsideBees Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "벌이다!! 잠깐만...", "시설이 윙윙거리는 소리로 가득합니다!", "벌을 '벌'벌 떨며 조심하세요", "내부가 아주 달콤하군요", "벌이 왜 미용실에서 해고당했는지 아세요? '삭발(Buzz cut)'밖에 할 줄 몰랐거든요." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToRemove = new List<string>() { nameof(Bees) };

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.CircuitBee,
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(3.0f, 0.05f, 2.0f, 9.0f),
                new Scale(4.0f, 0.084f, 3.0f, 12.0f),
                new Scale(3.0f, 0.06f, 3.0f, 9.0f),
                new Scale(5.0f, 0.1f, 5.0f, 15.0f))
            };

            ScaleList.Add(ScaleType.DaytimeEnemyRarity, new Scale(20.0f, 0.8f, 20.0f, 100.0f));
        }

        public override void Execute()
        {
            EnemyType bee = Assets.GetEnemy(Assets.EnemyName.CircuitBee);

            Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.Enemies, bee, monstersToSpawn[0].insideSpawnRarity.Compute(Type));
            Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.OutsideEnemies, bee, monstersToSpawn[0].outsideSpawnRarity.Compute(Type));
            Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.DaytimeEnemies, bee, Get(ScaleType.DaytimeEnemyRarity));

            Manager.Spawn.InsideEnemies(bee, UnityEngine.Random.Range(monstersToSpawn[0].minInside.Compute(Type), monstersToSpawn[0].maxInside.Compute(Type) + 1), 30.0f);
            Manager.Spawn.OutsideEnemies(bee, UnityEngine.Random.Range(monstersToSpawn[0].minOutside.Compute(Type), monstersToSpawn[0].maxOutside.Compute(Type) + 1));
        }
    }
}
