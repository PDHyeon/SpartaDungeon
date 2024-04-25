using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Equipment
    {
        public enum EquipmentType
        {
            Weapon = 0,
            Armor
        }

        public bool isEquip { get; set; }
        int stat;
        public string name { get; set; }
        string explain;
        EquipmentType type;
        public int cost { get; set; }
        public bool isSelled;

        public List<Equipment> itemData = new List<Equipment>();

        public Equipment()
        {
            AddItemData();
        }
        public Equipment(EquipmentType _type, int _stat, string _name, string _explain, int _cost)
        {
            isEquip = false;
            type = _type;
            stat = _stat;
            name = _name;
            explain = _explain;
            cost = _cost;
            isSelled = false;
            // 아이템 데이터를 저장 후에 인덱스로 가져다쓰는 형식으로 변경 예정
        }

        //데이터를 직접 저장
        void AddItemData()
        {
            itemData.Add(new Equipment(EquipmentType.Weapon,1, "나무 칼", "나무로 만든 평범한 칼입니다.", 300));
            itemData.Add(new Equipment(EquipmentType.Weapon, 3, "철제 칼", "철로 만든 튼튼한 칼입니다.", 1000));
            itemData.Add(new Equipment(EquipmentType.Weapon, 5, "흑요석 칼", "흑요석으로 만든 매우 날카로운 칼입니다.", 3000));
            itemData.Add(new Equipment(EquipmentType.Armor, 1, "천 갑옷", "천으로 만든 평범한 옷입니다.", 300));
            itemData.Add(new Equipment(EquipmentType.Armor, 3, "나무 갑옷", "나무로 만든 갑옷입니다. 조금 무겁습니다.", 1000));
            itemData.Add(new Equipment(EquipmentType.Armor, 5, "판금 갑옷", "판금으로 만든 갑옷입니다. 매우 무겁습니다.", 3000));
        }

        public void ShowEntireItem(bool isBuyPage)
        {
            if(itemData .Count == 0)
            {
                AddItemData();
            }
            int count = 1;
            
            foreach (Equipment item in itemData)
            {
                string str = String.Format($"-{count++} {item.name} | 공격력: +{item.stat} | {item.explain} | {item.cost}G");
                string sell = "";
                if (item.isSelled == true)
                {
                    sell = "[구매완료]";
                    str = str.Replace(item.cost + "G", sell);
                } 

                //앞의 숫자를 없애줌.
                if (!isBuyPage)
                {
                    str = str.Replace("-"+(count-1).ToString(), "-");
                }

                if (item.type == EquipmentType.Weapon)
                {
                    Console.WriteLine(str);
                }
                else
                {
                    Console.WriteLine(str.Replace("공격력", "방어력"));
                }
            }
        }

        public void ShowEquipmentInfo(string str)
        {            
            if(this.type == EquipmentType.Weapon)
            {
                Console.WriteLine(String.Format($"{this.name} | 공격력: +{this.stat} | {this.explain} | {str}"));
            }
            else
            {
                Console.WriteLine(String.Format($"{this.name} | 방어력: +{this.stat} | {this.explain} | {str}"));
            }
        }

        public int GetEquipmnentStat()
        {
            return stat;
        }

        public EquipmentType getEquipmentType()
        {
            return type;
        }
    }
}
