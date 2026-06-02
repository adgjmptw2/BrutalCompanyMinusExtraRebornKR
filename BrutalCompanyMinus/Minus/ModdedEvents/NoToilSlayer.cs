using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoToilSlayer : MEvent
    {
        public override string Name() => nameof(NoToilSlayer);

        public static NoToilSlayer Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "오늘은 내부 미니건 없음", "토일슬레이어 없음", "이 구역은 토일슬레이어 출입 금지입니다" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Coilhead), nameof(AntiCoilhead), nameof(ToilHead), nameof(MantiToilSlayer), nameof(ToilSlayer), nameof(NoMantiToilSlayer),/* nameof(NoMantitoil),*/ nameof(AllSlayers), nameof(NoSlayers) };

        }

        public override bool AddEventIfOnly() => Compatibility.toilheadPresent;

        public override void Execute()
        {
            if (!Compatibility.toilheadPresent) return;
          //  ExecuteAllMonsterEvents();
            com.github.zehsteam.ToilHead.Api.ForceToilSlayerMaxSpawnCount = 0;
            com.github.zehsteam.ToilHead.Api.ForceToilSlayerSpawns = false;
            com.github.zehsteam.ToilHead.Api.ForceMantiSlayerMaxSpawnCount = 0;
            com.github.zehsteam.ToilHead.Api.ForceToilHeadMaxSpawnCount = 0;
        }
    }
}
