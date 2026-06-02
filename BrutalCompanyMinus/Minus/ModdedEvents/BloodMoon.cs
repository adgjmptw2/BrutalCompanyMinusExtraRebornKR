using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class BloodMoon : MEvent
    {
        public override string Name() => nameof(BloodMoon);

        public static BloodMoon Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "고철을 하나도 못 챙겨오더라도 화내지 않을게요", "무슨 수를 써서라도 괴물들을 피하세요", "지금이 '블러드 문'이라는 사실을 알고 계셨나요?", "왠지 '일식(Eclipsed)'보다 더 최악인 것 같군요", "그 어디도 안전하지 않을 겁니다", "블러드 문을 찬양하라!" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToRemove = new List<string>() { nameof(Hurricane), nameof(Hallowed), nameof(Forsaken), nameof(SolarFlare), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather), nameof(MajoraMoon) };
        }

        public override bool AddEventIfOnly() => Compatibility.LegendWeathersPresent && Compatibility.WeatherRegistryPresent;

        public override void Execute()
        {
            if (Compatibility.LegendWeathersPresent)
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Blood Moon");
            }
        }
    }
}