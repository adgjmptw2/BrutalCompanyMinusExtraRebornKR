using System;
using System.Collections.Generic;
using BrutalCompanyMinus.Minus.Handlers.Modded;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Hurricane : MEvent
    {
        public override string Name() => nameof(Hurricane);

        public static Hurricane Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "날씨가 매우 험악합니다", "강풍과 폭우", "야외 활동을 즐기기엔 최악의 상황이네요" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToRemove = new List<string>() { nameof(Hallowed), nameof(Forsaken), nameof(SolarFlare), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather), nameof(MajoraMoon) };
        }

        public override bool AddEventIfOnly()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Hurricane"))
            {
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Hurricane"))
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Hurricane");
            }
        }
    }
}