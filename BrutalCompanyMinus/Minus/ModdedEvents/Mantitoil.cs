using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Mantitoil : MEvent
    {
        public override string Name() => nameof(Mantitoil);

        public static Mantitoil Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "비행 터렛!", "방금 터렛이 날아가는 걸 본 건가요?", "공중 저격수", "맨티토일???" };
            ColorHex = "#800000";
            Type = EventType.VeryBad;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.Manticoil,
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(8.0f, 0.0f, 8.0f, 8.0f),
                new Scale(10.0f, 0.0f, 10.0f, 10.0f))
            };

            EventsToRemove = new List<string>() { nameof(NoMantitoil) };
            ScaleList.Add(ScaleType.InsideEnemyRarity, new Scale(0f, 0f, 0f, 0f));
            ScaleList.Add(ScaleType.MaxInsideEnemyCount, new Scale(0f, 0f, 0f, 0f));
        }

        public override bool AddEventIfOnly() => Compatibility.toilheadPresent;

        public override void Execute()
        {
            if (!Compatibility.toilheadPresent) return;
            ExecuteAllMonsterEvents();
            com.github.zehsteam.ToilHead.Api.ForceMantiToilSpawns = true;
            // com.github.zehsteam.ToilHead.Api.forceSpawns = true;

            
        }

        
    }
}
