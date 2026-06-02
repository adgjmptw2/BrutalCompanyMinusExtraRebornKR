using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Unity.Netcode;
using UnityEngine;
using static BrutalCompanyMinus.Net;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Puma : MEvent
    {
        public override string Name() => nameof(Puma);

        public static Puma Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "고양이들", "걔네들은 널 사냥하려고 존재하는 거야...", "퓨마... 브랜드 퓨마 말고." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;
            MoonMode = true;
            Whitelist = new List<string>() { "Vow", "March" };
            isBetaEvent = true;

            monstersToSpawn = new List<MonsterEvent>() { new MonsterEvent(
                Assets.EnemyName.Puma,
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(5.0f, 0.2f, 5.0f, 25.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(0.0f, 0.0f, 0.0f, 0.0f),
                new Scale(1.0f, 0.0f, 1.0f, 1.0f),
                new Scale(2.0f, 0.60f, 2.0f, 6.0f))
            };

            

            //EventsToSpawnWith = new List<string>() { nameof(Tree) };

            //ScaleList.Add(ScaleType.MinDensity, new Scale(0.018f, 0.0f, 0.018f, 0.018f));
            //ScaleList.Add(ScaleType.MaxDensity, new Scale(0.025f, 0.0f, 0.025f, 0.025f));
        }

        //public override bool AddEventIfOnly() => (Assets.ReadSettingEarly(Paths.ConfigPath + "\\BrutalCompanyMinusExtraReborn\\CoreProperties.cfg", "Enable Beta Events?"));


        public override void Execute()
        {
            ExecuteAllMonsterEvents();
        }
    }
}
