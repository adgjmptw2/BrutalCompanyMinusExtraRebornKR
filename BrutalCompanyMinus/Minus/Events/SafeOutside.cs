using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SafeOutside : MEvent
    {
        public override string Name() => nameof(SafeOutside);

        public static SafeOutside Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            EventsToRemove = new List<string>() { nameof(NoOldBird), nameof(NoDogs), nameof(NoGiants), nameof(NoBaboons), nameof(NoWorm), nameof(NoMasks), nameof(NoBirds), nameof(Warzone), nameof(OutsideTurrets), nameof(OutsideLandmines), nameof(Masked), nameof(AllWeather) };

            Weight = 1;
            Descriptions = new List<string>() { "밖은 안전합니다!", "바깥이 이상할 정도로 조용하네요", "벌이 좀 있을 수도 있지만 그게 전부일 겁니다", "밖을 걷다 보면 자신의 발소리가 메아리치는 게 들릴 정도입니다." };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;
        }

        public override bool AddEventIfOnly() => !Compatibility.lethalEscapePresent;

        public override void Execute()
        {
            Active = true;
            Manager.RemoveSpawn(Assets.EnemyName.Masked);
        }

        public override void OnShipLeave() => Active = false;

        public override void OnGameStart() => Active = false;
    }
}
