using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Nutcracker : MEvent
    {
        public override string Name() => nameof(Nutcracker);

        public static Nutcracker Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "이 시설은 무장 상태입니다.", "낭심 가리개를 챙겨오는 게 좋을 겁니다", "움직여 보시지, 감히..." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToSpawnWith = new List<string>() { nameof(Turrets) };

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.NutCracker,
                new Scale(33.0f, 0.66f, 33.0f, 100.0f),
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
                new Scale(3.0f, 0.06f, 3.0f, 9.0f),
                new Scale(0.0f, 0.02f, 0.0f, 1.0f),
                new Scale(0.0f, 0.03f, 0.0f, 3.0f))
            };
        }

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
