using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Nothing : MEvent
    {
        public override string Name() => nameof(Nothing);

        public static Nothing Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 8;
            Descriptions = new List<string>() { "--없음--", "진짜 아무것도 없음", "와, 정말 아무것도 없네요", "---", "구름들이 행복해 보입니다", "놀랍게도, 아무 일도 일어나지 않았습니다.", "이걸 다행이라고 생각하세요" };
            ColorHex = "#FFFFFF";
            Type = EventType.Neutral;
        }
    }
}
