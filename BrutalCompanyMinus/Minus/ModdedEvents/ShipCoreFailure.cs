using System.Collections.Generic;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class ShipCoreFailure : MEvent
    {
        public override string Name() => nameof(ShipCoreFailure);

        public static ShipCoreFailure Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 2;
            Descriptions = new List<string>() { "함선 코어 고장!", "큰일 났습니다, 모든 함선 시스템이 오프라인입니다" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            EventsToSpawnWith = new List<string>() { nameof(DoorFailure), nameof(ItemChargerFailure), 
                nameof(LeverFailure), nameof(ManualCameraFailure), nameof(TeleporterFailure), nameof(TerminalFailure), nameof(WalkieFailure), nameof(ShipLightsFailure) };
        }

        public override bool AddEventIfOnly() => !Compatibility.SuperEclipsePresent;
    }
}
