using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [Serializable]
    public class Stage
    {
        public int stageNumber;
        public int highscore;
        public bool[] upgrades = new bool[3] { false, false, false };

        public Stage(int stageNumber, int highscore, bool[] upgrades)
        {
            this.stageNumber = stageNumber;
            this.highscore = highscore;
            this.upgrades = upgrades;
        }
    }
}
