using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoMantiToilSlayer : MEvent
    {
        public override string Name() => nameof(NoMantiToilSlayer);

        public static NoMantiToilSlayer Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "날아다니는 미니건 없음", "맨티슬레이어 출입 금지 구역", "오늘은 맨티코일이 당신을 해칠 수 없습니다" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Coilhead), nameof(AntiCoilhead), nameof(ToilHead), nameof(Mantitoil), nameof(ToilSlayer), nameof(MantiToilSlayer), nameof(NoToilSlayer) };


        }

        public override bool AddEventIfOnly() => Compatibility.toilheadPresent;

        public override void Execute()
        {
            if (!Compatibility.toilheadPresent) return;
            ExecuteAllMonsterEvents();
            com.github.zehsteam.ToilHead.Api.ForceMantiSlayerMaxSpawnCount = 0;
            com.github.zehsteam.ToilHead.Api.ForceMantiSlayerSpawns = false;
            com.github.zehsteam.ToilHead.Api.ForceToilSlayerMaxSpawnCount = 0;
            com.github.zehsteam.ToilHead.Api.ForceToilHeadMaxSpawnCount = 0;
        }
    }
}
