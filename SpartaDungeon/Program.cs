namespace SpartaDungeon
{
    internal class Program
    {
        // 데이터 가져오기 기능 수행을 위해 만듦
        static Equipment equipmentInfo = new Equipment();

        static void Main(string[] args)
        {
            Player player1 = new Player(1, "Chad", "전사", 10, 5, 100, 1500);

            while (true)
            {
                ChoiceMenu(player1, ShowIntro());
            }
        }


        static int ShowIntro()
        {
            int choice = 0;

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장\n");
            choice = MakeChoice();

            Console.Clear();

            return choice;
        }

        static void ChoiceMenu(Player user, int c)
        {
            switch (c)
            {
                case 0:
                    // 메뉴로 돌아가 재선택
                    break;

                case 1:
                    ShowStat(user);
                    break;

                case 2:
                    ShowInventory(user);
                    break;

                case 3:
                    ShowStore(user);
                    break;

                case 4:
                    Console.WriteLine("던전");
                    break;
                case 5:
                    Console.WriteLine("휴식");
                    break;
                //잘못된 입력이 들어온 경우 switch 문 종료
                case int.MaxValue:
                    break;

                    //default 는 1~5 외의 숫자가 들어올 때 실행된다.
                default:
                    // 메뉴로 다시 돌아가기.
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.WriteLine("계속하시려면 아무 키나 누르세요...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
            }
        }

        static int MakeChoice()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine();
            int num = 0;
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            else
            {
                Console.WriteLine("잘못된 값을 입력했습니다.");
                return int.MaxValue;
            }
        }

        static void ShowStat(Player player)
        {
            player.ShowStat();
            // 0이라면 메뉴로 돌아가기 
            // 0이 아니라면 다시 입력하라고 하기.
            while (MakeChoice() != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.Clear();

            //메뉴로 돌아가기
            ChoiceMenu(player, ShowIntro());
        }

        static void ShowInventory(Player user)
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            foreach (Equipment e in user.GetItemList())
            {
                if(e.isEquip)
                {
                    Console.Write("[E] ");
                }
                e.ShowEquipmentInfo("");
            }

            Console.WriteLine("\n1. 장착 관리 ");
            Console.WriteLine("0. 나가기 ");
            int choice = MakeChoice();
            // 메뉴로
            if (choice == 0)
            {
                Console.Clear();
            }
            //장비 장착 관리
            else if (choice == 1)
            {
                ManageItem(user);
            }
            // 0과 1 이외의 다른 값을 입력했을 때
            else
            {
                // 제대로 된 값을 입력했을 때는 위의 0, 1번을 따른다.
                while (choice != 0 && choice != 1)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    choice = MakeChoice();
                }
                // 결국 0이라면 메뉴로
                if(choice == 0)
                {
                    Console.Clear();
                }
                // 장착 관리 메뉴로
                if(choice == 1)
                {
                    ManageItem(user);
                }
            }
        }

        static void ManageItem(Player user)
        {
            // 인벤토리에 아이템이 있다면
            if (user.GetItemList().Count != 0)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]\n");
                int count = 1;
                // 장비 목록 출력
                foreach (Equipment e in user.GetItemList())
                {
                    Console.Write("{0}. ", count);
                    if (e.isEquip)
                    {
                        Console.Write("[E] ");
                    }
                    e.ShowEquipmentInfo("");
                    count++;
                }
                Console.WriteLine("\n0. 나가기 ");
                // 메뉴 개수에 따라 선택지를 조정해야한다.
                // (ex. 장비가 10개면 목록이 10개) - 카운트 갯수 만큼 선택지를 활성화 시키기.
                int choice = MakeChoice();
                while(count < choice)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    choice = MakeChoice();
                }
                count = 0;

                if (choice == 0)
                {
                    Console.Clear();
                    ShowInventory(user);
                }
                // 선택된 장비를 장착하기. (스탯 반영됨.)
                else
                {
                    Equipment e = user.GetItemList()[choice - 1];
                    if (e.isEquip)
                    {
                        user.UnEquipItem(e);
                    }
                    //장착 시에는 타 장비를 해제해야 함.
                    else
                    {
                        user.EquipItem(e);
                    }                  
                    Console.Clear();
                    ManageItem(user);
                }
            }
            else
            {
                Console.WriteLine("관리할 장비가 없습니다. 메뉴로 돌아갑니다.");
                Console.WriteLine("계속 하시려면 아무 키나 입력해주세요.");
                Console.ReadKey(true);
                Console.Clear();
                ChoiceMenu(user, ShowIntro());
            }
        }

        static void ShowStore(Player user)
        {
            PrintStoreInfo("", user);

            equipmentInfo.ShowEntireItem(false);
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            int choice = MakeChoice();

            
            while(choice > 2)
            {
                if(choice < int.MaxValue)
                    Console.WriteLine(("범위를 벗어난 값을 입력했습니다."));
                choice = MakeChoice();
            }

            switch (choice)
            {
                case 0:
                    //메뉴로 돌아갑니다.
                    Console.Clear();
                    break;
                // 아이템 구매 메뉴
                case 1:              
                    Console.Clear();
                    PrintStoreInfo("- 아이템 구매", user);                   
                    equipmentInfo.ShowEntireItem(true);
                    Console.WriteLine("\n0. 나가기");
                    int _choice = MakeChoice();

                    while (_choice > equipmentInfo.itemData.Count)
                    {
                        _choice = MakeChoice();
                    }

                    if(_choice == 0)
                    {
                        Console.Clear();
                        //메뉴로
                    }
                    else
                    {
                        user.BuyItem(equipmentInfo.itemData[_choice - 1]);
                        Console.ReadKey(true);
                        Console.Clear();
                        ShowStore(user);
                    }                        
                    //구매
                    break;
                // 아이템 판매 메뉴
                case 2:
                    Console.Clear();
                    PrintStoreInfo("- 아이템 판매", user);
                    // 인벤토리에 있는 아이템 목록을 보여준다.
                    user.ShowEntireInventory(true);
                    Console.WriteLine("\n0. 나가기");
                    _choice = MakeChoice();

                    while (_choice > user.GetItemList().Count)
                    {
                        _choice = MakeChoice();
                    }

                    if (_choice == 0)
                    {
                        ShowStore(user);
                    }
                    else
                    {
                        user.SellItem(_choice - 1);
                        ShowStore(user);
                    }
                    break;
            }
        }

        static void PrintStoreInfo(string str, Player user)
        {
            Console.WriteLine("상점 {0}\r\n필요한 아이템을 얻을 수 있는 상점입니다.\n",str);
            Console.WriteLine(String.Format($"보유 골드 : {user.gold}\n"));
            Console.WriteLine("[아이템 목록]\n");
        }
    }
}
