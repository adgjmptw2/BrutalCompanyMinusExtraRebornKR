using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoLandmines : MEvent
    {
        public override string Name() => nameof(NoLandmines);

        public static NoLandmines Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "지뢰 없음", "오늘은 바닥을 경계할 필요가 없겠네요.", "지뢰는 기대하지 마세요" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Landmines), nameof(OutsideLandmines), nameof(Warzone), nameof(GrabbableLandmines), nameof(Hell), nameof(Roomba) };
        }

        public override bool AddEventIfOnly() => RoundManager.Instance.currentLevel.spawnableMapObjects.ToList().Exists(x => x.prefabToSpawn.name == Assets.ObjectNameList[Assets.ObjectName.Landmine]) || Manager.SpawnExists("Boomba");

        public override void Execute()
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f));

            foreach (SpawnableMapObject obj in RoundManager.Instance.currentLevel.spawnableMapObjects)
            {
                if (obj.prefabToSpawn.name == Assets.ObjectNameList[Assets.ObjectName.Landmine])
                {
                    obj.numberToSpawn = curve;
                }
            }
            
            Manager.RemoveSpawn("Boomba");
        }
    }
}
