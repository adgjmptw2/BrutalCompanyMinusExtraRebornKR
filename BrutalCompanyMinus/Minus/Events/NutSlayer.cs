using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NutSlayer : MEvent
    {
        public override string Name() => nameof(NutSlayer);

        public static NutSlayer Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "호두 살인마가 시설 내부에 있습니다...", "즐거운 시간 되시길", "이 녀석한테 둠(DOOM) 브금을 깔아줘야겠는데.", "신조차 그로부터 당신을 구하지 못할 겁니다." };
            ColorHex = "#280000";
            Type = EventType.VeryBad;

            EventsToSpawnWith = new List<string>() { nameof(Gloomy), nameof(Thumpers), nameof(Spiders), nameof(Masked) };
            EventsToRemove = new List<string>() { nameof(HeavyRain), nameof(Raining) };

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.nutSlayer,
                new Scale(1.0f, 0.04f, 1.0f, 5.0f),
                new Scale(1.0f, 0.02f, 1.0f, 3.0f),
                new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f))
            };

            ScaleList.Add(ScaleType.SpawnMultiplier, new Scale(1.25f, 0.0075f, 1.25f, 2.0f));
            ScaleList.Add(ScaleType.SpawnCapMultiplier, new Scale(1.4f, 0.016f, 1.4f, 3.0f));
        }

        public override void Execute() 
        {
            ExecuteAllMonsterEvents();

            Manager.MultiplySpawnChance(RoundManager.Instance.currentLevel, Getf(ScaleType.SpawnMultiplier));
            Manager.MultiplySpawnCap(Getf(ScaleType.SpawnCapMultiplier));
        } 
    }
}
