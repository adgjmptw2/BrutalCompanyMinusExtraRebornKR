using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Walkers : MEvent
    {
        public override string Name() => nameof(Walkers);

        public static Walkers Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "워커가 시설 내부에 진입했습니다!", "여분 속옷 챙겨오셨길 바랍니다. 지릴 수도 있거든요.", "정신이 나갈 것 같습니다... 아니면 이미 나갔거나요." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                "WalkerType",
                new Scale(8.0f, 0.4f, 8.0f, 32.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(1.0f, 0.034f, 1.0f, 3.0f),
                new Scale(0.0f, 0.022f, 0.0f, 1.0f),
                new Scale(0.0f, 0.034f, 0.0f, 2.0f))
            };
        }

        public override bool AddEventIfOnly() => Compatibility.diversityPresent;

        public override void Execute() => ExecuteAllMonsterEvents();
    }
}
