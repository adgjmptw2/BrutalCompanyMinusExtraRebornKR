using BrutalCompanyMinus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class OutsideTurrets : MEvent
    {
        public override string Name() => nameof(OutsideTurrets);

        public static OutsideTurrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "포탑들이 나무들 사이에 숨어 있습니다...", "총알 세례를 즐기시길 바랍니다", "밖으로 나가기 참 좋은 날이네요." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            EventsToSpawnWith = new List<string>() { nameof(Trees) };
            EventsToRemove = new List<string>() { nameof(LeaflessBrownTrees), nameof(LeaflessTrees) };

            ScaleList.Add(ScaleType.MinDensity, new Scale(0.0008f, 0.000024f, 0.0008f, 0.0032f));
            ScaleList.Add(ScaleType.MaxDensity, new Scale(0.0012f, 0.000036f, 0.0012f, 0.0048f));
        }

        public override void Execute()
        {
            Manager.insideObjectsToSpawnOutside.Add(new Manager.ObjectInfo(Assets.GetObject(Assets.ObjectName.Turret), UnityEngine.Random.Range(Getf(ScaleType.MinDensity), Getf(ScaleType.MaxDensity))));
        }
    }
}
