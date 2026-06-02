using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Masked : MEvent
    {
        public override string Name() => nameof(Masked);

        public static Masked Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "친구들!!", "새로운 친구들에게 인사하세요", "참 사랑스러운 녀석들이네요", "이 일이 끝나면 불신지옥에 빠질지도 모릅니다", "이 신입은 누구죠???" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.Masked,
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(1.0f, 0.03f, 1.0f, 4.0f),
                new Scale(1.0f, 0.05f, 1.0f, 6.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f))
            };
        }

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
