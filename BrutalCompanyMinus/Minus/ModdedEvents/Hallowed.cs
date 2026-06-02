using System;
using System.Collections.Generic;
using BrutalCompanyMinus.Minus.Handlers.Modded;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Hallowed : MEvent
    {
        public override string Name() => nameof(Hallowed);

        public static Hallowed Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "할로윈의 영혼이 우리와 함께합니다", "으스스한 기운이 가득하네요", "사탕을 안 주면 장난칠 거예요!" };
            ColorHex = "#FFA500";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(Hurricane), nameof(Forsaken), nameof(SolarFlare), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather), nameof(MajoraMoon) };
        }

        public override bool AddEventIfOnly()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Hallowed"))
            {
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Hallowed"))
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Hallowed");
            }
        }
    }
}