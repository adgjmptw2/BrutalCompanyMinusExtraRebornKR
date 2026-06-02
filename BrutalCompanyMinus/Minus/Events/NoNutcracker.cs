using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoNutcracker : MEvent
    {
        public override string Name() => nameof(NoNutcracker);

        public static NoNutcracker Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "병정이 없네요", "움직이는 걸 허락받았어요", "여기서는 낭심 보호대를 가져올 필요가 없습니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Nutcracker), nameof(NutSlayer), nameof(Hell), nameof(HolidaySeason) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.NutCracker);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.NutCracker);
    }
}
