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
    internal class OutsideLandmines : MEvent
    {
        public override string Name() => nameof(OutsideLandmines);

        public static OutsideLandmines Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "외부에 지뢰가 매설되어 있습니다.", "이 시설은 건물 밖에도 자체적인 부비트랩을 설치해 뒀군요.", "발밑 조심하세요... 밖에서도 말이죠", "밖으로 발을 내딛는 순간, 파멸을 맞이할 겁니다!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.MinDensity, new Scale(0.003f, 0.00012f, 0.003f, 0.015f));
            ScaleList.Add(ScaleType.MaxDensity, new Scale(0.0042f, 0.000168f, 0.0042f, 0.021f));
        }

        public override void Execute()
        {
            Manager.insideObjectsToSpawnOutside.Add(new Manager.ObjectInfo(Assets.GetObject(Assets.ObjectName.Landmine), UnityEngine.Random.Range(Getf(ScaleType.MinDensity), Getf(ScaleType.MaxDensity))));
        }
    }
}
