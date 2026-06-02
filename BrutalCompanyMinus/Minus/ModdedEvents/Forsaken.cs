using System;
using System.Collections.Generic;
using BrutalCompanyMinus.Minus.Handlers.Modded;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Forsaken : MEvent
    {
        public override string Name() => nameof(Forsaken);

        public static Forsaken Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "당신은 변한 모습으로 돌아오게 될 겁니다" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(Hallowed), nameof(Hurricane), nameof(SolarFlare), nameof(Windy), nameof(Gloomy), nameof(Raining), nameof(AllWeather), nameof(MajoraMoon) };
        }

        public override bool AddEventIfOnly()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Forsaken"))
            {
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            if (Compatibility.IsModPresent("mrov.WeatherRegistry") && CustomWeather.isWeatherPresent("Forsaken"))
            {
                Handlers.Modded.CustomWeather.SetCustomWeather("Forsaken");
            }
        }
    }
}