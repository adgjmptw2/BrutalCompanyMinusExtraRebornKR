using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using HarmonyLib;
using GameNetcodeStuff;
using BrutalCompanyMinus.Minus.Handlers.Modded;
using BrutalCompanyMinus;
using Steamworks.Ugc;
using BrutalCompanyMinus.Minus.Handlers;
using System.ComponentModel.Design;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using BepInEx;

namespace BrutalCompanyMinus.Minus.Events
{
    [HarmonyPatch]
    internal class TrapsFailure : MEvent
    {
        public override string Name() => nameof(TrapsFailure);

        public static TrapsFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "고장 난 함정들", "오늘은 함정이 전혀 문제가 되지 않습니다", "함정이 있더라도 오늘은 걱정 마세요!" };
            ColorHex = "#00FF00";
            Type = EventType.VeryGood;

            EventsToRemove = new List<string>() { nameof(BerserkTurrets) };
        }

        public override void Execute()
        {
            GameObject netObject = new GameObject("TrapsFailureEvent");
            netObject.AddComponent<TrapsFailureNet>();
        }
        public override void OnShipLeave()
        {

            Active = false;
        }
        public override void OnGameStart()
        {
            Active = false;
        }

        public override void OnLocalDisconnect()
        {
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Landmine), nameof(Landmine.OnTriggerEnter))]
        public static bool InterruptLandmine()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Landmine), nameof(Landmine.TriggerOtherMineDelayed))]
        public static bool InterruptLandmineOther()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Landmine), nameof(Landmine.OnTriggerExit))]
        public static bool InterruptLandmineExit()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Landmine), nameof(Landmine.Detonate))]
        public static bool InterruptLandmineAttack()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Turret), nameof(Turret.Update))]
        public static bool InterruptTurret()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SpikeRoofTrap), nameof(SpikeRoofTrap.Update))]
        public static bool InterruptSpikeRoofTrap()
        {
            // Interrupt the charger method  
            if (Events.TrapsFailure.Instance.Active)
            {

                return false;
            }
            return true;
        }
    }
}