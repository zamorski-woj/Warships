namespace Warships
{
    public class Map
    {
        public CellType[,] Grid { get; set; }
        public Player Owner { get; set; }

        public Map(Player p, int size)
        {
            Grid = new CellType[size, size];
            Owner = p;
        }
        public Map(int size = 10)
        {
            Grid = new CellType[size, size];
            Owner = new Player("Ziutek");
        }

        public void FillWith(CellType cellType)
        {
            for (int i = 0; i < (int)Math.Sqrt(Grid.Length); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(Grid.Length); j++)
                {
                    Grid.SetValue(cellType, i, j);
                }
            }
        }
        public bool IsOnMap(Tuple<int, int> coordinates)
        {
            int mapSize = (int)Math.Sqrt(this.Grid.Length);
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
                    this.Grid[coord.Item1, coord.Item2] = CellType.Ship;
                }
                this.Owner?.Fleet.Add(ship);
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
                return (Grid[xCoordinate, yCoordinate] != CellType.Water && Grid[xCoordinate, yCoordinate] != CellType.Unknown);
            }
        }


        internal void PlotOutcome(Tuple<int, int> whereToShoot, CellType outcome)
        {
            Grid[whereToShoot.Item1, whereToShoot.Item2] = outcome;
        }
    }

}