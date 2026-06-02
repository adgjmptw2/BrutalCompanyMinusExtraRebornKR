using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoSpiders : MEvent
    {
        public override string Name() => nameof(NoSpiders);

        public static NoSpiders Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "다리 8개 달린 생물 없음", "거미 공포증 주의보 해제", "시설 안에 진공청소기를 들고 올 필요가 없습니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Spiders), nameof(Arachnophobia), nameof(Hell), nameof(NutSlayer) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.BunkerSpider);

        public override void Execute() => Manager.RemoveSpawn(Assets.EnemyName.BunkerSpider);
    }
}
