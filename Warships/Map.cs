using Microsoft.VisualBasic;

namespace Warships
{
    public class Map
    {
        public CellType[,] grid;
        public Player owner;

        public Map(Player p, int size)
        {
            grid = new CellType[size, size];
            owner = p;
        }
        public Map(int size = 10)
        {
            grid = new CellType[size, size];
            owner = new Player("Ziutek");
        }

        internal void FillWith(CellType cellType)
        {
            for (int i = 0; i < (int)Math.Sqrt(grid.Length); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(grid.Length); j++)
                {
                    grid.SetValue(cellType, i, j);
                }
            }
        }
        public bool IsOnMap(Tuple<int, int> coordinates)
        {
            int mapSize = (int)Math.Sqrt(this.grid.Length);
            int xCoordinate = coordinates.Item1;
            int yCoordinate = coordinates.Item2;

            if (xCoordinate < 0 || yCoordinate < 0 || xCoordinate >= mapSize || yCoordinate >= mapSize)
            {
                return false;
            }
            return true;
        }

        public void PlaceShip(Ship ship)
        {

            if (this.CanPlaceShip(ship))
            {
                List<Tuple<int, int>> shipCoordinates = ship.GetCoordinates();

                foreach (var coord in shipCoordinates)
                {
                    this.grid[coord.Item1, coord.Item2] = CellType.Ship;
                }
                if (this.owner != null)
                {
                    this.owner.fleet.Add(ship);
                }
            }
        }

        public bool CanPlaceShip(int xCoordinate, int yCoordinate, Direction direction, int length)
        {
            Ship ship = new(length, direction, new Tuple<int, int>(xCoordinate, yCoordinate));
            return this.CanPlaceShip(ship);
        }

        public bool CanPlaceShip(Ship ship)
        {

            
            List<Tuple<int, int>> shipCoordinates = ship.GetCoordinates();

            foreach (var coord in shipCoordinates)
            {
                if (this.Occupied(coord))
                {
                    return false;
                }
            }
            return true;
        }


        public bool Occupied(Tuple<int, int> coord)
        {
            return this.Occupied(coord.Item1, coord.Item2);
        }

        public bool Occupied(int xCoordinate, int yCoordinate)
        {
            if (!IsOnMap(new Tuple<int, int>(xCoordinate, yCoordinate)))
            {
                return true;
            }
            else
            {
                if (this.grid[xCoordinate, yCoordinate] != CellType.Water && this.grid[xCoordinate, yCoordinate] != CellType.Unknown)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal void ShowMap()
        {

            Console.WriteLine("  ABCDEFGHIJ");
            Console.WriteLine();
            for (int i = 0; i < (int)Math.Sqrt(grid.Length); i++)
            {
                Console.Write(i+1);
                Console.Write(" ");

                for (int j = 0; j < (int)Math.Sqrt(grid.Length); j++)
                {
                    switch (grid[i, j])
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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


        }

        internal void PlotOutcome(Tuple<int, int> whereToShoot, CellType outcome)
        {
            grid[whereToShoot.Item1, whereToShoot.Item2] = outcome;
        }
    }

}