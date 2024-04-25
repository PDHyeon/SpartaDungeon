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
            Easy = 1,
            Normal = 2,
            Hard = 3
        }

        public Level level { get; set; }
        int leastDef;
        int clearGold;
        int minusHp;
        int clearCount;

        List<Dungeon> dungeonData = new List<Dungeon>();

        public Dungeon()
        {         
            
        }
        public Dungeon(Level _level, int def, int gold) 
        {
            level = _level;
            leastDef = def;
            clearGold = gold;
        }
        
        public bool ChallengeDungeon(Player user)
        {
            Random rd = new Random();
            int x = rd.Next(0, 10);
            minusHp = rd.Next(20, 35) + leastDef - (int)user.def;
            if (user.def < leastDef)
            {
                if(x < 4)
                {
                    FailDungeon(user);
                    return true;
                }
                else
                {
                    return ClearDungeon(user, minusHp);
                }
            }

            else
            {
                return ClearDungeon(user, minusHp);
            }
        }

        void FailDungeon(Player user)
        {
            Console.Clear();
            user.hp /= 2;
            clearGold = 0;
            Console.WriteLine("클리어 실패...\n");
            Console.WriteLine("{0} 던전 도전에 실패했습니다..\n", level);
            Console.WriteLine("0. 나가기");
        }
        bool ClearDungeon(Player user, int minusHp)
        {
            Console.Clear();
            Random rd = new Random();
            float bonusPercent = (float)rd.Next((int)user.atk, (int)user.atk * 2 + 1) / 100;
            int oriHp = user.hp;
            int oriGold = user.gold;
            user.hp -= minusHp;
            if(user.hp <= 0)
            {
                user.hp = 0;
                Console.WriteLine("모든 체력을 소모했습니다...");
                Console.WriteLine("게임이 종료되었습니다.");

                return false;
            }
            else
            {
                user.gold += (int)(clearGold * (1 + bonusPercent));
                Console.WriteLine("던전 클리어!\n");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine("{0} 던전을 클리어 하였습니다!\n", level);

                DungeonResult(user, oriHp, oriGold);
                clearCount++;

                if(clearCount == user.level)
                {
                    user.LevelUp();
                    clearCount = 0;
                }
                return true;
            }           
        }

        void DungeonResult(Player user, int originHp, int originGold)
        {
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine(String.Format($"체력 {originHp} -> {user.hp}"));
            Console.WriteLine(String.Format($"Gold {originGold}G -> {user.gold}G"));
            Console.WriteLine("\n0. 나가기");
        }
        public void SetDungeon(Level _level)
        {
            level = _level;
            switch (_level)
            {
                case Level.Easy:
                    leastDef = 5;
                    clearGold = 1000;
                    break;
                case Level.Normal:
                    leastDef = 11;
                    clearGold = 1700;
                    break;
                case Level.Hard:
                    leastDef = 17;
                    clearGold = 2500;
                    break;
            }
        }

        void AddDungeonData()
        {
            dungeonData.Add(new Dungeon(Level.Easy, 5, 1000));
            dungeonData.Add(new Dungeon(Level.Normal, 8, 1700));
            dungeonData.Add(new Dungeon(Level.Hard, 12, 2500));
        }
    }
}
