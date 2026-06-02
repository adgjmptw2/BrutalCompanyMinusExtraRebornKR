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
    internal class VeryEarlyShip : MEvent
    {
        public override string Name() => nameof(VeryEarlyShip);

        public static VeryEarlyShip Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "함선이 아주 적절한 시간에 도착했습니다.", "평소보다 일찍 도착했군요!", "일출 전입니다!" };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;

            EventsToRemove = new List<string>() { nameof(LateShip), nameof(EarlyShip), nameof(VeryLateShip), nameof(Hell) };

            ScaleList.Add(ScaleType.TimeMin, new Scale(-480.0f, -0.55f, -480.0f, -240.0f));
            ScaleList.Add(ScaleType.TimeMax, new Scale(-342.0f, -0.55f, -342.0f, -300.0f));
        }

        public override void Execute() => Net.Instance.MoveTimeServerRpc(UnityEngine.Random.Range(Getf(ScaleType.TimeMin), Getf(ScaleType.TimeMax)));
    }
}
