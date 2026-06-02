using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class KiwiBird : MEvent
    {
        public override string Name() => nameof(KiwiBird);

        public static KiwiBird Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "아침 식사로 계란은 어때요!", "살살 부추기는 중입니다", "그만한 가치가 있을까요?", "거대 키위를 조심하세요", "저 쪼아대는 소리는 뭐죠?", "딱따구리 같긴 한데..." };
            ColorHex = "#800000";
            Type = EventType.VeryBad;
            isSpecialEvent = true;
            isBetaEvent = false;
        }

        public override void Execute()
        {
            // I hate this method used to spawn the Giant Kiwi Bird but it somehow works.
            GameObject hangarShip = Assets.hangarShip;
            if (hangarShip == null)
            {
                return;
            }

            try
            {
                Vector3 shipPosition = hangarShip.transform.position;
                Vector3 spawnSpot = RoundManager.Instance.GetRandomNavMeshPositionInRadiusSpherical(shipPosition, 170.0f); // Hope the position you receive is a good one. This position has a chance to be "undesirable" for players depending on the moon.
                EnemyType giantKiwiType = Assets.GetEnemy(Assets.EnemyName.GiantKiwi);
                RoundManager.Instance.SpawnEnemyGameObject(spawnSpot, 0, 1, giantKiwiType); // Spawn the Giant Kiwi
                if (GameObject.FindObjectOfType<EnemyAINestSpawnObject>() == null)
                {
                    Log.LogWarning("A nest was not found. Spawning Next");
                    GiantKiwiAI giantKiwiAI = GameObject.FindObjectOfType<GiantKiwiAI>();
                    try
                    {
                        giantKiwiAI.SpawnBirdNest(); // Should things go wrong? Who knows if this is needed.
                    }
                    catch (Exception ex)
                    {
                        Log.LogError($"Error while spawning GiantKiwiAI nest: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"Error while spawning GiantKiwiAI: {ex.Message}");
            }
        }

        //public override bool AddEventIfOnly() => Assets.ReadSettingEarly(Paths.ConfigPath + "\\BrutalCompanyMinusExtraReborn\\CoreProperties.cfg", "Enable Special Events?");
    }
}

