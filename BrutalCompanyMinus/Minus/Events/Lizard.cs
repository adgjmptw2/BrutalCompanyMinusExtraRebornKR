using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Lizard : MEvent
    {
        public override string Name() => nameof(Lizard);

        public static Lizard Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;

            Descriptions = new List<string>() { "안 물어요... 진짜로요", "성가신 녀석들...", "비켜!!!!!!!!", "이 녀석들이 당신에게 방귀를 뀔 텐데, 전혀 유쾌하지 않을 겁니다." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.SporeLizard,
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(5.0f, 0.1f, 5.0f, 15.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(2.0f, 0.04f, 2.0f, 6.0f),
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
