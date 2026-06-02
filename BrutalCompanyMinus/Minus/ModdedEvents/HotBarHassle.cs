using BrutalCompanyMinus.Minus.Handlers.Modded;
using System.Collections.Generic;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class HotBarHassle : MEvent
    {
        public override string Name() => nameof(HotBarHassle);

        public static HotBarHassle Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "어떤 아이템을 먼저 챙기실 건가요?", "이번엔 가볍게 다녀오죠", "전부 다 가져가긴 힘들겠는데요" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
            EventsToRemove = new List<string>() { nameof(HotBarMania) };
        }

        public override bool AddEventIfOnly() => Compatibility.HotBarPlusPresent;

        public override void Execute()
        {
            if (Compatibility.HotBarPlusPresent)
            {
                Net.Instance.ResizeHotbarRandomlySmallServerRpc();
            }
        }

        public override void OnShipLeave()
        {
            if (Compatibility.HotBarPlusPresent)
            {
                // Reset the hotbar to the original size
                Net.Instance.ResetHotbarServerRpc();
            }
        }
    }
}