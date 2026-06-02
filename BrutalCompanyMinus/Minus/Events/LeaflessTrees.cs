using BrutalCompanyMinus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using static BrutalCompanyMinus.Net;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class LeaflessTrees : MEvent
    {
        public override string Name() => nameof(LeaflessTrees);

        public static LeaflessTrees Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 4;
            Descriptions = new List<string>() { "이 나무들은 죽은 것 같네요", "이 나무들도 이제 예전 같지 않군요", "겨울입니다" };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;

            EventsToRemove = new List<string>() { nameof(Trees), nameof(LeaflessBrownTrees) };

            ScaleList.Add(ScaleType.MinDensity, new Scale(0.018f, 0.0f, 0.018f, 0.018f));
            ScaleList.Add(ScaleType.MaxDensity, new Scale(0.025f, 0.0f, 0.025f, 0.025f));
        }

        public override void Execute()
        {
            if (LeaflessBrownTrees.Instance.Executed || Trees.Instance.Executed) return;

            Net.Instance.outsideObjectsToSpawn.Add(new OutsideObjectsToSpawnMethod(UnityEngine.Random.Range(Getf(ScaleType.MinDensity) * 0.25f, Getf(ScaleType.MaxDensity) * 0.25f), (int)Assets.ObjectName.TreeLeafless2));
            Net.Instance.outsideObjectsToSpawn.Add(new OutsideObjectsToSpawnMethod(UnityEngine.Random.Range(Getf(ScaleType.MinDensity) * 0.25f, Getf(ScaleType.MaxDensity) * 0.25f), (int)Assets.ObjectName.TreeLeafless3));
        }
    }
}
