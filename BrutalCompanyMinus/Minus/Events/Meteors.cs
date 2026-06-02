using BrutalCompanyMinus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Meteors : MEvent
    {
        public override string Name() => nameof(Meteors);

        public static Meteors Instance;

        //private readonly int meteorStartTime = (TimeOfDay.Instance.hour = 5);

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "가끔은 하늘을 올려다보는 것도 나쁘지 않겠네요", "유성우다, 대피하세요!", "낙석이라니... 참 잘 돌아가네요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(Raining), nameof(Gloomy), nameof(HeavyRain) };
        }

        public override void Execute() 
        {
            TimeOfDay.Instance.overrideMeteorChance = 1000;
            TimeOfDay.Instance.meteorShowerAtTime = 6f/* (float)meteorStartTime*/;
        }

        public override void OnShipLeave()
        {
            TimeOfDay.Instance.overrideMeteorChance = -1;
            TimeOfDay.Instance.meteorShowerAtTime = -1f;
        }

        public override void OnGameStart()
        {
            TimeOfDay.Instance.overrideMeteorChance = -1;
            TimeOfDay.Instance.meteorShowerAtTime = -1f;
        }
    }
}
