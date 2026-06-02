using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class GrabbableTurrets : MEvent
    {
        public static bool Active = false;
        public override string Name() => nameof(GrabbableTurrets);

        public static GrabbableTurrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "포탑 몇 개가 고철로 변했습니다...", "이제 포탑 중 일부를 공식적으로 팔 수 있습니다. 즐기세요!", "침입자 방어용 자동 방어 시스템으로 집에 가져가도 되겠네요." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.Rarity, new Scale(0.33f, 0.0066f, 0.33f, 1.0f));
            ScaleList.Add(ScaleType.MinAmount, new Scale(2.0f, 0.06f, 2.0f, 8.0f));
            ScaleList.Add(ScaleType.MaxAmount, new Scale(3.0f, 0.09f, 3.0f, 12.0f));
        }

        public override bool AddEventIfOnly() => RoundManager.Instance.currentLevel.spawnableMapObjects.ToList().Exists(x => x.prefabToSpawn.name == Assets.ObjectNameList[Assets.ObjectName.Turret]);

        public override void Execute()
        {
            Active = true;

            var turret = new IndoorMapHazard()
            {
                hazardType = new IndoorMapHazardType()
                {
                    prefabToSpawn = Assets.GetObject(Assets.ObjectName.Turret),
                    spawnFacingAwayFromWall = true,
                    spawnFacingWall = false,
                    spawnWithBackToWall = false,
                    spawnWithBackFlushAgainstWall = false,
                    requireDistanceBetweenSpawns = false,
                    disallowSpawningNearEntrances = false
                },
                numberToSpawn = new AnimationCurve(new Keyframe(0f, Get(ScaleType.MinAmount)), new Keyframe(1f, Get(ScaleType.MaxAmount)))
            };

            EventManager.hazards.Add(turret);

            RoundManager.Instance.currentLevel.indoorMapHazards = RoundManager.Instance.currentLevel.indoorMapHazards.AddToArray(turret);
        }

        public override void OnShipLeave() => Active = false;

        public override void OnGameStart() => Active = false;
    }
}
