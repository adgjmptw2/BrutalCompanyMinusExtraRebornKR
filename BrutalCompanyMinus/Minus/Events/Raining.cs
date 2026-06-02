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
    internal class Raining : MEvent
    {
        public override string Name() => nameof(Raining);

        public static Raining Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 8;
            Descriptions = new List<string>() { "여기는 비가 내리고 있네요", "비...", "아일랜드에서 볼 수 있는 유일한 풍경" };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;
        }

        public override bool AddEventIfOnly()
        {
            switch(RoundManager.Instance.currentLevel.currentWeather)
            {
                case LevelWeatherType.Rainy: return false;
                case LevelWeatherType.Stormy: return false;
                case LevelWeatherType.Flooded: return false;
            }
            return true;
        }

        public override void Execute() => Manager.SetAtmosphere(Assets.AtmosphereName.Rainy, true);
    }
}
