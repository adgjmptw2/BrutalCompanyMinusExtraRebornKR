using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoGhosts : MEvent
    {
        public override string Name() => nameof(NoGhosts);

        public static NoGhosts Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "유령 없음", "더 이상의 초자연적 현상은 없습니다", "고스트버스터즈가 이 시설의 유령들을 소탕했습니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(LittleGirl), nameof(FacilityGhost), nameof(Hell), nameof(Walkers), nameof(Herobrine), nameof(SlenderMan) };
        }

        public override bool AddEventIfOnly() => Manager.SpawnExists(Assets.EnemyName.GhostGirl) || Manager.SpawnExists("WalkerType") || Manager.SpawnExists("Herobrine") || Manager.SpawnExists("Football");

        public override void Execute()
        {
            Manager.RemoveSpawn(Assets.EnemyName.GhostGirl);
            Manager.RemoveSpawn("WalkerType");
            Manager.RemoveSpawn("Herobrine");
            Manager.RemoveSpawn("Football");
        }
    }
}
