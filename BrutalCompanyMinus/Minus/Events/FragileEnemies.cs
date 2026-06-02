using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class FragileEnemies : MEvent
    {
        public override string Name() => nameof(FragileEnemies);

        public static FragileEnemies Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "이곳의 적들은 평소보다 조금 더 연약합니다.", "몽둥이질 한 번 정도는 덜 해도 되겠네요", "의문의 질병이 적들을 연약하게 만들고 있습니다..." };
            ColorHex = "#008000";
            Type = EventType.Good;

            EventsToRemove = new List<string>() { nameof(StrongEnemies) };

            ScaleList.Add(ScaleType.MinHp, new Scale(-2.0f, -0.04f, -6.0f, -2.0f));
            ScaleList.Add(ScaleType.MaxHp, new Scale(-1.0f, -0.03f, -4.0f, -1.0f));
        }

        public override void Execute() => Manager.AddEnemyHp(UnityEngine.Random.Range(Get(ScaleType.MinHp), Get(ScaleType.MaxHp) + 1));
    }
}
