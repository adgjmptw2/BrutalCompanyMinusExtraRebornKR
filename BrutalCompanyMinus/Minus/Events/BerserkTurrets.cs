using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Unity.Netcode;
using UnityEngine;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using HarmonyLib;
using UnityEngine.PlayerLoop;

namespace BrutalCompanyMinus.Minus.Events
{
    [HarmonyPatch]
    internal class BerserkTurrets : MEvent
    {
        public override string Name() => nameof(BerserkTurrets);

        public static BerserkTurrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "완전한 지옥", "광기... 그 애니메이션 말고요.", "시야에 들지 마십시오", "아주 끔찍한 하루가 될 겁니다!", "엄청난 소음 주의!" };
            ColorHex = "#280000";
            Type = EventType.VeryBad;
            EventsToSpawnWith = new List<string>() { nameof(Turrets) };
        }

        //public override bool AddEventIfOnly() => Assets.ReadSettingEarly(Paths.ConfigPath + "\\BrutalCompanyMinusExtraReborn\\CoreProperties.cfg", "Enable Special Events?");

        public override void Execute()
        {
            Active = true;
            GameObject netObject = new GameObject("BerserkTurretsEvent");
            netObject.AddComponent<BerserkTurretsNet>();
        }

        public override void OnShipLeave()
        {
            Active = false;
        }

        public override void OnGameStart()
        {
            Active = false;
        }
    }
}