using BrutalCompanyMinus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using FacilityMeltdown.API;
using Random = System.Random;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Meltdown : MEvent
    {
        public override string Name() => nameof(Meltdown);

        public static Meltdown Instance;

        private float meltdownTime;
        private bool meltdownStarted;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "코어 붕괴 감지", "멜트다운 임박", "작고 귀여운 연기 한 줄기" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

        }

        public override bool AddEventIfOnly() => Compatibility.FacilityMeltdownPresent;

        public override void Execute()
        {
            Random random = new Random();
            this.meltdownTime = (float)random.Next(5, 100) / 100f;

            this.meltdownStarted = false;
            TimeOfDay.Instance.onTimeSync.AddListener(MeltdownClock);

        }

        public override void OnGameStart()
        {
        }

        private void MeltdownClock()
        {
            if (!meltdownStarted && TimeOfDay.Instance.normalizedTimeOfDay > meltdownTime)
            {
                meltdownStarted = true;
                MeltdownAPI.StartMeltdown("SoftDiamond.BrutalCompanyMinusExtraReborn");
            }
        }
    }
}
