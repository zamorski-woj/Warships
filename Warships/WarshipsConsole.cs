using System.Text.RegularExpressions;
using static Warships.NPC;
using static Warships.WarshipsGame;

namespace Warships
{
    public static class WarshipsConsole
    {
        public static void Menu()
        {
            int numberOfPlayers=2;
            string? decision = null;
            while(decision == null)
            {
                Console.WriteLine("How many human players? 0, 1 or 2?");
                decision = Console.ReadKey().KeyChar.ToString();
                try
                {
                    numberOfPlayers = int.Parse(decision);
                }
                catch(FormatException f)
                {
                    continue;//with default 2
                }
            }

            Console.Clear();
            Console.WriteLine("What to call the first player?");
            string name1 = Console.ReadLine() ?? "Ewaryst";
            Console.WriteLine("What to call the second player?");
            string name2 = Console.ReadLine() ?? "Antyfilidor";
            Console.Clear();

            Player p =RunGame(numberOfPlayers, true, name1, name2);
            Console.WriteLine(p.Name + " won!");
            Console.ReadLine();
            Console.Clear();

        }

        public static Player RunGame(int numberOfPlayers, bool showNPC = true, string name1 = "Ewaryst", string name2 = "Antyfilidor")
        {
            
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers(name1, name2, 10);
            Player p1 = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            p1.GenerateFleet();
            p2.GenerateFleet();

            while (p1.FleetStillAlive() && p2.FleetStillAlive())
            {
                switch (numberOfPlayers)
                {
                    case 0:
                        PlayAutomaticTurn(p1, p2, showNPC);
                        PlayAutomaticTurn(p2, p1, showNPC);
                        break;
                    case 1:
                        PlayOneTurn(p1, p2);
                        PlayAutomaticTurn(p2, p1, showNPC);
                        break;
                    case 2:
                    default:
                        PlayOneTurn(p1, p2);
                        PlayOneTurn(p2, p1);
                        break;
                }
            }
            if (p1.FleetStillAlive())
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }

        private static void PlayOneTurn(Player player, Player opponent)
        {
            Console.Clear();
            Console.WriteLine(player.Name + "'s turn. Press any Key");
            Console.ReadKey();

            string? order = null;
            while (order == null || NotValidOrder(order))
            {
                Console.Clear();
                ShowBothMaps(player);
                Console.WriteLine("Your orders, Admiral " + player.Name + "?");
                order = Console.ReadLine();
            }
            Tuple<int, int> whereToShoot = CoordinatesFromString(order);
            CellType outcome = Shoot(opponent.Map, whereToShoot);
            player.LastMove = whereToShoot;
            player.EnemyMap.PlotOutcome(whereToShoot, outcome);
            Console.Clear();
            ShowBothMaps(player);
            Console.WriteLine(OutcomeToString(outcome));

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        public static string OutcomeToString(CellType outcome)
        {
            return outcome switch
            {
                CellType.Hit => "It'a a hit!",
                CellType.Ship => "It'a a hit!",
                CellType.Sunken => "Hit and sunken!",
                CellType.Water => "Missed!",
                _ => "Did you enter right coordinates?",
            };
        }
      
        private static bool NotValidOrder(string order)
        {
            if (Regex.IsMatch(order, @"^[a-jA-J][0-9]$") || Regex.IsMatch(order, @"^[0-9][a-jA-J]$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static Tuple<int, int> CoordinatesFromString(string order)
        {
            int x = 0, y = 0;
            if (Regex.IsMatch(order, @"^[a-jA-J][0-9]$"))
            {
                x = IntFromLetter(order[0].ToString());
                y = int.Parse(order[1].ToString());
            }
            if (Regex.IsMatch(order, @"^[0-9][a-jA-J]$"))
            {
                y = int.Parse(order[0].ToString());
                x = IntFromLetter(order[1].ToString());
            }

            return new Tuple<int, int>(x, y);
        }

        private static int IntFromLetter(string v)
        {
            return v switch
            {
                "a" or "A" => 0,
                "b" or "B" => 1,
                "c" or "C" => 2,
                "d" or "D" => 3,
                "e" or "E" => 4,
                "f" or "F" => 5,
                "g" or "G" => 6,
                "h" or "H" => 7,
                "i" or "I" => 8,
                _ => 9,
            };
        }

        private static void ShowBothMaps(Player p)
        {

            Console.WriteLine("Your map");
            ShowMap(p.Map, p.Opponent.LastMove);

            Console.WriteLine("Enemy map");
            ShowMap(p.EnemyMap, p.LastMove);
        }

        public static void ShowMap(Map m, Tuple<int, int>? lastMove = null)
        {
            lastMove ??= new Tuple<int, int>(-1, -1);
            int mapSize = m.Grid.GetLength(0);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ABCDEFGHIJ");
            Console.ResetColor();
            for (int j = 0; j < mapSize; j++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(j);
                Console.ResetColor();

                for (int i = 0; i < mapSize; i++)
                {
                    switch (m.Grid[i, j])
                    {
                        case CellType.Unknown:
                            Console.Write("?");
                            break;
                        case CellType.Water:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            if(lastMove.Item1==i && lastMove.Item2==j)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            Console.Write("~");
                            Console.ResetColor();
                            break;
                        case CellType.Ship:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            if (lastMove.Item1 == i && lastMove.Item2 == j)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            Console.Write("O");
                            Console.ResetColor();
                            break;
                        case CellType.Hit:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            if (lastMove.Item1 == i && lastMove.Item2 == j)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            Console.Write("*");
                            Console.ResetColor();
                            break;
                        case CellType.Sunken:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            if (lastMove.Item1 == i && lastMove.Item2 == j)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            Console.Write("X");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }
    }
}
