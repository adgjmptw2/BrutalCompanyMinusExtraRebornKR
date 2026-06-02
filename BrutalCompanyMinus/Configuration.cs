using BepInEx;
using BepInEx.Configuration;
using BrutalCompanyMinus.Minus;
using System.Collections.Generic;
using static BrutalCompanyMinus.Minus.MEvent;
using HarmonyLib;
using UnityEngine;
using System.Globalization;
using BrutalCompanyMinus.Minus.CustomEvents;
using BrutalCompanyMinus.Minus.Events;
using static BrutalCompanyMinus.Minus.MonoBehaviours.EnemySpawnCycle;
using static BrutalCompanyMinus.Assets;
using static BrutalCompanyMinus.Helper;
using BrutalCompanyMinus.Minus.MonoBehaviours;
using System;
using EventType = BrutalCompanyMinus.Minus.MEvent.EventType;
using System.ComponentModel;
using Steamworks.ServerList;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BrutalCompanyMinus
{
    [HarmonyPatch]
    public class Configuration
    {
        // Config files
        public static ConfigFile? uiConfig, eventConfig, weatherConfig, customAssetsConfig, difficultyConfig, moddedEventConfig, customEventConfig, allEnemiesConfig, levelPropertiesConfig, CorePropertiesConfig /*, extensiveSettingsConfig*/;

        // Event settings
        public static List<ConfigEntry<int>> eventWeights = new List<ConfigEntry<int>>();
        public static List<List<string>> eventDescriptions = new List<List<string>>();
        public static List<ConfigEntry<string>> eventColorHexes = new List<ConfigEntry<string>>();
        public static List<ConfigEntry<MEvent.EventType>> eventTypes = new List<ConfigEntry<MEvent.EventType>>();
        public static List<Dictionary<ScaleType, Scale>> eventScales = new List<Dictionary<ScaleType, Scale>>();
        
        public static List<ConfigEntry<bool>> showTip = new List<ConfigEntry<bool>>();
        public static List<List<string>> TipsList = new List<List<string>>();
        public static List<List<string>> TipsTitles = new List<List<string>>();
        public static List<ConfigEntry<bool>> TipIsWarning = new List<ConfigEntry<bool>>();

        public static List<ConfigEntry<bool>> eventEnables = new List<ConfigEntry<bool>>();
        public static List<List<string>> eventsToRemove = new List<List<string>>(), eventsToSpawnWith = new List<List<string>>();
        public static List<List<string>> eventAliases = new List<List<string>>();
        public static List<List<string>> moonBlacklist = new List<List<string>>();
        public static List<List<string>> moonWhitelist = new List<List<string>>();
        public static List<ConfigEntry<bool>> moonMode = new List<ConfigEntry<bool>>();
        public static List<ConfigEntry<bool>> eventSpecial = new List<ConfigEntry<bool>>();
        public static List<ConfigEntry<bool>> eventBeta = new List<ConfigEntry<bool>>();
        public static List<List<MonsterEvent>> monsterEvents = new List<List<MonsterEvent>>();
        public static List<ScrapTransmutationEvent> transmutationEvents = new List<ScrapTransmutationEvent>();

        // Difficulty Settings
        public static ConfigEntry<bool>? useCustomWeights, showEventsInChat;
        public static Scale eventsToSpawn;
        public static float[]? weightsForExtraEvents;
        public static Scale[] eventTypeScales = new Scale[8];
        public static Dictionary<EventType, Scale> scrapValueByEventTypeScale = new Dictionary<EventType, Scale>();
        public static Dictionary<EventType, Scale> scrapAmountByEventTypeScale = new Dictionary<EventType, Scale>();
        public static ConfigEntry<string>? MoonsToIgnore;
        public static Scale factorySizeMultiplier = new Scale();

        public static EventManager.DifficultyTransition[]? difficultyTransitions;
        public static ConfigEntry<bool>? enableQuotaChanges;
        public static ConfigEntry<int>? deadLineDaysAmount, startingCredits, startingQuota, baseIncrease, increaseSteepness;
        public static Scale
            spawnChanceMultiplierScaling = new Scale(),
            insideEnemyMaxPowerCountScaling = new Scale(),
            outsideEnemyPowerCountScaling = new Scale(),
            enemyBonusHpScaling = new Scale(),
            spawnCapMultiplier = new Scale(),
            scrapAmountMultiplier = new Scale(),
            scrapValueMultiplier = new Scale(),
            insideSpawnChanceAdditive = new Scale(),
            outsideSpawnChanceAdditive = new Scale();
        public static ConfigEntry<bool>? ignoreMaxCap;
        public static ConfigEntry<float>? difficultyMaxCap;
        public static ConfigEntry<float>? scrapValueMax;
        public static ConfigEntry<float>? scrapAmountMax;
        public static ConfigEntry<float>? FactorySizeMax;
        public static ConfigEntry<float>? FactorySizeMin;
        public static ConfigEntry<bool>? scaleByDaysPassed, scaleByScrapInShip, scaleByMoonGrade, scaleByWeather, scaleByQuota, scaleHeat;
        public static ConfigEntry<float>? daysPassedDifficultyMultiplier, daysPassedDifficultyCap, scrapInShipDifficultyMultiplier, scrapInShipDifficultyCap, quotaDifficultyMultiplier, quotaDifficultyCap;
        public static Dictionary<string, float> gradeAdditives = new Dictionary<string, float>();
        public static Dictionary<LevelWeatherType, float> weatherAdditives = new Dictionary<LevelWeatherType, float>();
        public static ConfigEntry<bool>? enableCustomTimeAdjustments;
        public static Scale timeScaling = new Scale();
        public static Scale startingTime = new Scale();

        public static ConfigEntry<float>? heatIncrementAmount, heatDecrementAmount, heatMaxCap, heatDampening, heatMultiplierDifficulty, heatMultiplierOtherCalculations, startingHeat;
        public static ConfigEntry<bool>? heatForceEventAtMax;
        public static ConfigEntry<string>? heatEventsToForce;
   
        [Flags]
        public enum HeatSettingsFlags
        {
            None = 0,
            Difficulty = 1 << 0,
            InsidePower = 1 << 1,
            OutsidePower = 1 << 2,
            All = ~0
        }

        public static ConfigEntry<HeatSettingsFlags>? heatSettingsToAffect;

        // Player Scaling Settings
        public static ConfigEntry<bool>? enablePlayerScaling;
        public static ConfigEntry<string>? playerScalingType;
        public static ConfigEntry<float>? playerScalingMultiplier;
        public static ConfigEntry<int>? basePlayerAmount;

        // Weather settings
        public static ConfigEntry<bool>? useWeatherMultipliers, randomizeWeatherMultipliers, enableTerminalText;
        public static ConfigEntry<float>? weatherRandomRandomMinInclusive, weatherRandomRandomMaxInclusive;
        public static Weather noneMultiplier, dustCloudMultiplier, rainyMultiplier, stormyMultiplier, foggyMultiplier, floodedMultiplier, eclipsedMultiplier;

        // UI settings
        public static ConfigEntry<string>? UIKey;
        public static ConfigEntry<bool>? NormaliseScrapValueDisplay, EnableUI, ShowUILetterBox, ShowExtraProperties, PopUpUI, DisplayUIAfterShipLeaves, DisplayExtraPropertiesAfterShipLeaves, displayEvents;
        public static ConfigEntry<float>? UITime, scrollSpeed;
        public static ConfigEntry<string>? color;
        public static ConfigEntry<float>? uiColorReduction;
        public static ConfigEntry<float>? colorArrowsIncrease;
        public static ConfigEntry<string>? colorArrows;
        public static ConfigEntry<string>? colorText;
        //public static ConfigEntry<string> menuColor;
        //public static ConfigEntry<float> menuTransparency;

        // Custom assets settings
        public static ConfigEntry<int>? nutSlayerLives, nutSlayerHp;
        public static ConfigEntry<float>? nutSlayerMovementSpeed;
        public static ConfigEntry<bool>? nutSlayerImmortal;
        public static ConfigEntry<bool>? onlyPlayersAttackSlayer;
        public static ConfigEntry<int>? slayerShotgunMinValue, slayerShotgunMaxValue;

        // All enemies settings
        public static ConfigEntry<bool>? enableAllEnemies, enableAllAllEnemies;

        // Level properties settings
        public static Dictionary<int, LevelProperties> levelProperties = new Dictionary<int, LevelProperties>();

        // Other
        public static CultureInfo en = new CultureInfo("en-US"); // This is important, no touchy
        public static string scaleDescription = "형식: 기본값, 증가값, 최솟값, 최댓값   공식: 기본값 + (증가값 * 난이도)   기본적으로 난이도는 특정 요소에 따라 0~100 사이를 오갑니다";

        // Custom Events and Settings
        public static string customEventsFolder = Paths.ConfigPath + "\\BrutalCompanyMinusExtraReborn\\CustomEvents";

        // Core Properties
        public static ConfigEntry<bool>? enableCustomEvents;
        public static ConfigEntry<bool>? ExtraLogging;
        public static ConfigEntry<bool>? SilenceEnum;
        public static ConfigEntry<bool>? SilencePrefab;
        public static ConfigEntry<bool>? GetMethods;
        public static ConfigEntry<bool>? DisableAllEvents;
        public static ConfigEntry<bool>? dontHandlePower;
        public static ConfigEntry<bool>? dontHandleSpawnCurves;
        public static ConfigEntry<bool>? AffectPropertiesOutOfEvents;
        public static ConfigEntry<bool>? deferWeatherToMods;
        public static ConfigEntry<bool>? enforceEscapeModChecks;
        public static ConfigEntry<bool>? enableSpecialEvents;
        public static ConfigEntry<bool>? enableBetaEvents;
        public static Scale timeChaosScale = new Scale();
        public static ConfigEntry<string>? transmutationBlacklist;
        public static ConfigEntry<bool>? handleScanCommand;
        public static ConfigEntry<bool>? speedrunMode;
        public static Scale EventChanceGlobal = new Scale();
        public static ConfigEntry<float>? timeBetweenTips, InitTimePopUp;


        /*   public static ConfigEntry<bool> EnableStreamerEvents;*/

        private static void Initalize()
        {
            // Difficulty Settings
            useCustomWeights = difficultyConfig.Bind("_Event Settings", "Use custom weights?", false, "'false'= 이벤트 유형 가중치로 모든 가중치 설정     'true'= 직접 설정한 가중치 사용");
            eventsToSpawn = getScale(difficultyConfig.Bind("_Event Settings", "Event scale amount", "2, 0.03, 2.0, 5.0", "이벤트의 기본 발생 수   형식: 기본값, 증가값, 최솟값, 최댓값   " + scaleDescription).Value);
            weightsForExtraEvents = ParseValuesFromString(difficultyConfig.Bind("_Event Settings", "Weights for bonus events", "40, 39, 15, 5, 1", "추가 이벤트 가중치, 확장 가능. (40, 39, 15, 5, 1)은 (+0, +1, +2, +3, +4) 이벤트에 해당").Value);

            eventTypeScales = new Scale[8]
            {
                getScale(difficultyConfig.Bind("_EventType Weights", "Insane event scale", "3, 0.05, 3, 20", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "VeryBad event scale", "5, 0.25, 5, 30", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "Bad event scale", "40, -0.15, 25, 40", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "Neutral event scale", "10, -0.05, 5, 10", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "Good event scale", "23, -0.1, 13, 23", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "VeryGood event scale", "3, 0.14, 3, 17", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "Rare event scale", "2, 0.02, 2, 10", scaleDescription).Value),
                getScale(difficultyConfig.Bind("_EventType Weights", "Remove event scale", "15, -0.05, 10, 15", "이 이벤트들은 무언가를 제거합니다   " + scaleDescription).Value)
            };

            difficultyTransitions = GetDifficultyTransitionsFromString(difficultyConfig.Bind("Difficulty Scaling", "Difficulty Transitions", "쉬움,00FF00,0|보통,008000,15|어려움,FF0000,30|매우어려움,800000,50|절망,140000,75", "형식: 이름,16진수색상,기준값, 기준값은 해당 이름이 표시되는 난이도 수치입니다.").Value);
            ignoreMaxCap = difficultyConfig.Bind("Difficulty Scaling", "Ignore max cap?", false, "true로 설정 시 최대 상한을 무시합니다. 상한은 '난이도 최대 상한' 설정에 의해서도 결정됩니다.");
            difficultyMaxCap = difficultyConfig.Bind("Difficulty Scaling", "Difficulty max cap", 100.0f, "난이도 수치가 이 값을 초과하지 않습니다.");
            scaleByDaysPassed = difficultyConfig.Bind("Difficulty Scaling", "Scale by days passed?", true, "경과 일수에 따라 난이도가 증가합니다.");
            daysPassedDifficultyMultiplier = difficultyConfig.Bind("Difficulty Scaling", "Difficulty per days passed?", 1.0f, "경과한 일수 1일당 추가되는 난이도입니다.");
            daysPassedDifficultyCap = difficultyConfig.Bind("Difficulty Scaling", "Days passed difficulty cap", 60.0f, "경과 일수로 인한 난이도 증가가 이 값을 초과하지 않습니다.");
            scaleByScrapInShip = difficultyConfig.Bind("Difficulty Scaling", "Scale by scrap in ship?", true, "함선 내 스크랩 양에 따라 난이도가 증가합니다.");
            scrapInShipDifficultyMultiplier = difficultyConfig.Bind("Difficulty Scaling", "Difficulty per scrap value in ship?", 0.0025f, "기본값: 함선 내 스크랩 400당 +1.0");
            scrapInShipDifficultyCap = difficultyConfig.Bind("Difficulty Scaling", "Scrap in ship difficulty cap", 30.0f, "함선 내 스크랩으로 인한 난이도 증가가 이 값을 초과하지 않습니다.");
            scaleByQuota = difficultyConfig.Bind("Difficulty Scaling", "Scale by quota?", false, "할당량 수치에 따라 난이도가 증가합니다.");
            quotaDifficultyMultiplier = difficultyConfig.Bind("Difficulty Scaling", "Difficulty per quota value?", 0.005f, "기본값: 할당량 200당 +1.0");
            quotaDifficultyCap = difficultyConfig.Bind("Difficulty Scaling", "Quota difficulty cap", 100.0f, "할당량으로 인한 난이도 증가가 이 값을 초과하지 않습니다");
            scaleByMoonGrade = difficultyConfig.Bind("Difficulty Scaling", "Scale by moon grade?", true, "착륙한 달의 등급에 따라 난이도가 증가합니다.");
            gradeAdditives = GetMoonRiskFromString(difficultyConfig.Bind("Difficulty Scaling", "Grade difficulty scaling", "D,-8|C,-8|B,-4|A,5|S,10|S+,15|S++,20|S+++,30|Other,10", "형식: 등급,난이도, 'Other'는 삭제하지 마세요").Value);
            scaleByWeather = difficultyConfig.Bind("Difficulty Scaling", "Scale by weather type?", false, "착륙한 달의 날씨에 따라 난이도가 증가합니다.");
            scaleHeat = difficultyConfig.Bind("Difficulty Scaling", "Scale by Heat?", false, "행성의 현재 열기에 따라 난이도를 조정합니다. 열기 값은 0 미만으로 내려가지 않습니다.");
            heatIncrementAmount = difficultyConfig.Bind("Difficulty Scaling", "Heat difficulty additive", 1f, "달을 방문한 뒤, 같은 달을 다시 갈 때 이만큼 열기가 쌓입니다.");
            heatDecrementAmount = difficultyConfig.Bind("Difficulty Scaling", "Heat difficulty decrement", 1f, "전날과 다른 달을 방문하면 열기가 이만큼 줄어듭니다.");
            heatDampening = difficultyConfig.Bind("Difficulty Scaling", "Heat dampening factor", 0.15f, "열기 보정 계수입니다. 0에 가까울수록 난이도가 더 크게 오르고, 값이 클수록 원래 수치에 가깝게 유지됩니다.");
            heatMultiplierDifficulty = difficultyConfig.Bind("Difficulty Scaling", "Heat multiplier (Difficulty)", 0.0015f, "난이도 계산에 쓰입니다. 값이 클수록 열기가 난이도에 더 빠르게 반영됩니다.");
            heatMultiplierOtherCalculations = difficultyConfig.Bind("Difficulty Scaling", "Heat multiplier (Non-Difficulty)", 0.004f, "난이도 외(예: 파워 값) 계산에 쓰입니다. 값이 클수록 더 빠르게 반영됩니다.");
            startingHeat = difficultyConfig.Bind("Difficulty Scaling", "Starting Heat", 0f, "각 행성을 처음 방문할 때의 시작 열기입니다. 기본값은 0입니다.");
            heatMaxCap = difficultyConfig.Bind("Difficulty Scaling", "Heat Max Cap", 10f, "열기가 이 값을 넘지 않습니다.");
            heatForceEventAtMax = difficultyConfig.Bind("Difficulty Scaling", "Force event at max heat?", false, "true이면 열기가 최대일 때 아래 목록의 이벤트를 강제로 발생시킵니다.");
            heatEventsToForce = difficultyConfig.Bind("Difficulty Scaling", "Events to force at max heat", "", "열기가 최대일 때 강제 발생할 이벤트 이름입니다. 쉼표로 구분하며, 등록된 이름과 대소문자가 정확히 같아야 합니다. 앞뒤 공백은 자동으로 제거됩니다.");
            heatSettingsToAffect = difficultyConfig.Bind("Difficulty Scaling", "Heat Affects What Properties", HeatSettingsFlags.Difficulty, "열기가 영향을 줄 항목을 선택합니다.");

            weatherAdditives = new Dictionary<LevelWeatherType, float>()
            {
                { LevelWeatherType.None, difficultyConfig.Bind("Difficulty Scaling", "None weather difficulty", 0.0f, "날씨 없음 상태에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.Rainy, difficultyConfig.Bind("Difficulty Scaling", "Rainy weather difficulty", 2.0f, "비 날씨에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.DustClouds, difficultyConfig.Bind("Difficulty Scaling", "DustClouds weather difficulty", 2.0f, "먼지 구름 날씨에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.Flooded, difficultyConfig.Bind("Difficulty Scaling", "Flooded weather difficulty", 4.0f, "홍수 날씨에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.Foggy, difficultyConfig.Bind("Difficulty Scaling", "Foggy weather difficulty", 4.0f, "안개 날씨에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.Stormy, difficultyConfig.Bind("Difficulty Scaling", "Stormy weather difficulty", 7.0f, "폭풍 날씨에서 플레이 시 추가 난이도").Value },
                { LevelWeatherType.Eclipsed, difficultyConfig.Bind("Difficulty Scaling", "Eclipsed weather difficulty", 7.0f, "일식 날씨에서 플레이 시 추가 난이도").Value },
            };
            scrapValueMax = difficultyConfig.Bind("Difficulty Scaling", "Scrap value max cap", 2147483647.0f, "스크랩 가치 배율의 합산이 이 값을 초과하지 않습니다.");
            scrapAmountMax = difficultyConfig.Bind("Difficulty Scaling", "Scrap amount max cap", 2147483647.0f, "스크랩 수량 배율의 합산이 이 값을 초과하지 않습니다.");
            FactorySizeMax = difficultyConfig.Bind("Difficulty Scaling", "Factory size max cap", 2147483647.0f, "공장 크기 배율의 합산이 이 값을 초과하지 않습니다. 사용에 주의하세요");
            FactorySizeMin = difficultyConfig.Bind("Difficulty Scaling", "Factory size min cap", 0.01f, "공장 크기 배율의 합산이 이 값 미만으로 내려가지 않습니다. 사용에 주의하세요.");
            enableCustomTimeAdjustments = difficultyConfig.Bind("Difficulty Scaling", "Enable Time Adjustments?", false, "난이도 스케일링에 커스텀 시간 조정을 전역으로 활성화합니다.");
            timeScaling = getScale(difficultyConfig.Bind("Difficulty Scaling", "Time Scaling", "1, 0.0, 1, 1", "난이도 스케일링에 따른 시간 배율" + scaleDescription).Value);
            startingTime = getScale(difficultyConfig.Bind("Difficulty Scaling", "Starting Time", "100, 0.0, 100, 100", "난이도에 따른 시작 시간" + scaleDescription).Value);

            spawnChanceMultiplierScaling = getScale(difficultyConfig.Bind("Difficulty", "Spawn chance multiplier scale", "1.0, 0.017, 1.0, 2.0", "이 값으로 스폰 확률을 곱합니다,   " + scaleDescription).Value);
            insideSpawnChanceAdditive = getScale(difficultyConfig.Bind("Difficulty", "Inside spawn chance additive", "0.0, 0.0, 0.0, 0.0", "애니메이션 커브의 내부 스폰 키프레임에 이 값을 더합니다,   " + scaleDescription).Value);
            outsideSpawnChanceAdditive = getScale(difficultyConfig.Bind("Difficulty", "Outside spawn chance additive", "0.0, 0.0, 0.0, 0.0", "애니메이션 커브의 외부 스폰 키프레임에 이 값을 더합니다,   " + scaleDescription).Value);
            spawnCapMultiplier = getScale(difficultyConfig.Bind("Difficulty", "Spawn cap multipler scale", "1.0, 0.017, 1.0, 2.0", "내외부 파워 카운트를 이 값으로 곱합니다,   " + scaleDescription).Value);
            insideEnemyMaxPowerCountScaling = getScale(difficultyConfig.Bind("Difficulty", "Additional Inside Max Enemy Power Count", "0, 0, 0, 0", "내부 적의 최대 파워 카운트에 추가,   " + scaleDescription).Value);
            outsideEnemyPowerCountScaling = getScale(difficultyConfig.Bind("Difficulty", "Additional Outside Max Enemy Power Count", "0, 0, 0, 0", "외부 적의 최대 파워 카운트에 추가,   " + scaleDescription).Value);
            enemyBonusHpScaling = getScale(difficultyConfig.Bind("Difficulty", "Additional hp scale", "0, 0, 0, 0", "모든 적의 체력에 추가,   " + scaleDescription).Value);
            scrapValueMultiplier = getScale(difficultyConfig.Bind("Difficulty", "Scrap value multiplier scale", "1.0, 0.003, 1.0, 1.3", "전역 스크랩 가치 배율,   " + scaleDescription).Value);
            scrapAmountMultiplier = getScale(difficultyConfig.Bind("Difficulty", "Scrap amount multiplier scale", "1.0, 0.003, 1.0, 1.3", "전역 스크랩 수량 배율,   " + scaleDescription).Value);
            factorySizeMultiplier = getScale(difficultyConfig.Bind("Difficulty", "Factory Size multiplier scale", "1.0, 0, 1.0, 1.0", "공장 크기 배율. 사용에 주의하세요. 로딩이 안 되거나 생성에 매우 오랜 시간이 걸릴 수 있습니다." + scaleDescription).Value);

            enablePlayerScaling = difficultyConfig.Bind("Player Scaling", "Enable player scaling?", false, "플레이어 스케일링 활성화");
            playerScalingType = difficultyConfig.Bind("Player Scaling", "Player scaling type", "Linear", "플레이어 수에 대한 스케일링 유형. 옵션: 선형(Linear), 지수(Exponential), 로그(Logarithmic), 3차(Cubic)");
            playerScalingMultiplier = difficultyConfig.Bind("Player Scaling", "Player scaling multiplier", 1.0f, "플레이어 스케일링 배율");
            basePlayerAmount = difficultyConfig.Bind("Player Scaling", "기본 플레이어 수", 4, "기본 플레이어 수");

            Scale bindEventTypeScrapAmountMultiplier(EventType difficulty)
                => getScale(difficultyConfig.Bind("_EventType Scrap Multipliers", difficulty + " scrap amount scale", "1, 0.0, 1, 1", scaleDescription).Value);
            Scale bindEventTypeScrapValueMultiplier(EventType difficulty)
                => getScale(difficultyConfig.Bind("_EventType Scrap Multipliers", difficulty + " scrap value scale", "1, 0.0, 1, 1", scaleDescription).Value);
            foreach (EventType difficulty in (EventType[])Enum.GetValues(typeof(EventType)))
            {
                scrapAmountByEventTypeScale.Add(difficulty, bindEventTypeScrapAmountMultiplier(difficulty));
                scrapValueByEventTypeScale.Add(difficulty, bindEventTypeScrapValueMultiplier(difficulty));
            }

            MoonsToIgnore = difficultyConfig.Bind("Moons Settings", "Moons to Not Spawn Events On", "", "이 달에서는 이벤트가 발생하지 않습니다. 달 이름은 쉼표로 구분하세요.");


            // Custom scrap settings
            nutSlayerLives = customAssetsConfig.Bind("NutSlayer", "Lives", 5, "체력이 0 이하가 되면 목숨이 1 줄고 체력이 초기화됩니다. 목숨이 0이 되면 사망합니다.");
            nutSlayerHp = customAssetsConfig.Bind("NutSlayer", "Hp", 6);
            nutSlayerMovementSpeed = customAssetsConfig.Bind("NutSlayer", "Speed?", 9.5f);
            nutSlayerImmortal = customAssetsConfig.Bind("NutSlayer", "Immortal?", true);
            onlyPlayersAttackSlayer = customAssetsConfig.Bind("NutSlayer", "Only players can attack NutSlayer?", false, "넛슬레이어가 무적이면 효과가 없습니다. true면 플레이어만 공격할 수 있습니다.");
            grabbableTurret.minValue = customAssetsConfig.Bind("Grabbable Landmine", "Min value", 50).Value;
            grabbableTurret.maxValue = customAssetsConfig.Bind("Grabbable Landmine", "Max value", 75).Value;
            grabbableLandmine.minValue = customAssetsConfig.Bind("Grabbable Turret", "Min value", 100).Value;
            grabbableLandmine.maxValue = customAssetsConfig.Bind("Grabbable Turret", "Max value", 150).Value;
            slayerShotgunMinValue = customAssetsConfig.Bind("Slayer Shotgun", "Min value", 200);
            slayerShotgunMaxValue = customAssetsConfig.Bind("Slayer Shotgun", "Max value", 300);

            // Weather settings
            useWeatherMultipliers = weatherConfig.Bind("_Weather Settings", "Enable weather multipliers?", true, "'false'= 모든 날씨 배율 비활성화     'true'= 날씨 배율 활성화");
            randomizeWeatherMultipliers = weatherConfig.Bind("_Weather Settings", "Weather multiplier randomness?", false, "'false'= 비활성화     'true'= 활성화");
            enableTerminalText = weatherConfig.Bind("_Weather Settings", "Enable terminal text?", true, "터미널에 날씨 관련 문구를 표시할지 여부입니다.");

            weatherRandomRandomMinInclusive = weatherConfig.Bind("_Weather Random Multipliers", "Min Inclusive", 0.9f, "무작위 배율의 최솟값(이상)입니다.");
            weatherRandomRandomMaxInclusive = weatherConfig.Bind("_Weather Random Multipliers", "Max Inclusive", 1.2f, "무작위 배율의 최댓값(이하)입니다.");

            Weather createWeatherSettings(Weather weather)
            {
                string configHeader = "(" + weather.weatherType.ToString() + ") Weather multipliers";

                float valueMultiplierSetting = weatherConfig.Bind(configHeader, "Scrap Value Multiplier", weather.scrapValueMultiplier, weather.weatherType.ToString() + " 날씨의 스크랩 가치 배율입니다.").Value;
                float amountMultiplierSetting = weatherConfig.Bind(configHeader, "Scrap Amount Multiplier", weather.scrapAmountMultiplier, weather.weatherType.ToString() + " 날씨의 스크랩 수량 배율입니다.").Value;

                return new Weather(weather.weatherType, valueMultiplierSetting, amountMultiplierSetting);
            }

            noneMultiplier = createWeatherSettings(new Weather(LevelWeatherType.None, 1.00f, 1.00f));
            dustCloudMultiplier = createWeatherSettings(new Weather(LevelWeatherType.DustClouds, 1.05f, 1.00f));
            rainyMultiplier = createWeatherSettings(new Weather(LevelWeatherType.Rainy, 1.05f, 1.00f));
            stormyMultiplier = createWeatherSettings(new Weather(LevelWeatherType.Stormy, 1.35f, 1.20f));
            foggyMultiplier = createWeatherSettings(new Weather(LevelWeatherType.Foggy, 1.15f, 1.10f));
            floodedMultiplier = createWeatherSettings(new Weather(LevelWeatherType.Flooded, 1.25f, 1.15f));
            eclipsedMultiplier = createWeatherSettings(new Weather(LevelWeatherType.Eclipsed, 1.35f, 1.20f));

            // UI Settings
            UIKey = uiConfig.Bind("UI Options", "Toggle UI Key", "K");
            color = uiConfig.Bind("UI Options", "UI Color Hex", "00A000", "UI 요소 색상(16진수)입니다.");
            uiColorReduction = uiConfig.Bind("UI Options", "UI Color Reduction Factor", 0.6275f, "UI가 비활성일 때 색을 어둡게 하는 비율입니다(0~1).");
            colorArrows = uiConfig.Bind("UI Options", "UI Arrow Color Hex", "00A000", "UI 화살표 색상(16진수)입니다.");
            colorArrowsIncrease = uiConfig.Bind("UI Options", "UI Arrow Color Amplification", 255f / 160f, "화살표가 활성일 때 색에 곱하는 배율입니다.");
            colorText = uiConfig.Bind("UI Options", "UI Text Color Hex", "00FF00", "UI 텍스트 색상(16진수)입니다.");
            //menuColor = uiConfig.Bind("UI Options", "UI Menu Color Hex", "000000", "Color hex for UI menu background.");
            //menuTransparency = uiConfig.Bind("UI Options", "UI Menu Transparency", 0.498f, "Transparency for UI menu background. (0-1)");

            NormaliseScrapValueDisplay = uiConfig.Bind("UI Options", "Normalize scrap value display number?", true, "게임 기본 표시값(0.4)을 2.5배해 UI에 익숙한 숫자로 보이게 합니다.");
            EnableUI = uiConfig.Bind("UI Options", "Enable UI?", true, "이벤트 UI를 사용할지 여부입니다.");
            ShowUILetterBox = uiConfig.Bind("UI Options", "Display UI Letter Box?", true, "UI 글자 상자를 표시할지 여부입니다.");
            ShowExtraProperties = uiConfig.Bind("UI Options", "Display extra properties", true, "스크랩 가치·수량 배율 등 추가 정보를 UI에 표시합니다.");
            PopUpUI = uiConfig.Bind("UI Options", "PopUp UI?", true, "하루를 시작할 때 UI가 자동으로 뜨게 할지 여부입니다.");
            UITime = uiConfig.Bind("UI Options", "PopUp UI time.", 45.0f, "UI가 자동으로 닫히기까지의 시간(초)입니다.");
            scrollSpeed = uiConfig.Bind("UI Options", "Scroll speed", 1.0f, "화살표로 스크롤할 때의 속도 배율입니다.");
            DisplayUIAfterShipLeaves = uiConfig.Bind("UI Options", "Display UI after ship leaves?", false, "true이면 함선이 떠난 뒤에만 이벤트를 표시합니다.");
            DisplayExtraPropertiesAfterShipLeaves = uiConfig.Bind("UI Options", "Display extra properties on UI after ship leaves?", true, "함선 출발 후 다음 날 이벤트 유형 확률과 난이도 정보를 표시합니다.");
            displayEvents = uiConfig.Bind("UI Options", "Display events?", true, "false이면 UI에 이벤트 목록을 표시하지 않습니다.");
            showEventsInChat = uiConfig.Bind("UI Options", "Will Minus display events in chat?", false, "true이면 채팅에도 이벤트 목록을 표시합니다.");

            //Core Properties
            enableCustomEvents = CorePropertiesConfig.Bind("Custom Events", "Enable Custom Events?", true, "커스텀 이벤트 폴더에서 이벤트를 불러옵니다.");
            ExtraLogging = CorePropertiesConfig.Bind("Debugging", "Enable Extra Logging?", false, "디버깅용 추가 로그를 출력합니다.");
            SilenceEnum = CorePropertiesConfig.Bind("Debugging", "Silence Enum Warnings?", true, "열거형(enum) 불일치 경고를 숨깁니다.");
            SilencePrefab = CorePropertiesConfig.Bind("Debugging", "Silence Prefab Warnings?", true, "프리팹 누락 경고를 숨깁니다.");
            GetMethods = CorePropertiesConfig.Bind("Debugging", "Silence Get Method Warnings?", true, "설치되지 않은 모드의 적·아이템 관련 경고를 숨깁니다.");
            DisableAllEvents = CorePropertiesConfig.Bind("Events Features", "Disable all events?", false, "true이면 모든 이벤트 발생을 끕니다. 이벤트 수가 0이 됩니다.");
            dontHandlePower = CorePropertiesConfig.Bind("Mod Compatibility", "Experimental Dont Handle Power?", false, "다른 모드가 파워 레벨을 처리하게 하려면 켜세요. 일부 설정은 여전히 영향을 줄 수 있습니다.");
            dontHandleSpawnCurves = CorePropertiesConfig.Bind("Mod Compatibility", "Experimental Dont Handle Spawn Chance?", false, "다른 모드가 스폰 확률 곡선을 처리하게 하려면 켜세요. 일부 설정은 여전히 영향을 줄 수 있습니다.");
            AffectPropertiesOutOfEvents = CorePropertiesConfig.Bind("Mod Compatibility", "Let Brutal handle properties outside of events?", true, "false이면 이벤트 밖의 속성은 Brutal이 건드리지 않습니다.");
            deferWeatherToMods = CorePropertiesConfig.Bind("Mod Compatibility", "Defer Weather To Weather ToolKit Mod?", false, "다른 모드가 Brutal 날씨 효과를 처리하게 하려면 켜세요. 커스텀 이벤트에는 기본적으로 Weather Toolkit을 씁니다.");
            enforceEscapeModChecks = CorePropertiesConfig.Bind("Mod Compatibility", "Enforce Escape Mod Checks?", true, "탈출 AI 모드가 없을 때, AI 오작동을 막기 위해 일부 이벤트 스폰을 조정합니다. 안전 검사 없이 쓰려면 끄세요.");
            enableSpecialEvents = CorePropertiesConfig.Bind("Events Features", "Enable Special Events?", false, "스페셜 이벤트를 불러옵니다.");
            enableBetaEvents = CorePropertiesConfig.Bind("Events Features", "Enable Beta Events?", false, "베타 이벤트를 불러옵니다. 미검증이라 버그가 많을 수 있습니다.");
            transmutationBlacklist = CorePropertiesConfig.Bind("Events Features", "Transmutation Blacklist", "", "스크랩 변환에 쓰이지 않게 막을 아이템 목록입니다. itemProperties.itemName(컴포넌트 이름)을 사용합니다.");
            handleScanCommand = CorePropertiesConfig.Bind("Mod Compatibility", "Let Brutal handle the SCAN command?", true, "true이면 Brutal이 SCAN 명령과 스크랩 가치 표시를 처리합니다. 다른 모드가 담당하면 끄세요. 끄면 수정된 가치가 SCAN에 반영되지 않을 수 있습니다.");
            speedrunMode = CorePropertiesConfig.Bind("Events Features", "Enable Speedrun Mode?", false, "true이면 스피드런에 맞게 일부 이벤트·기능을 조정합니다. 스피드런이 아니면 끄는 것을 권장합니다.");
            EventChanceGlobal = getScale(CorePropertiesConfig.Bind("Events Features", "Chance of Events Occurring", "100, 0.0, 100, 100", "하루에 이벤트가 발생할 확률입니다. " + scaleDescription).Value);
            InitTimePopUp = CorePropertiesConfig.Bind("Events Features", "Initial Time PopUp", 5.0f, "첫 팁이 뜨기까지의 대기 시간(초)입니다.");
            timeBetweenTips = CorePropertiesConfig.Bind("Events Features", "Time between tips", 5.0f, "팁 메시지 사이 간격(초)입니다.");

            //Custom Events Folder
            try
            {
                if (!System.IO.Directory.Exists(customEventsFolder))
                {
                    System.IO.Directory.CreateDirectory(customEventsFolder);
                }
            }
            catch (Exception e)
            {
                Log.LogWarning("Failed to create custom events folder: " + e.Message);
            }

            //Leftover from "Streamer Events" feature, non-functional
            /*   EnableStreamerEvents = extensiveSettingsConfig.Bind("Extra Options", "Enable Streamer events?", true, "Enables streamer specific events");*/


            // Event settings
            void RegisterEvents(ConfigFile toConfig, List<MEvent> events)
            {

                // Event settings
                foreach (MEvent e in events)
                {
                    eventWeights.Add(toConfig.Bind(e.Name(), "Custom Weight", e.Weight, "직접 가중치를 쓰려면 '_Event Settings'의 'Use custom weights?'를 true로 설정하세요."));
                    eventDescriptions.Add(ListToDescriptions(toConfig.Bind(e.Name(), "Descriptions", StringsToList(e.Descriptions, "|"), "| 로 구분합니다.").Value));
                    eventColorHexes.Add(toConfig.Bind(e.Name(), "Color Hex", e.ColorHex));
                    eventTypes.Add(toConfig.Bind(e.Name(), "Event Type", e.Type));

                    showTip.Add(toConfig.Bind(e.Name(), "Show Tip?", e.showTip, "true이면 팁을 표시합니다."));
                    TipsTitles.Add(ListToDescriptions(toConfig.Bind(e.Name(), "Tip Titles", StringsToList(e.TipTitle, "|"), "| 로 구분합니다.").Value));
                    TipsList.Add(ListToDescriptions(toConfig.Bind(e.Name(), "Tip Messages", StringsToList(e.TipMessages, "|"), "| 로 구분합니다.").Value));
                    TipIsWarning.Add(toConfig.Bind(e.Name(), "Is Tip A Warning?", e.isWarning, "true이면 빨간 경고로, false이면 노란 안내로 표시합니다."));
                    eventEnables.Add(toConfig.Bind(e.Name(), "Event Enabled?", e.Enabled, "false이면 이 이벤트는 발생하지 않습니다.")); // Normal event

                    // Make scale list
                    Dictionary<ScaleType, Scale> scales = new Dictionary<ScaleType, Scale>();
                    foreach (KeyValuePair<ScaleType, Scale> obj in e.ScaleList)
                    {
                        scales.Add(obj.Key, getScale(toConfig.Bind(e.Name(), obj.Key.ToString(), GetStringFromScale(obj.Value), ScaleInfoList[obj.Key] + "   " + scaleDescription).Value));
                    }
                    eventScales.Add(scales);

                    // EventsToRemove and EventsToSpawnWith and Moon Blacklist
                    eventsToRemove.Add(ListToStrings(toConfig.Bind(e.Name(), "Events To Remove", StringsToList(e.EventsToRemove, ", "), "목록의 이벤트가 발생하지 않도록 막습니다.").Value));
                    eventsToSpawnWith.Add(ListToStrings(toConfig.Bind(e.Name(), "Events To Spawn With", StringsToList(e.EventsToSpawnWith, ", "), "목록의 이벤트를 함께 발생시킵니다.").Value));
                    eventAliases.Add(ListToStrings(toConfig.Bind(e.Name(), "Event Aliases", StringsToList(e.Aliases, ", "), "쉼표로 별칭을 구분합니다. 이벤트 강제 발생 시 쓸 수 있으며 대소문자를 구분하지 않습니다. 여러 이벤트에 같은 별칭을 쓰지 마세요.").Value));
                    moonMode.Add(toConfig.Bind(e.Name(), "Whitelist Mode?", e.MoonMode, "어떤 달 목록을 쓸지 선택합니다. 화이트리스트 모드는 항목이 하나 이상 있어야 합니다."));
                    eventSpecial.Add(toConfig.Bind(e.Name(), "Special Event?", e.isSpecialEvent, "스페셜 이벤트는 'Enable Special Events'가 켜져 있어야 합니다."));
                    eventBeta.Add(toConfig.Bind(e.Name(), "Beta Event?", e.isBetaEvent, "베타 이벤트는 'Enable Beta Events'가 켜져 있어야 합니다."));

                    moonBlacklist.Add(ListToStrings(toConfig.Bind(e.Name(), "Moons To Not Spawn On", StringsToList(e.Blacklist, ", "), "이 달에서는 이벤트가 발생하지 않습니다. 달 이름은 쉼표로 구분하고 목록을 채워야 합니다.").Value));
                    moonWhitelist.Add(ListToStrings(toConfig.Bind(e.Name(), "Moons To Spawn Only On", StringsToList(e.Whitelist, ", "), "이 달에서만 이벤트가 발생합니다. 달 이름은 쉼표로 구분하고 목록을 채워야 합니다.").Value));

                    // Monster events
                    List<MonsterEvent> newMonsterEvents = new List<MonsterEvent>();
                    for (int i = 0; i < e.monstersToSpawn.Count; i++)
                    {
                        newMonsterEvents.Add(new MonsterEvent(
                            toConfig.Bind(e.Name(), $"Enemy {i} Name", e.monstersToSpawn[i].enemy.name, "잘못된 적 이름이면 빈 enemyType이 반환됩니다.").Value,
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.InsideEnemyRarity}", GetStringFromScale(e.monstersToSpawn[i].insideSpawnRarity), $"{ScaleInfoList[ScaleType.InsideEnemyRarity]}   {scaleDescription}").Value),
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.OutsideEnemyRarity}", GetStringFromScale(e.monstersToSpawn[i].outsideSpawnRarity), $"{ScaleInfoList[ScaleType.OutsideEnemyRarity]}   {scaleDescription}").Value),
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.MinInsideEnemy}", GetStringFromScale(e.monstersToSpawn[i].minInside), $"{ScaleInfoList[ScaleType.MinInsideEnemy]}   {scaleDescription}").Value),
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.MaxInsideEnemy}", GetStringFromScale(e.monstersToSpawn[i].maxInside), $"{ScaleInfoList[ScaleType.MaxInsideEnemy]}   {scaleDescription}").Value),
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.MinOutsideEnemy}", GetStringFromScale(e.monstersToSpawn[i].minOutside), $"{ScaleInfoList[ScaleType.MinOutsideEnemy]}   {scaleDescription}").Value),
                            getScale(toConfig.Bind(e.Name(), $"{e.monstersToSpawn[i].enemy.name} {ScaleType.MaxOutsideEnemy}", GetStringFromScale(e.monstersToSpawn[i].maxOutside), $"{ScaleInfoList[ScaleType.MaxOutsideEnemy]}   {scaleDescription}").Value)
                        ));
                    }
                    monsterEvents.Add(newMonsterEvents);

                    // Scrap transmutation events
                    Scale amount = new Scale(0.0f, 0.0f, 0.0f, 0.0f);
                    if (e.scrapTransmutationEvent.items.Length > 0) amount = getScale(toConfig.Bind(e.Name(), "Percentage", GetStringFromScale(e.scrapTransmutationEvent.amount), $"{ScaleInfoList[ScaleType.Percentage]}   {scaleDescription}").Value);
                    SpawnableItemWithRarity[] newScrapTransmuations = new SpawnableItemWithRarity[e.scrapTransmutationEvent.items.Length];
                    for (int i = 0; i < e.scrapTransmutationEvent.items.Length; i++)
                    {
                        newScrapTransmuations[i] = new SpawnableItemWithRarity(
                            GetItem(toConfig.Bind(e.Name(), $"Scrap {i} name", e.scrapTransmutationEvent.items[i].spawnableItem.name, "잘못된 스크랩 이름이면 빈 아이템이 반환됩니다.").Value),
                            toConfig.Bind(e.Name(), $"{e.scrapTransmutationEvent.items[i].spawnableItem.name} Rarity", e.scrapTransmutationEvent.items[i].rarity).Value
                        );
                    }
                    transmutationEvents.Add(new ScrapTransmutationEvent(amount, newScrapTransmuations));
                }

            }

            RegisterEvents(eventConfig, EventManager.vanillaEvents);
            RegisterEvents(moddedEventConfig, EventManager.moddedEvents);

            if (enableCustomEvents.Value)
            {
                foreach (string eventFile in System.IO.Directory.GetFiles(customEventsFolder))
                {
                    if (eventFile.EndsWith(".json"))
                    {
                        MEvent cEvent = new GeneralCustomEvent(eventFile);
                        cEvent.Initalize();

                        if (cEvent.Enabled) EventManager.customEvents.Add(cEvent);
                    }
                }
                RegisterEvents(customEventConfig, EventManager.customEvents);
            }

            /*foreach (EventManager.CustomEvents customevent in EventManager.customEventsList)
            {
                foreach (MEvent e in customevent.events)
                {
                    e.Initalize();
                }

                RegisterEvents(customevent.configFile, customevent.events);
                EventManager.customEvents.AddRange(customevent.events);
            }
            EventManager.customEventsList.Clear();*/

            EventManager.events.AddRange(EventManager.vanillaEvents);
            EventManager.events.AddRange(EventManager.moddedEvents);
            EventManager.events.AddRange(EventManager.customEvents);
            //EventManager.events.AddRange(EventManager.ExternalEvents);

            // Specific event settings
            Minus.Handlers.FacilityGhost.actionTimeCooldown = eventConfig.Bind(nameof(FacilityGhost), "Normal Action Time Interval", 15.0f, "유령이 행동을 결정하는 간격(초)입니다.").Value;
            Minus.Handlers.FacilityGhost.ghostCrazyActionInterval = eventConfig.Bind(nameof(FacilityGhost), "Crazy Action Time Interval", 0.1f, "유령이 광란 상태일 때 행동을 결정하는 간격(초)입니다.").Value;
            Minus.Handlers.FacilityGhost.ghostCrazyPeriod = eventConfig.Bind(nameof(FacilityGhost), "Crazy Period", 5.0f, "유령이 광란 상태로 유지되는 시간(초)입니다.").Value;
            Minus.Handlers.FacilityGhost.crazyGhostChance = eventConfig.Bind(nameof(FacilityGhost), "Crazy Chance", 0.1f, "행동 결정 시 광란 상태가 될 확률입니다.").Value;
            Minus.Handlers.FacilityGhost.DoNothingWeight = eventConfig.Bind(nameof(FacilityGhost), "Do Nothing Weight?", 25, "행동 결정 시 아무것도 하지 않을 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.OpenCloseBigDoorsWeight = eventConfig.Bind(nameof(FacilityGhost), "Open and close big doors weight", 20, "행동 결정 시 대형 문을 열고 닫을 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.MessWithLightsWeight = eventConfig.Bind(nameof(FacilityGhost), "Mess with lights weight", 16, "행동 결정 시 조명을 건드릴 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.MessWithBreakerWeight = eventConfig.Bind(nameof(FacilityGhost), "Mess with breaker weight", 4, "행동 결정 시 차단기를 건드릴 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.disableTurretsWeight = eventConfig.Bind(nameof(FacilityGhost), "Disable turrets weight?", 5, "행동 결정 시 포탑을 끌 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.disableLandminesWeight = eventConfig.Bind(nameof(FacilityGhost), "Disable landmines weight?", 5, "행동 결정 시 지뢰를 끌 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.turretRageWeight = eventConfig.Bind(nameof(FacilityGhost), "Turret rage weight?", 5, "행동 결정 시 포탑을 폭주시킬 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.OpenCloseDoorsWeight = eventConfig.Bind(nameof(FacilityGhost), "Open and close normal doors weight", 9, "행동 결정 시 일반 문을 열고 닫을 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.lockUnlockDoorsWeight = eventConfig.Bind(nameof(FacilityGhost), "Lock and unlock normal doors weight", 3, "행동 결정 시 일반 문을 잠그거나 열 가중치입니다.").Value;
            Minus.Handlers.FacilityGhost.chanceToOpenCloseDoor = eventConfig.Bind(nameof(FacilityGhost), "Chance to open and close normal doors", 0.3f, "문 열기·닫기를 선택했을 때, 각 문마다 실제로 적용될 확률입니다.").Value;
            Minus.Handlers.FacilityGhost.rageTurretsChance = eventConfig.Bind(nameof(FacilityGhost), "Chance to rage a turret", 0.3f, "포탑 폭주를 선택했을 때, 각 포탑마다 실제로 적용될 확률입니다.").Value;

            Minus.Handlers.RealityShift.normalScrapWeight = eventConfig.Bind(nameof(RealityShift), "Normal shift weight", 85, "스크랩을 다른 스크랩으로 바꿀 가중치입니다.").Value;
            Minus.Handlers.RealityShift.grabbableLandmineWeight = eventConfig.Bind(nameof(RealityShift), "Grabbable landmine shift weight", 15, "스크랩을 줍기 가능한 지뢰로 바꿀 가중치입니다.").Value;
            Minus.Handlers.RealityShift.transmuteChance = eventConfig.Bind(nameof(RealityShift), "Chance to transmute", 0.5f, "변환이 일어날 확률입니다.").Value;
            Minus.Handlers.RealityShift.enemyTeleportChance = eventConfig.Bind(nameof(RealityShift), "Enemy teleport chance", 0.1f, "적이 맞았을 때 순간이동할 확률입니다.").Value;


            DDay.bombardmentInterval = eventConfig.Bind(nameof(Warzone), "Bombardment interval", 100, "포격 이벤트 사이의 대기 시간입니다.").Value;
            DDay.bombardmentTime = eventConfig.Bind(nameof(Warzone), "Bombardment time", 15, "포격이 한 번 발생하면 지속되는 시간(초)입니다.").Value;
            DDay.fireInterval = eventConfig.Bind(nameof(Warzone), "Fire interval", 1, "포격 중 발사 간격(초)입니다.").Value;
            DDay.fireAmount = eventConfig.Bind(nameof(Warzone), "Fire amount", 8, "발사 간격마다 쏘는 발 수입니다. 큰 맵일수록 더 많아집니다.").Value;
            DDay.displayWarning = eventConfig.Bind(nameof(Warzone), "Display warning?", true, "포격 전에 경고 메시지를 표시할지 여부입니다.").Value;
            DDay.volume = eventConfig.Bind(nameof(Warzone), "Siren Volume?", 0.3f, "사이렌 음량입니다(0.0~1.0).").Value;
            ArtilleryShell.speed = eventConfig.Bind(nameof(Warzone), "Artillery shell speed", 100.0f, "포탄이 날아가는 속도입니다.").Value;

            //Minus.Handlers.Mimics.spawnRateScales[0] = getScale(moddedEventConfig.Bind(nameof(Mimics), "Zero Mimics Scale", "0, 0, 0, 0", "Weight Scale of zero mimics spawning   " + scaleDescription).Value);
            //Minus.Handlers.Mimics.spawnRateScales[1] = getScale(moddedEventConfig.Bind(nameof(Mimics), "One Mimic Scale", "0, 0, 0, 0", "Weight Scale of one mimic spawning   " + scaleDescription).Value);
            //Minus.Handlers.Mimics.spawnRateScales[2] = getScale(moddedEventConfig.Bind(nameof(Mimics), "Two Mimics Scale", "0, 0, 0, 0", "Weight Scale of two mimics spawning   " + scaleDescription).Value);
            //Minus.Handlers.Mimics.spawnRateScales[3] = getScale(moddedEventConfig.Bind(nameof(Mimics), "Three Mimics Scale", "80.0, -1.25, 5.0, 80.0", "Weight Scale of three mimics spawning   " + scaleDescription).Value);
            //Minus.Handlers.Mimics.spawnRateScales[4] = getScale(moddedEventConfig.Bind(nameof(Mimics), "Four Mimics Scale", "40.0, -0.5, 10.0, 40.0", "Weight Scale of four mimics spawning   " + scaleDescription).Value);
            //Minus.Handlers.Mimics.spawnRateScales[5] = getScale(moddedEventConfig.Bind(nameof(Mimics), "Maximum Mimics Scale", "10.0, 0.84, 10.0, 60.0", "Weight Scale of maximum mimics spawning   " + scaleDescription).Value);

            // Level properties config
            foreach (SelectableLevel level in StartOfRound.Instance.levels)
            {
                if (level == null) continue;

                Scale minScrapAmount = getScale(levelPropertiesConfig.Bind($"{level.levelID}:{level.name}", "Min scrap amount scale", "1.0, 0.0, 1.0, 1.0", scaleDescription).Value);
                Scale maxScrapAmount = getScale(levelPropertiesConfig.Bind($"{level.levelID}:{level.name}", "Max scrap amount scale", "1.0, 0.0, 1.0, 1.0", scaleDescription).Value);
                Scale minScrapValue = getScale(levelPropertiesConfig.Bind($"{level.levelID}:{level.name}", "Min scrap value scale", "1.0, 0.0, 1.0, 1.0", scaleDescription).Value);
                Scale maxScrapValue = getScale(levelPropertiesConfig.Bind($"{level.levelID}:{level.name}", "Max scrap value scale", "1.0, 0.0, 1.0, 1.0", scaleDescription).Value);

                levelProperties.TryAdd(level.levelID, new LevelProperties(level.levelID, minScrapAmount, maxScrapAmount, minScrapValue, maxScrapValue));
            }

        }

        internal static bool Initalized = false;
        public static void CreateConfig()
        {
            if (Initalized) return;

            // Initalize Events
            foreach (MEvent e in EventManager.vanillaEvents) e.Initalize();
            foreach (MEvent e in EventManager.moddedEvents) e.Initalize();

            // Config
            Initalize();

            // Use config settings
            for (int i = 0; i != EventManager.events.Count; i++)
            {
                EventManager.events[i].Weight = eventWeights[i].Value;
                EventManager.events[i].Descriptions = eventDescriptions[i];
                EventManager.events[i].ColorHex = eventColorHexes[i].Value;
                EventManager.events[i].Type = eventTypes[i].Value;
                EventManager.events[i].ScaleList = eventScales[i];
                EventManager.events[i].showTip = showTip[i].Value;
                EventManager.events[i].isWarning = TipIsWarning[i].Value;
                EventManager.events[i].TipTitle = TipsTitles[i];
                EventManager.events[i].TipMessages = TipsList[i];
                EventManager.events[i].Enabled = eventEnables[i].Value;
                EventManager.events[i].EventsToRemove = eventsToRemove[i];
                EventManager.events[i].EventsToSpawnWith = eventsToSpawnWith[i];
                EventManager.events[i].Aliases = eventAliases[i];
                EventManager.events[i].MoonMode = moonMode[i].Value;
                EventManager.events[i].Blacklist = moonBlacklist[i];
                EventManager.events[i].Whitelist = moonWhitelist[i];
                EventManager.events[i].isSpecialEvent = eventSpecial[i].Value;
                EventManager.events[i].isBetaEvent = eventBeta[i].Value;
                EventManager.events[i].monstersToSpawn = monsterEvents[i];
                EventManager.events[i].scrapTransmutationEvent = transmutationEvents[i];
            }

            // Create disabled events list and update
            List<MEvent> newEvents = new List<MEvent>();
            foreach (MEvent e in EventManager.events)
            {
                if (!e.Enabled)
                {
                    EventManager.disabledEvents.Add(e);
                }
                else
                {
                    newEvents.Add(e);
                    switch (e.Type)
                    {
                        case EventType.Insane:
                            EventManager.allInsane.Add(e);
                            break;
                        case EventType.VeryBad:
                            EventManager.allVeryBad.Add(e);
                            break;
                        case EventType.Bad:
                            EventManager.allBad.Add(e);
                            break;
                        case EventType.Neutral:
                            EventManager.allNeutral.Add(e);
                            break;
                        case EventType.Good:
                            EventManager.allGood.Add(e);
                            break;
                        case EventType.VeryGood:
                            EventManager.allVeryGood.Add(e);
                            break;
                        case EventType.Rare:
                            EventManager.allRare.Add(e);
                            break;
                        case EventType.Remove:
                            EventManager.allRemove.Add(e);
                            break;
                    }
                }
            }
            EventManager.events = newEvents;

            EventManager.UpdateEventTypeCounts();
            EventManager.UpdateAllEventWeights();

            Log.LogInfo(
                $"\n\nTotal Events:{EventManager.events.Count},   Disabled Events:{EventManager.disabledEvents.Count},   Total Events - Remove Count:{EventManager.events.Count - EventManager.eventTypeCount[5]}\n" +
                $"Very Bad:{EventManager.eventTypeCount[0]}\n" +
                $"Bad:{EventManager.eventTypeCount[1]}\n" +
                $"Neutral:{EventManager.eventTypeCount[2]}\n" +
                $"Good:{EventManager.eventTypeCount[3]}\n" +
                $"Very Good:{EventManager.eventTypeCount[4]}\n" +
                $"Remove:{EventManager.eventTypeCount[5]}\n");

            CreateAllEnemiesConfig();

            uiConfig.Save();
            difficultyConfig.Save();
            eventConfig.Save();
            weatherConfig.Save();
            customAssetsConfig.Save();
            moddedEventConfig.Save();
            //customEventConfig.Save();
            allEnemiesConfig.Save();
            levelPropertiesConfig.Save();
            CorePropertiesConfig.Save();
            //   extensiveSettingsConfig.Save();

            Initalized = true;
        }

        private static void CreateAllEnemiesConfig()
        {
            enableAllEnemies = allEnemiesConfig.Bind("_All Enemies", "Enable?", false, "모든 적이 모든 달에 스폰될 수 있게 합니다. 난이도가 크게 올라갑니다.");
            enableAllAllEnemies = allEnemiesConfig.Bind("_All All Enemies", "Enable?", false, "내부·외부 적 구분 없이 어디서나 스폰되게 합니다(거인·벌레가 시설 안에도 등장). 'All'과 'All All'을 함께 켜면 극악 난이도입니다.");

            List<EnemySpawnInfo> allSpawnInfos = new List<EnemySpawnInfo>()
            {
                // Inside
                CreateEnemyEntry(EnemyName.Bracken, 8, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.HoardingBug, 60, 10, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.CoilHead, 20, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Thumper, 25, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.BunkerSpider, 35, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Jester, 7, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.SnareFlea, 45, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Hygrodere, 10, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.GhostGirl, 5, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.SporeLizard, 15, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.NutCracker, 15, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Masked, 10, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Butler, 20, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Lasso, 5, 1, SpawnLocation.Inside),
                CreateEnemyEntry(kamikazieBug.name, 30, 5, SpawnLocation.Inside),
                CreateEnemyEntry(antiCoilHead.name, 10, 2, SpawnLocation.Inside),
                CreateEnemyEntry(nutSlayer.name, 3, 1, SpawnLocation.Inside),
                // Outside
                CreateEnemyEntry(EnemyName.EyelessDog, 25, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.ForestKeeper, 10, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.EarthLeviathan, 8, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.BaboonHawk, 35, 10, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.OldBird, 6, 3, SpawnLocation.Outside)
            };

            foreach (EnemyType enemy in EnemyList.Values)
            {
                if (enemy == null || enemy.enemyPrefab == null || enemy.isDaytimeEnemy || allSpawnInfos.Exists(x => x.enemy.name == enemy.name)) continue;

                if (enemy.isOutsideEnemy)
                {
                    CreateEnemyEntry(enemy.name, 5, 1, SpawnLocation.Outside);
                }
                else
                {
                    CreateEnemyEntry(enemy.name, 5, 1, SpawnLocation.Inside);
                }
            }

            allEnemiesCycle = new SpawnCycle()
            {
                enemies = allSpawnInfos,
                nothingWeight = allEnemiesConfig.Bind("_All Enemies", "All enemies nothing weight", 400.0f, "스폰이 일어나지 않을 가중치입니다.").Value,
                spawnAttemptInterval = allEnemiesConfig.Bind("_All Enemies", "Spawn interval", 86.0f, "이 주기가 스폰을 시도하는 간격(초)입니다.").Value,
                spawnCycleDuration = 0.0f
            };

            header = "All All Enemies";
            List<EnemySpawnInfo> allAllSpawnInfos = new List<EnemySpawnInfo>()
            {
                // Inside
                CreateEnemyEntry(EnemyName.Bracken, 8, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.HoardingBug, 60, 10, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.CoilHead, 20, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Thumper, 25, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.BunkerSpider, 35, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Jester, 7, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.SnareFlea, 45, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Hygrodere, 10, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.GhostGirl, 5, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.SporeLizard, 15, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.NutCracker, 15, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Masked, 10, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Butler, 20, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.Lasso, 5, 1, SpawnLocation.Inside),
                CreateEnemyEntry(kamikazieBug.name, 30, 5, SpawnLocation.Inside),
                CreateEnemyEntry(antiCoilHead.name, 10, 2, SpawnLocation.Inside),
                CreateEnemyEntry(nutSlayer.name, 3, 1, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.EyelessDog, 10, 5, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.ForestKeeper, 6, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.EarthLeviathan, 8, 3, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.BaboonHawk, 20, 10, SpawnLocation.Inside),
                CreateEnemyEntry(EnemyName.OldBird, 6, 3, SpawnLocation.Inside),
                // Outside
                CreateEnemyEntry(EnemyName.Bracken, 4, 1, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.HoardingBug, 30, 10, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.CoilHead, 10, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Thumper, 13, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.BunkerSpider, 18, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Jester, 3, 1, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.SnareFlea, 10, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Hygrodere, 5, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.GhostGirl, 3, 1, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.SporeLizard, 7, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.NutCracker, 8, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Masked, 5, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Butler, 10, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.Lasso, 3, 1, SpawnLocation.Outside),
                CreateEnemyEntry(kamikazieBug.name, 15, 5, SpawnLocation.Outside),
                CreateEnemyEntry(antiCoilHead.name, 5, 2, SpawnLocation.Outside),
                CreateEnemyEntry(nutSlayer.name, 2, 1, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.EyelessDog, 15, 5, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.ForestKeeper, 10, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.EarthLeviathan, 12, 3, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.BaboonHawk, 35, 10, SpawnLocation.Outside),
                CreateEnemyEntry(EnemyName.OldBird, 10, 3, SpawnLocation.Outside)
            };

            foreach (EnemyType enemy in EnemyList.Values)
            {
                if (enemy == null || enemy.enemyPrefab == null || allSpawnInfos.Exists(x => x.enemy.name == enemy.name)) continue;

                CreateEnemyEntry(enemy.name, 5, 1, SpawnLocation.Inside);
                CreateEnemyEntry(enemy.name, 5, 1, SpawnLocation.Outside);
            }

            allAllEnemiesCycle = new SpawnCycle()
            {
                enemies = allAllSpawnInfos,
                nothingWeight = allEnemiesConfig.Bind("_All All Enemies", "All enemies nothing weight", 400.0f, "스폰이 일어나지 않을 가중치입니다.").Value,
                spawnAttemptInterval = allEnemiesConfig.Bind("_All All Enemies", "Spawn interval", 86.0f, "이 주기가 스폰을 시도하는 간격(초)입니다.").Value,
                spawnCycleDuration = 0.0f
            };
        }

        private static string header = "All Enemies";
        private static EnemySpawnInfo CreateEnemyEntry(string enemy, float defaultWeight, int spawnCap, SpawnLocation spawnLocation)
        {
            return new EnemySpawnInfo()
            {
                enemy = GetEnemyOrDefault(enemy).enemyPrefab,
                enemyWeight = allEnemiesConfig.Bind(header, $"{spawnLocation} {enemy} Weight", defaultWeight, "스폰 가중치입니다.").Value,
                spawnCap = allEnemiesConfig.Bind(header, $"{spawnLocation} {enemy} Spawn Cap", spawnCap, "최대 스폰 수입니다.").Value,
                spawnLocation = spawnLocation
            };
        }

        private static EnemySpawnInfo CreateEnemyEntry(EnemyName name, float defaultWeight, int spawnCap, SpawnLocation spawnLocation) => CreateEnemyEntry(EnemyNameList[name], defaultWeight, spawnCap, spawnLocation);

        [HarmonyPrefix]
        [HarmonyPatch(typeof(TimeOfDay), "Awake")]
        private static void OnTimeDayStart(ref TimeOfDay __instance)
        {
            enableQuotaChanges = difficultyConfig.Bind("Quota Settings", "_Enable Quota Changes", false, "true로 설정한 뒤 세이브를 불러오면 나머지 할당량 설정이 생성됩니다. 기본값은 게임 값을 따릅니다.");
            if (enableQuotaChanges.Value)
            {
                __instance.quotaVariables.deadlineDaysAmount = difficultyConfig.Bind("Quota Settings", "Deadline Days Amount", __instance.quotaVariables.deadlineDaysAmount).Value;
                __instance.quotaVariables.startingCredits = difficultyConfig.Bind("Quota Settings", "Starting Credits", __instance.quotaVariables.startingCredits).Value;
                __instance.quotaVariables.startingQuota = difficultyConfig.Bind("Quota Settings", "Starting Quota", __instance.quotaVariables.startingQuota).Value;
                __instance.quotaVariables.baseIncrease = difficultyConfig.Bind("Quota Settings", "Base Increase", __instance.quotaVariables.baseIncrease).Value;
                __instance.quotaVariables.increaseSteepness = difficultyConfig.Bind("Quota Settings", "Increase Steepness", __instance.quotaVariables.increaseSteepness).Value;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Terminal), "Awake")]
        private static void OnTerminalAwake()
        {
            Manager.currentTerminal = GameObject.FindObjectOfType<Terminal>();
        }
    }
}