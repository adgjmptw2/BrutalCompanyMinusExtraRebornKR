using System.Collections.Generic;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Dweller : MEvent
    {
        public override string Name() => nameof(Dweller);

        public static Dweller Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;//3
            Descriptions = new List<string>() { "맨이터... 하지만 실외입니다, 행운을 빌어요", "겁먹지 마세요" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                "CaveDweller",
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                new Scale(1.0f, 0.0f, 1.0f, 1.0f))
            };
        }

        public override void Execute()
        {
            ExecuteAllMonsterEvents();
        }
    }
}
