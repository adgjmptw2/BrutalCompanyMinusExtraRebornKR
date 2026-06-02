using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class LittleGirl : MEvent
    {
        public override string Name() => nameof(LittleGirl);

        public static LittleGirl Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "그저 당신을 만지고 싶을 뿐이에요", "머리가 터지고 싶나요?", "그저 당신과 놀고 싶을 뿐입니다", "죽은 아이들의 유치원" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.GhostGirl,
                new Scale(20.0f, 0.8f, 20.0f, 100.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(3.0f, 0.06f, 3.0f, 9.0f),
                new Scale(3.0f, 0.09f, 3.0f, 12.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f))
            };
        }

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
