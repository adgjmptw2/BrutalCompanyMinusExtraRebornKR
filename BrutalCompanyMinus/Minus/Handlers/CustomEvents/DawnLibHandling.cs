using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BrutalCompanyMinus.Minus.CustomEvents;
using Dawn;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Handlers.CustomEvents
{
    public class DawnLibHandling
    {
        public static Queue<GeneralCustomEvent.HazardEvent> eventQueue = new Queue<GeneralCustomEvent.HazardEvent>();

        public static Dictionary<string, DawnSnapshot> originalStates = new Dictionary<string, DawnSnapshot>();

        /// <summary>  
        /// This struct helps provide a template for storing original map object data.  
        /// </summary>  
        public struct DawnSnapshot
        {
            public object OutsideWeights;
            public object InsideWeights;
            public bool FacingAway, FacingWall, BackToWall, BackFlush, ReqDist, DisallowNear;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool ProcessHazardEvents()
        {
            bool dataFound = false;
            foreach (MEvent _event in EventManager.currentEvents)
            {
                Log.LogInfo($"Event name: {_event.GetType().Name}");
                if (_event is GeneralCustomEvent customEvent)
                {
                    Log.LogInfo($"Event {customEvent.Name()} is a GeneralCustomEvent, checking hazard events...");
                    foreach (GeneralCustomEvent.HazardEvent hazard in customEvent.hazardEvents)
                    {
                        Log.LogInfo("Processing...");
                        dataFound |= ProcessMapObject(hazard);
                    }
                }
            }

            return dataFound;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool IsDawnManaged(string hazardName)
        {
            bool managed = false;
            foreach (DawnMapObjectInfo mapObjectInfo in LethalContent.MapObjects.Values)
            {
                if (mapObjectInfo.ShouldSkipIgnoreOverride() || mapObjectInfo.GetMapObjectPrefab()?.name != hazardName)
                    continue;

                DawnOutsideMapObjectInfo? outsideInfo = mapObjectInfo.OutsideInfo;
                DawnInsideMapObjectInfo? insideInfo = mapObjectInfo.InsideInfo;

                if (outsideInfo != null || insideInfo != null)
                {
                    managed = true;
                    break;
                }
            }
            Log.LogInfo($"{hazardName} managed: {managed}");

            return managed;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool ProcessMapObject(GeneralCustomEvent.HazardEvent hazard)
        {
            bool processed = false;
            foreach (DawnMapObjectInfo mapObjectInfo in LethalContent.MapObjects.Values)
            {
                if (mapObjectInfo.ShouldSkipIgnoreOverride() || mapObjectInfo.GetMapObjectPrefab()?.name != hazard.hazardObject.name)
                    continue;

                string hazardName = mapObjectInfo.GetMapObjectPrefab()!.name;

                if (!originalStates.ContainsKey(hazardName))
                {
                    originalStates[hazardName] = new DawnSnapshot
                    {
                        OutsideWeights = mapObjectInfo.OutsideInfo?.SpawnWeights,
                        InsideWeights = mapObjectInfo.InsideInfo?.SpawnWeights,
                        FacingAway = mapObjectInfo.InsideInfo?.IndoorMapHazardType.spawnFacingAwayFromWall ?? false,
                        FacingWall = mapObjectInfo.InsideInfo?.IndoorMapHazardType.spawnFacingWall ?? false,
                        BackToWall = mapObjectInfo.InsideInfo?.IndoorMapHazardType.spawnWithBackToWall ?? false,
                        BackFlush = mapObjectInfo.InsideInfo?.IndoorMapHazardType.spawnWithBackFlushAgainstWall ?? false,
                        ReqDist = mapObjectInfo.InsideInfo?.IndoorMapHazardType.requireDistanceBetweenSpawns ?? false,
                        DisallowNear = mapObjectInfo.InsideInfo?.IndoorMapHazardType.disallowSpawningNearEntrances ?? false
                    };
                }

                DawnOutsideMapObjectInfo? outsideInfo = mapObjectInfo.OutsideInfo;
                DawnInsideMapObjectInfo? insideInfo = mapObjectInfo.InsideInfo;

                NamespacedKey<DawnMoonInfo> moonKey = RoundManager.Instance.currentLevel.GetDawnInfo().TypedKey;

                float computedWeight = UnityEngine.Random.Range(
                    hazard.minDensity.Computef(hazard.Type),
                    hazard.maxDensity.Computef(hazard.Type)
                );
                Log.LogInfo($"Raw computed: {computedWeight}");

                if (outsideInfo != null)
                {
                    computedWeight = (int)Mathf.Clamp(computedWeight * Manager.terrainArea, 0f, 1000f);
                    Log.LogInfo($"Outside adjusted computed: {computedWeight} Area: {Manager.terrainArea}");

                    outsideInfo.SpawnWeights = new CurveTableBuilder<DawnMoonInfo, SpawnWeightContext>()
                        .AddCurve(moonKey, AnimationCurve.Constant(0, 1, computedWeight)).Build();

                    mapObjectInfo.OutsideInfo = outsideInfo;
                    processed = true;
                }

                if (insideInfo != null)
                {
                    int insideWeight = (int)computedWeight;
                    Log.LogInfo("Inside adjusted computed: " + insideWeight);
                    insideInfo.SpawnWeights = new CurveTableBuilder<DawnMoonInfo, SpawnWeightContext>()
                        .AddCurve(moonKey, AnimationCurve.Constant(0, 1, insideWeight))
                        .Build();
                    IndoorMapHazardType hazardType = insideInfo.IndoorMapHazardType;
                    hazardType.spawnFacingAwayFromWall = hazard.facingAwayFromWall;
                    hazardType.spawnFacingWall = hazard.facingWall;
                    hazardType.spawnWithBackToWall = hazard.backToWall;
                    hazardType.spawnWithBackFlushAgainstWall = hazard.backFlushWithWall;
                    hazardType.requireDistanceBetweenSpawns = hazard.requireDistanceBetween;
                    hazardType.disallowSpawningNearEntrances = hazard.disallowNearEntrances;
                    insideInfo.IndoorMapHazardType = hazardType;

                    mapObjectInfo.InsideInfo = insideInfo;
                    processed = true;
                }

                if (processed) break;
            }

            return processed;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool OnEventStart()
        {
            return ProcessHazardEvents();
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void OnEventEnd()
        {
            foreach (var entry in originalStates)
            {
                foreach (DawnMapObjectInfo mapObjectInfo in LethalContent.MapObjects.Values)
                {
                    if (mapObjectInfo.GetMapObjectPrefab()?.name == entry.Key)
                    {
                        if (mapObjectInfo.OutsideInfo != null)
                            mapObjectInfo.OutsideInfo.SpawnWeights = (ProviderTable<AnimationCurve?, DawnMoonInfo, SpawnWeightContext>?)entry.Value.OutsideWeights;

                        if (mapObjectInfo.InsideInfo != null)
                        {
                            mapObjectInfo.InsideInfo.SpawnWeights = (ProviderTable<AnimationCurve?, DawnMoonInfo, SpawnWeightContext>?)entry.Value.InsideWeights;
                            IndoorMapHazardType ht = mapObjectInfo.InsideInfo.IndoorMapHazardType;
                            ht.spawnFacingAwayFromWall = entry.Value.FacingAway;
                            ht.spawnFacingWall = entry.Value.FacingWall;
                            ht.spawnWithBackToWall = entry.Value.BackToWall;
                            ht.spawnWithBackFlushAgainstWall = entry.Value.BackFlush;
                            ht.requireDistanceBetweenSpawns = entry.Value.ReqDist;
                            ht.disallowSpawningNearEntrances = entry.Value.DisallowNear;
                            mapObjectInfo.InsideInfo.IndoorMapHazardType = ht;
                        }
                        break;
                    }
                }
            }
            originalStates.Clear();
        }
    }
}
