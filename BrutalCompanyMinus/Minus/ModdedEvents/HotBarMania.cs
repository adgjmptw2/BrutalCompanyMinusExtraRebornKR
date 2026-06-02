using BrutalCompanyMinus.Minus.Handlers.Modded;
using System.Collections.Generic;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class HotBarMania : MEvent
    {
        public override string Name() => nameof(HotBarMania);

        public static HotBarMania Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "예상보다 일찍 퇴근할 수 있겠는데요", "왔다 갔다 덜 해도 되겠어요", "이 정도면 해볼 만하죠!" };
            ColorHex = "#008000";
            Type = EventType.Good;
            EventsToRemove = new List<string>() { nameof(HotBarHassle) };
        }

        public override bool AddEventIfOnly() => Compatibility.HotBarPlusPresent;

        public override void Execute()
        {
            if (Compatibility.HotBarPlusPresent)
            {
                Net.Instance.ResizeHotbarRandomlyServerRpc();
            }
        }

        public override void OnShipLeave()
        {
            if (Compatibility.HotBarPlusPresent)
            {
                Net.Instance.ResetHotbarServerRpc();
            }
        }
    }
}