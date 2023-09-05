using System.Text.RegularExpressions;
using static Warships.WarshipsGame;

namespace Warships
{
    public static class WarshipsConsole
    {

        public static void Run()
        {
            Console.WriteLine("What to call the first player?");
            string name1 = Console.ReadLine() ?? "Ziutek";
            Console.WriteLine("What to call the second player?");
            string name2 = Console.ReadLine() ?? "Ewaryst";
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers(name1, name2, 10);
            Player p1 = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            p1.GenerateFleet();
            p2.GenerateFleet();

            while (p1.FleetStillAlive() && p2.FleetStillAlive())
            {
                PlayOneTurn(p1, p2);
                PlayOneTurn(p2, p1);
            }
            if (p1.FleetStillAlive())
            {
                Console.WriteLine(p1.name + " won!");
            }
            else
            {
                Console.WriteLine(p2.name + " won!");
            }
            Console.ReadLine();
            Console.Clear();
        }


        private static void PlayOneTurn(Player p, Player opponent)
        {

            Console.Clear();
            Console.WriteLine(p.name + "'s turn. Press any Key");
            Console.ReadKey();

            string? order = null;
            while (order == null || NotValidOrder(order))
            {
                Console.Clear();
                ShowBothMaps(p);
                Console.WriteLine("Your orders, Admiral " + p.name + "?");
                order = Console.ReadLine();
            }
            Tuple<int, int> whereToShoot = CoordinatesFromString(order);
            CellType outcome = Shoot(opponent.map, whereToShoot);
            p.enemyMap.PlotOutcome(whereToShoot, outcome);
            Console.Clear();
            ShowBothMaps(p);
            Console.WriteLine(OutcomeToString(outcome));

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        private static string OutcomeToString(CellType outcome)
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

                x = int.Parse(order[0].ToString());
                y = IntFromLetter(order[1].ToString());
            }

            return new Tuple<int, int>(x, y);
        }

        private static int IntFromLetter(string v)
        {
            switch (v)
            {
                case "a":
                case "A":
                    return 0;
                case "b":
                case "B":
                    return 1;
                case "c":
                case "C":
                    return 2;
                case "d":
                case "D":
                    return 3;
                case "e":
                case "E":
                    return 4;
                case "f":
                case "F":
                    return 5;
                case "g":
                case "G":
                    return 6;
                case "h":
                case "H":
                    return 7;
                case "i":
                case "I":
                    return 8;
                default:
                case "j":
                case "J":
                    return 9;

            }

        }


        private static void ShowBothMaps(Player p)
        {
            Console.WriteLine("Your map");
            ShowMap(p.map);

            Console.WriteLine("Enemy map");
            ShowMap(p.enemyMap);
        }

        private static void ShowMap(Map m)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ABCDEFGHIJ");
            Console.ResetColor();
            for (int i = 0; i < (int)Math.Sqrt(m.grid.Length); i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(i);
                Console.ResetColor();

                for (int j = 0; j < (int)Math.Sqrt(m.grid.Length); j++)
                {
                    switch (m.grid[i, j])
                    {
                        case CellType.Unknown:
                            Console.Write("?");
                            break;
                        case CellType.Water:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("~");
                            Console.ResetColor();
                            break;
                        case CellType.Ship:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("O");
                            Console.ResetColor();
                            break;
                        case CellType.Hit:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            Console.Write("*");
                            Console.ResetColor();
                            break;
                        case CellType.Sunken:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkRed;

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
