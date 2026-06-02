using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class KamikazieBugs : MEvent
    {
        public override string Name() => nameof(KamikazieBugs);

        public static KamikazieBugs Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "내가 당신이라면 굳이 쟤들을 화나게 하지 않을 거예요.", "비축 벌레한테 장기가 있다는 사실을 아시나요? 글쎄, 얘들은 장기 대신 '폭탄'을 갖고 있네요...", "이 녀석들의 군단과 마주치길 기원합니다.", "자폭하는 해충들이 시설 내부를 침공했습니다." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.kamikazieBug,
                new Scale(10.0f, 0.4f, 10.0f, 50.0f),
                new Scale(4.0f, 0.08f, 4.0f, 12.0f),
                new Scale(2.0f, 0.08f, 2.0f, 10.0f),
                new Scale(3.0f, 0.12f, 3.0f, 15.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f))
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
