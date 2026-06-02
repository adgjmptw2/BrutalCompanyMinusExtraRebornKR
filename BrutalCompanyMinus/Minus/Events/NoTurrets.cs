using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoTurrets : MEvent
    {
        public override string Name() => nameof(NoTurrets);

        public static NoTurrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "터렛 없음", "더 이상 가정 보안 시스템이 필요 없습니다.", "이 행성은 그 폭정으로부터 안전합니다." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Turrets), nameof(OutsideTurrets), nameof(Warzone), nameof(GrabbableTurrets), nameof(Hell), nameof(MobileTurrets) };
        }

        public override bool AddEventIfOnly() => RoundManager.Instance.currentLevel.spawnableMapObjects.ToList().Exists(x => x.prefabToSpawn.name == Assets.ObjectNameList[Assets.ObjectName.Turret]);

        public override void Execute()
        {
            Manager.RemoveSpawn("WalkerTurret");

            AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f));

            foreach (SpawnableMapObject obj in RoundManager.Instance.currentLevel.spawnableMapObjects)
            {
                if (obj.prefabToSpawn.name == Assets.ObjectNameList[Assets.ObjectName.Turret])
                {
                    obj.numberToSpawn = curve;
                }
            }
        }

    }
}
