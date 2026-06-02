using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Heatwave : MEvent
    {
        public override string Name() => nameof(Heatwave);

        public static Heatwave Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "온도가 상승하고 있습니다!", "폭염이 몰려옵니다!", "여기 점점 뜨거워지는데요!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(BloodMoon), nameof(MajoraMoon), nameof(SolarFlare), nameof(MeteorShower), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather) };
        }

        public override bool AddEventIfOnly() => Compatibility.LethalElementsPresent && Compatibility.WeatherRegistryPresent;

        public override void Execute()
        {
            if (Compatibility.LethalElementsPresent)
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Heatwave");
            }
        }
    }
}