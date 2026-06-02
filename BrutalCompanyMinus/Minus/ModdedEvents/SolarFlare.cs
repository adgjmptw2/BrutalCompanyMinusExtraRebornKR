using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SolarFlare : MEvent
    {
        public override string Name() => nameof(SolarFlare);

        public static SolarFlare Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "긴급: 태양풍 접근 중!", "초대형 태양풍이 옵니다! (지직...) 타 죽기 싫으면 뛰세요!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(Hurricane), nameof(Hallowed), nameof(Forsaken), nameof(BloodMoon), nameof(MajoraMoon), nameof(Heatwave), nameof(MeteorShower), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather) };
        }

        public override bool AddEventIfOnly() => Compatibility.LethalElementsPresent && Compatibility.WeatherRegistryPresent;

        public override void Execute()
        {
            if (Compatibility.LethalElementsPresent)
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Solar Flare");
            }
        }
    }
}