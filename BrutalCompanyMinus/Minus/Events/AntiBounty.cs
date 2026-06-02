using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class AntiBounty : MEvent
    {
        public static bool AntiBountyActive = false;
        public override string Name() => nameof(AntiBounty);

        public static AntiBounty Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "회사는 야생 동물 보호 구역을 훼손한 것에 대해 벌금을 부과합니다", "평화주의자 루트", "살생의 대가를 치르게 될 것입니다..." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToRemove = new List<string>() { nameof(Bounty) };

            ScaleList.Add(ScaleType.MinValue, new Scale(20.0f, 0.4f, 20.0f, 60.0f));
            ScaleList.Add(ScaleType.MaxValue, new Scale(30.0f, 0.6f, 30.0f, 90.0f));
        }

        public override void Execute()
        {
            Handlers.AntiBounty.enemyObjectIDs.Clear();
            AntiBountyActive = true;
        }

        public override void OnShipLeave()
        {
            AntiBountyActive = false;
        }
        public override void OnGameStart()
        {
            AntiBountyActive = false;
        }
    }
}
