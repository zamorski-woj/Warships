using static Warships.WarshipsGame;
using static Warships.WarshipsConsole;

namespace Warships
{
    public static class NPC
    {

        public static Tuple<int, int> PlayAutomaticTurn(Player player, Player opponent, bool showNPC = true)
        {
            Tuple<int, int> whereToShoot = ChooseWhereToShoot(player.enemyMap);
            CellType outcome = Shoot(opponent.map, whereToShoot);
            player.enemyMap.PlotOutcome(whereToShoot, outcome);
            player.lastMove = whereToShoot;

            if (showNPC)
            {
                Console.Clear();
                Console.WriteLine("Admiral " + player.name + " shoots at " + StringFromCoordinates(whereToShoot));
                Console.WriteLine(OutcomeToString(outcome));
                ShowMap(player.enemyMap, whereToShoot);
                Console.ReadKey();
                Console.Clear();
            }
            return whereToShoot;
        }

        public static string StringFromCoordinates(Tuple<int, int> whereToShoot)
        {
            return whereToShoot.Item1 switch
            {
                0 => "a" + whereToShoot.Item2,
                1 => "b" + whereToShoot.Item2,
                2 => "c" + whereToShoot.Item2,
                3 => "d" + whereToShoot.Item2,
                4 => "e" + whereToShoot.Item2,
                5 => "f" + whereToShoot.Item2,
                6 => "g" + whereToShoot.Item2,
                7 => "h" + whereToShoot.Item2,
                8 => "i" + whereToShoot.Item2,
                _ => "j" + whereToShoot.Item2,
            };
        }

        private static Tuple<int, int> ChooseWhereToShoot(Map enemyMap)
        {
            Random r = new();
            int size = enemyMap.grid.GetLength(0);
            double[,] weight = new double[size, size];

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    weight[i, j] = r.NextDouble();//fill with random small numbers, so they are not equal but negligable
                }
            }
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    switch (enemyMap.grid[i, j])
                    {
                        case CellType.Unknown:
                            weight[i, j] += 1;
                            break;

                        case CellType.Hit:
                            weight[i, j] -= 999;
                            weight = ChangeNeighbouring(weight, i, j, +50);
                            PredictLines(enemyMap, weight, i, j);
                            break;

                        case CellType.Ship://how would he know?
                            weight[i, j] += 999;
                            weight = ChangeNeighbouring(weight, i, j, +50);
                            break;

                        case CellType.Sunken:
                            weight[i, j] -= 999;
                            weight = ChangeNeighbouring(weight, i, j, -5);
                            break;
                        default:
                        case CellType.Water:
                            weight[i, j] -= 999;
                            break;
                    }
                }
            }

            double maxValue = weight.Cast<double>().Max();
            return IndexOf(weight, maxValue);

        }

        private static void PredictLines(Map map, double[,] weight, int x, int y)//we know xy was hit
        {
            int size = map.grid.GetLength(0);
            bool endloop = false;//lets me break out of loop inside the switch. ambigous "break" keyword
            for (int i = x + 1; i < size; i++)
            {
                if (endloop)
                {
                    break;
                }
                switch (map.grid[i, y])
                {
                    case CellType.Hit:
                        break;
                    case CellType.Unknown:
                        weight[i, y] += 100;
                        endloop = true;
                        break;
                    default:
                        endloop = true;
                        break;
                }
            }
            endloop = false;
            for (int i = x - 1; i >= 0; i--)
            {
                if (endloop)
                {
                    break;
                }
                switch (map.grid[i, y])
                {
                    case CellType.Hit:
                        break;
                    case CellType.Unknown:
                        weight[i, y] += 100;
                        endloop = true;
                        break;
                    default:
                        endloop = true;
                        break;
                }
            }
            endloop = false;
            for (int j = y - 1; j >= 0; j--)
            {
                if (endloop)
                {
                    break;
                }
                switch (map.grid[x, j])
                {
                    case CellType.Hit:
                        break;
                    case CellType.Unknown:
                        weight[x, j] += 100;
                        endloop = true;
                        break;
                    default:
                        endloop = true;
                        break;
                }
            }
            endloop = false;
            for (int j = y + 1; j < size; j++)
            {
                if (endloop)
                {
                    break;
                }
                switch (map.grid[x, j])
                {
                    case CellType.Hit:
                        break;
                    case CellType.Unknown:
                        weight[x, j] += 100;
                        endloop = true;
                        break;
                    default:
                        endloop = true;
                        break;
                }

            }
        }

        private static Tuple<int, int> IndexOf(double[,] array, double value)
        {
            int size = array.GetLength(0);

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (array[i, j] == value)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }

            }
            return new Tuple<int, int>(0, 0);
        }

        public static double[,] ChangeNeighbouring(double[,] array, int x, int y, int howMuch)
        {
            if (x + 1 < array.GetLength(0))
            {
                array[x + 1, y] += howMuch;
            }
            if (y + 1 < array.GetLength(1))
            {
                array[x, y + 1] += howMuch;
            }
            if (x - 1 >= 0)
            {
                array[x - 1, y] += howMuch;
            }
            if (y - 1 >= 0)
            {
                array[x, y - 1] += howMuch;
            }
            return array;
        }

    }
}
