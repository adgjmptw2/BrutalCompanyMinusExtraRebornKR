using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Coilhead : MEvent
    {
        public override string Name() => nameof(Coilhead);

        public static Coilhead Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "시설 내에서 코일헤드가 탐지되었습니다!", "격리 실패!", "그들에게 등을 돌리지 마십시오...", "잘린 머리가 보통 4~5초 동안은 의식을 유지한다는 사실을 알고 계셨나요?" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.CoilHead,
                new Scale(20.0f, 0.8f, 20.0f, 100.0f),
                new Scale(5.0f, 0.2f, 5.0f, 25.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
                new Scale(2.0f, 0.06f, 2.0f, 8.0f),
                new Scale(0.0f, 0.02f, 0.0f, 1.0f),
                new Scale(0.0f, 0.03f, 0.0f, 3.0f))
            };
        }

        public override void Execute()
        {
            if (Configuration.enforceEscapeModChecks.Value && !Compatibility.StarLancereNemyEscapePresent)
            {
                Instance.monstersToSpawn[0].minOutside = new Scale(0f, 0f, 0f, 0f);
                Instance.monstersToSpawn[0].maxOutside = new Scale(0f, 0f, 0f, 0f);
                Instance.monstersToSpawn[0].outsideSpawnRarity = new Scale(0f, 0f, 0f, 0f);
            }

            ExecuteAllMonsterEvents();
        }
    }
}
