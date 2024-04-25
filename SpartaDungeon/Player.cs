using static SpartaDungeon.Equipment;

namespace SpartaDungeon
{
    public class Player
    {
        public int level { get; set; }
        string name;
        string job;
        public float atk { get; set; } 
        public int def { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        List<Equipment> inventory = new List<Equipment>();

        int upAtk;
        int upDef;

        //현재 무기
        Equipment nowWeapon;
        Equipment nowArmor;

        public Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            this.level = level;
            this.name = name;
            this.job = job;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.gold = gold;
        }

        public void ShowStat()
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine(String.Format($"Lv. {level}"));
            Console.WriteLine(String.Format($"{name} ({job})"));
            if (upAtk != 0)
            {
                Console.WriteLine(String.Format($"공격력 :  {atk + upAtk} (+ {upAtk})"));
            }
            else
                Console.WriteLine(String.Format($"공격력 :  {atk}"));
            if (upDef != 0)
            {
                Console.WriteLine(String.Format($"공격력 :  {def + upDef} (+ {upDef})"));
            }
            else
            {
                Console.WriteLine(String.Format($"방어력 :  {def}"));
            }
            Console.WriteLine(String.Format($"체력 :  {hp}"));
            Console.WriteLine(String.Format($"Gold :  {gold}\n"));
            Console.WriteLine("0.나가기");
        }

        public void PlayerGetItem(Equipment e)
        {
            //inventory.Add(e.itemData[1]);
        }

        public void EquipItem(Equipment e)
        {
            if (e.getEquipmentType() == EquipmentType.Weapon)
            {
                if (nowWeapon == null)
                {
                    upAtk = e.GetEquipmnentStat();
                    nowWeapon = e;
                    e.isEquip = true;
                }
                else
                {
                    UnEquipOutofSelect(e);
                }
            }
            else
            {
                if (nowArmor == null)
                {
                    upDef += e.GetEquipmnentStat();
                    nowArmor = e;
                    e.isEquip = true;
                }
                else
                {
                    UnEquipOutofSelect(e);
                }
            }
        }

        public void UnEquipItem(Equipment e)
        {
            e.isEquip = false;
            if (e.getEquipmentType() == EquipmentType.Weapon)
            {
                upAtk -= e.GetEquipmnentStat();
                nowWeapon = null;
            }
            else
            {
                upDef -= e.GetEquipmnentStat();
                nowArmor = null;
            }
        }

        public void UnEquipOutofSelect(Equipment e)
        {
            //장착 외의 아이템을 해제한다.
            foreach (Equipment equip in inventory)
            {
                if (equip != e && equip.getEquipmentType() == e.getEquipmentType())
                {
                    UnEquipItem(equip);
                    equip.isEquip = false;
                }

                if (e.getEquipmentType() == EquipmentType.Weapon)
                {
                    upAtk = e.GetEquipmnentStat();
                    nowWeapon = e;
                }

                else
                {
                    upDef = e.GetEquipmnentStat();
                    nowArmor = e;
                }
            }
            e.isEquip = true;
        }

        public void BuyItem(Equipment e)
        {
            if (!e.isSelled)
            {
                if (gold - e.cost >= 0)
                {
                    Console.WriteLine(String.Format($"{e.name} 을 구매했습니다."));
                    gold -= e.cost;
                    e.isSelled = true;
                    inventory.Add(e);
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
        }

        public void SellItem(int idx)
        {
            int sellCost = (int)(inventory[idx].cost * 0.85f);
            if (inventory[idx].isEquip)
            {
                inventory[idx].isEquip = false;
            }
            inventory[idx].isSelled = false;
            if (inventory[idx].getEquipmentType() == EquipmentType.Weapon)
            {
                upAtk = 0;
            }
            else
            {
                upDef = 0;
            }

            Console.WriteLine(inventory[idx].name + "을/를 " + sellCost+"G에 판매했습니다.");
            gold += sellCost;
            Console.WriteLine("계속 하시려면 아무 키나 눌러주세요.");
            Console.ReadKey(true);
            Console.Clear();
            inventory.Remove(inventory[idx]);
        }

        public void ShowEntireInventory(bool isSelling)
        {
            int count = 0;

            int sellPrice = 0;
            string sellstr = "";
            foreach(Equipment e in inventory)
            {
                if (isSelling)
                {
                    sellPrice = (int)(e.cost * 0.85f);
                    sellstr = sellPrice.ToString() + "G";
                }
                Console.Write("-{0} ",count + 1);
                inventory[count++].ShowEquipmentInfo(sellstr);
            }
            Console.WriteLine();
        }

        public void LevelUp()
        {
            level += 1;
            atk += 0.5f;
            def += 1;
        }
        public List<Equipment> GetItemList()
        {
            return inventory;
        }
    }
}
