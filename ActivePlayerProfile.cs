using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    
    public struct ActivePlayerProfile
    {
        public static Sprite shipSprite;
        public static Stage[] stageInfo;
        public static int selectedStage;
        public static string chosenCharacter = "";
        public static string shipName = "";
        public static int saveSlot = 1;
        public static int level = 1;
        public static int experience = 0;
        public static int maxHealth = 1;
        public static int currentHealth = 1;
        public static int firePower = 1;
        public static float speed = 1;
        public static int upgradeTokens = 0;
        public static bool[] upgrades = new bool[18];
        public static bool[] stages = new bool[6];
        
    }
}
