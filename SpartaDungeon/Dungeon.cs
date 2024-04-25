using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Dungeon
    {
        public enum Level
        {
            Easy = 0,
            Normal,
            Hard
        }

        Level level;
        int leastDef;
        int clearGold;

        List<Dungeon> dungeonData = new List<Dungeon>();

        public Dungeon(Level _level, int def, int gold) 
        {
            level = _level;
            leastDef = def;
            clearGold = gold;
        }
        
        public void ChallengeDungeon()
        {

        }

        public void ClearDungeon()
        {

        }
        void SetDungeon()
        {
            dungeonData.Add(new Dungeon(Level.Easy,5, 1000));
            dungeonData.Add(new Dungeon(Level.Normal, 8, 1700));
            dungeonData.Add(new Dungeon(Level.Hard, 12, 2500));
        }
    }
}
