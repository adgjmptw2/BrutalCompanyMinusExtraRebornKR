using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ItsPlaytime : MEvent
    {
        public override string Name() => nameof(ItsPlaytime);

        public static ItsPlaytime Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2; //2
            Descriptions = new List<string>() { "다음 장을 준비하세요...", "후회하게 될 겁니다...", "작은 놈들과 더 큰 놈들" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToSpawnWith = new List<string>() { nameof(PlaytimeBig), nameof(Critters) };
        }

        public override bool AddEventIfOnly() => Compatibility.PlaytimePresent && Compatibility.CrittersPresent;
    }
}
