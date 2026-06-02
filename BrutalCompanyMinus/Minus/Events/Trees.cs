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
    internal class Trees : MEvent
    {
        public override string Name() => nameof(Trees);

        public static Trees Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 4;
            Descriptions = new List<string>() { "나무들", "더 많은 나무들", "나무가 나무처럼 생겼군요" };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;

            EventsToRemove = new List<string>() { nameof(LeaflessBrownTrees), nameof(LeaflessTrees) };

            ScaleList.Add(ScaleType.MinDensity, new Scale(0.018f, 0.0f, 0.018f, 0.018f));
            ScaleList.Add(ScaleType.MaxDensity, new Scale(0.025f, 0.0f, 0.025f, 0.025f));
        }

        public override void Execute()
        {
            if (LeaflessTrees.Instance.Executed || LeaflessBrownTrees.Instance.Executed) return;

            Net.Instance.outsideObjectsToSpawn.Add(new OutsideObjectsToSpawnMethod(UnityEngine.Random.Range(Getf(ScaleType.MinDensity) * 0.5f, Getf(ScaleType.MaxDensity) * 0.5f), (int)Assets.ObjectName.Tree));
        }
    }
}
