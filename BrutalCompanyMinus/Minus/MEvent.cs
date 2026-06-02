using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BrutalCompanyMinus.Minus
{
    public class MEvent
    {
        /// <summary>
        /// This is the text displayed in the UI.
        /// </summary>
        public List<string> Descriptions = new List<string>() { "" };

        /// <summary>
        /// The color the event will be displayed in the UI in hex.
        /// </summary>
        public string ColorHex = "#FFFFFF";

        /// <summary>
        /// This can be ignored, this value is only used when Use_Custom_Weights in the config is set to true.
        /// </summary>
        public int Weight = 1;

        /// <summary>
        /// Set in what your opinion the severity of this event.
        /// </summary>
        public EventType Type = EventType.Neutral;

        /// <summary>
        /// If true event will appear, if false event will not appear.
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        /// If the event is a special event, declare it true.
        /// </summary>
        public bool isSpecialEvent = false;

        /// <summary>
        /// If the event is a beta event, declare it true.
        /// </summary>
        public bool isBetaEvent = false;

        /// <summary>
        /// If true, a box ingame will come up with a random tip from TipMessages if the event pops up.
        /// </summary>
        public bool showTip = false;

        /// <summary>
        /// If showTip is true, these messages will be randomly chosen,
        /// </summary>
        public List<string> TipMessages = new List<string>() { "" };

        /// <summary>
        /// If show tip is true, this will be the title of the tip box.
        /// </summary>
        public List<string> TipTitle = new List<string>() { "" };

        /// <summary>
        /// If true, this will be an warning message (Red), otherwise appears yellow.
        /// </summary>
        public bool isWarning = true;

        /// <summary>
        /// Set scales in Initalize() and then use Getf(ScaleType) or Get(ScaleType) to compute scale in Execute(), this will also generate automatically generate in the config.
        /// </summary>
        public Dictionary<ScaleType, Scale> ScaleList = new Dictionary<ScaleType, Scale>();

        /// <summary>
        /// Set this in Initalize() to specify events to prevent from occuring.
        /// </summary>
        public List<string> EventsToRemove = new List<string>();

        /// <summary>
        /// Set this in Initalize() to specify events to use whitelist mode instead of blacklist mode.
        /// </summary>
        public bool MoonMode = false;

        /// <summary>
        /// Set this in Initalize() to specify moons to prevent this event from occuring.
        /// </summary>
        public List<string> Blacklist = new List<string>();

        /// <summary>
        /// Set this in Initalize() to specify moons to prevent this event from occuring. Whitelist takes priority over Blacklist
        /// </summary>
        public List<string> Whitelist = new List<string>();

        /// <summary>
        /// Set this in Initalize() to specify events to spawn with, these wont be shown in the UI.
        /// </summary>
        public List<string> EventsToSpawnWith = new List<string>();
        
        /// <summary>
        /// Used internally for logic functions for events.
        /// </summary>
        public bool Executed = false;

        /// <summary>
        /// This is used internally for events. Especially for events that require a Net Object registered for specific networked effects to occur.
        /// </summary>
        public bool Active = false;

        /// <summary>
        /// Internally used to prevent events from being included if speedrun mode is on.
        /// </summary>
        public bool SpeedRunSafe = true;

        /// <summary>
        /// Set this in Initalize() to specify aliases for the event. This is used for forcing events.
        /// </summary>
        public List<string> Aliases = new List<string>();

        /// <summary>
        /// Set this in Initalize() to make monster event(s).
        /// </summary>
        public List<MonsterEvent> monstersToSpawn = new List<MonsterEvent>();

        /// <summary>
        /// Set this in Initalize() to make a transmutation event.
        /// </summary>
        public ScrapTransmutationEvent scrapTransmutationEvent = new ScrapTransmutationEvent(new Scale(0.0f, 0.0f, 0.0f, 0.0f));

        /// <summary>
        /// This is the event type.
        /// </summary>
        public enum EventType
        {
            Insane, VeryBad, Bad, Neutral, Good, VeryGood, Rare, Remove
        }

        /// <summary>
        /// Use the right one to generate the right config name and description.
        /// </summary>
        public enum ScaleType
        {
            InsideEnemyRarity, OutsideEnemyRarity, DaytimeEnemyRarity, MinOutsideEnemy, MinInsideEnemy, MaxOutsideEnemy, MaxInsideEnemy,
            ScrapValue, ScrapAmount, FactorySize, MinDensity, MaxDensity, MinCash, MaxCash, MinItemAmount, MaxItemAmount, MinValue, MaxValue, Rarity, MinRarity, MaxRarity,
            MinCut, MaxCut, MinHp, MaxHp, SpawnMultiplier, MaxInsideEnemyCount, MaxOutsideEnemyCount, SpawnCapMultiplier, MinPercentageCut, MaxPercentageCut, MinAmount, MaxAmount, 
            Percentage, TimeSettings, TimeMin, TimeMax, MinPercentSelected, MaxPercentSelected, ObjectWidth, minMold, maxMold
        }

        internal static Dictionary<ScaleType, string> ScaleInfoList = new Dictionary<ScaleType, string>() {
            { ScaleType.InsideEnemyRarity, "시설 내부 적 목록에 희귀도와 함께 추가됩니다." }, 
            { ScaleType.OutsideEnemyRarity, "시설 외부 적 목록에 희귀도와 함께 추가됩니다." }, 
            { ScaleType.DaytimeEnemyRarity, "낮 시간 적 목록에 희귀도와 함께 추가됩니다." }, 
            { ScaleType.MinOutsideEnemy, "외부에 최소한 이만큼은 스폰됩니다." }, 
            { ScaleType.MaxOutsideEnemy, "외부에 최대 이만큼까지 스폰됩니다." }, 
            { ScaleType.MinInsideEnemy, "내부에 최소한 이만큼은 스폰됩니다." }, 
            { ScaleType.MaxInsideEnemy, "내부에 최대 이만큼까지 스폰됩니다." },
            { ScaleType.ScrapValue, "스크랩 가치에 곱해지는 배율입니다." }, 
            { ScaleType.ScrapAmount, "스크랩 수량에 곱해지는 배율입니다." }, 
            { ScaleType.FactorySize, "공장(시설) 크기에 곱해지는 배율입니다." },
            { ScaleType.MinDensity, "선택되는 최소 밀도 값입니다." }, 
            { ScaleType.MaxDensity, "선택되는 최대 밀도 값입니다." }, 
            { ScaleType.MinCash, "지급되는 최소 현금입니다." }, 
            { ScaleType.MaxCash, "지급되는 최대 현금입니다." },
            { ScaleType.MinItemAmount, "스폰되는 최소 아이템 수입니다." }, 
            { ScaleType.MaxItemAmount, "스폰되는 최대 아이템 수입니다." }, 
            { ScaleType.MinValue, "선택되는 최솟값입니다." }, 
            { ScaleType.MaxValue, "선택되는 최댓값입니다." },
            { ScaleType.Rarity, "일반적인 발생 확률입니다." }, 
            { ScaleType.MinRarity, "최소 발생 확률입니다." }, 
            { ScaleType.MaxRarity, "최대 발생 확률입니다." }, 
            { ScaleType.MinCut, "최소 차감 비율입니다." }, 
            { ScaleType.MaxCut, "최대 차감 비율입니다." }, 
            { ScaleType.MinHp, "선택될 수 있는 최소 체력입니다." }, 
            { ScaleType.MaxHp, "선택될 수 있는 최대 체력입니다." }, 
            { ScaleType.SpawnMultiplier, "스폰 확률에 곱해집니다." }, 
            { ScaleType.SpawnCapMultiplier, "스폰 상한에 곱해집니다." }, 
            { ScaleType.MaxInsideEnemyCount, "내부에 스폰 가능한 적 최대 수를 변경합니다." }, 
            { ScaleType.MaxOutsideEnemyCount, "외부에 스폰 가능한 적 최대 수를 변경합니다." }, 
            { ScaleType.MinPercentageCut, "최소 비율(%) 차감입니다." }, 
            { ScaleType.MaxPercentageCut, "최대 비율(%) 차감입니다." }, 
            { ScaleType.MinAmount, "선택되는 최소 수량입니다." }, 
            { ScaleType.MaxAmount, "선택되는 최대 수량입니다." }, 
            { ScaleType.Percentage, "0.0~1.0 사이 값입니다." }, 
            { ScaleType.TimeSettings, "시간 스케일 배율입니다." }, 
            { ScaleType.TimeMin, "선택되는 최소 시간입니다." }, 
            { ScaleType.TimeMax, "선택되는 최대 시간입니다." },
            { ScaleType.MinPercentSelected, "선택되는 최소 비율(%)입니다." },
            { ScaleType.MaxPercentSelected, "선택되는 최대 비율(%)입니다." },
            { ScaleType.ObjectWidth, "내비메시 가장자리에서 얼마나 가까이 오브젝트가 스폰될 수 있는지입니다." },
            { ScaleType.minMold, "선택되는 최소 곰팡이(몰드) 양입니다." },
            { ScaleType.maxMold, "선택되는 최대 곰팡이(몰드) 양입니다." }
        };

        /// <summary>
        /// This is used to scale events by difficulty.
        /// </summary>
        public struct Scale
        {
            public float Base, Increment, MinCap, MaxCap;

            public Scale(float Base, float Increment, float MinCap, float MaxCap)
            {
                this.Base = Base;
                this.Increment = Increment;
                this.MinCap = MinCap;
                this.MaxCap = MaxCap;
            }

            public static float Compute(Scale scale, EventType Type = EventType.Neutral)
            {
                float increment = scale.Increment;

                return Mathf.Clamp(scale.Base + (increment * Manager.difficulty), scale.MinCap, Configuration.ignoreMaxCap.Value ? 2147483647.0f : scale.MaxCap);
            }

            public float Computef(EventType type) => Compute(this, type);

            public int Compute(EventType type) => (int)Compute(this, type);
        }

        /// <summary>
        /// This is used to identify said event, preferably use nameof(thisClass) for name.
        /// </summary>
        /// <returns>Returns the given name.</returns>
        public virtual string Name() => "";

        /// <summary>
        /// This is called just right before config is generated.
        /// </summary>
        public virtual void Initalize() { }

        /// <summary>
        /// Event algorithm will only pick this event if this returns true.
        /// </summary>
        /// <returns>Always true if not overriden.</returns>
        public virtual bool AddEventIfOnly() { return true; }

        /// <summary>
        /// This is called after lever is pulled. This is only called on the host.
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// This is called when ship leaves.
        /// </summary>
        public virtual void OnShipLeave() { }

        /// <summary>
        /// This will only be called once when the game starts.
        /// </summary>
        public virtual void OnGameStart() { }

        /// <summary>
        /// This is called when the local player is disconncted.
        /// </summary>
        public virtual void OnLocalDisconnect() { }

        /// <summary>
        /// This is used to compute scales, can be overriden.
        /// </summary>
        /// <param name="scaleType">A scale from Scale List, if dosen't exist it will return 0.0f.</param>
        /// <returns>Returns computed float value of given ScaleType if found.</returns>
        public virtual float Getf(ScaleType scaleType)
        {
            try
            {
                return Scale.Compute(ScaleList[scaleType], Type);
            } catch
            {
                Log.LogError(string.Format("Scalar '{0}' for '{1}' not found, returning 0.", scaleType.ToString(), Name()));
            }
            return 0.0f;
        }

        /// <summary>
        /// This computes Getf() and then converts to int.
        /// </summary>
        /// <param name="scaleType">A scale from Scale List, if dosen't exist it will return 0.</param>
        /// <returns>Returns computed int value of given ScaleType if found.</returns>
        public int Get(ScaleType scaleType)
        {
            return (int)Getf(scaleType);
        }

        /// <summary>
        /// Will execute every monster event inside of monstersToSpawn.
        /// </summary>
        public void ExecuteAllMonsterEvents()
        {
            foreach(MonsterEvent monsterEvent in monstersToSpawn)
            {
                monsterEvent.Execute();
            }
        }

        /// <summary>
        /// Will return an event from name.
        /// </summary>
        /// <param name="name">Name of event to find.</param>
        /// <returns>Will return said event if found, otherwise it will return the Nothing event.</returns>
        public static MEvent GetEvent(string name)
        {
            int index = EventManager.events.FindIndex(x => x.Name() == name);
            if (index != -1) return EventManager.events[index];

            Log.LogError(string.Format("Event '{0}' dosen't exist, returning nothing event", name));
            return new Events.Nothing();
        }

        /// <summary>
        /// This is used to describe a basic monster event.
        /// </summary>
        public class MonsterEvent
        {
            public EnemyType enemy;

            public Scale insideSpawnRarity, outsideSpawnRarity, minInside, maxInside, minOutside, maxOutside;

            public EventType eventType;

            public MonsterEvent(EnemyType enemy, Scale insideSpawnRarity, Scale outsideSpawnRarity, Scale minInside, Scale maxInside, Scale minOutside, Scale maxOutside)
            {
                this.enemy = enemy;
                assignRarities(insideSpawnRarity, outsideSpawnRarity, minInside, maxInside, minOutside, maxOutside);
            }

            public MonsterEvent(Assets.EnemyName enemyName, Scale insideSpawnRarity, Scale outsideSpawnRarity, Scale minInside, Scale maxInside, Scale minOutside, Scale maxOutside)
            {
                this.enemy = Assets.GetEnemy(enemyName);
                assignRarities(insideSpawnRarity, outsideSpawnRarity, minInside, maxInside, minOutside, maxOutside);
            }

            public MonsterEvent(string enemyName, Scale insideSpawnRarity, Scale outsideSpawnRarity, Scale minInside, Scale maxInside, Scale minOutside, Scale maxOutside)
            {
                this.enemy = Assets.GetEnemy(enemyName);
                assignRarities(insideSpawnRarity, outsideSpawnRarity, minInside, maxInside, minOutside, maxOutside);
            }

            internal void assignRarities(Scale insideSpawnRarity, Scale outsideSpawnRarity, Scale minInside, Scale maxInside, Scale minOutside, Scale maxOutside)
            {
                this.insideSpawnRarity = insideSpawnRarity;
                this.outsideSpawnRarity = outsideSpawnRarity;
                this.minInside = minInside;
                this.maxInside = maxInside;
                this.minOutside = minOutside;
                this.maxOutside = maxOutside;
            }

            public void Execute()
            {
                Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.Enemies, enemy, insideSpawnRarity.Compute(eventType));
                Manager.AddEnemyToPoolWithRarity(ref RoundManager.Instance.currentLevel.OutsideEnemies, enemy, outsideSpawnRarity.Compute(eventType));
                Manager.Spawn.InsideEnemies(enemy, UnityEngine.Random.Range(minInside.Compute(eventType), maxInside.Compute(eventType) + 1)); 
                Manager.Spawn.OutsideEnemies(enemy, UnityEngine.Random.Range(minOutside.Compute(eventType), maxOutside.Compute(eventType) + 1));
            }
        }

        /// <summary>
        /// This is used to describe a scrap transmutation event.
        /// </summary>
        public class ScrapTransmutationEvent
        {
            public Scale amount; // Between 0.0 to 1.0

            public SpawnableItemWithRarity[] items;

            public ScrapTransmutationEvent(Scale amount, params SpawnableItemWithRarity[] items)
            {
                this.items = items;
                this.amount = amount;
            }

            public void Execute()
            {
                Manager.TransmuteScrap(amount.Computef(EventType.Neutral), items);
            }
        }
    }
}
