using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using System.Collections;
using HarmonyLib;
using UnityEngine.Rendering;
using BrutalCompanyMinus.Minus.MonoBehaviours;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class FullAccess : MEvent
    {
        public override string Name() => nameof(FullAccess);

        public static FullAccess Instance;

        public static bool Active = false;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "모든 것이 열려 있습니다!", "누군가 문을 열어두었네요", "모든 도둑들의 꿈", "진정한 탐험을 경험하십시오", "여기선 열쇠가 필요 없을 겁니다" };
            ColorHex = "#008000";
            Type = EventType.Good;

            EventsToRemove = new List<string>() { nameof(FacilityGhost) };
        }

        public override void Execute() => Active = true;

        public override void OnShipLeave() => Active = false;

        public override void OnGameStart() => Active = false;
    }
}
