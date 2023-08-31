namespace Warships
{

    public enum CellType
    {
        Unknown,
        Water,
        Ship,
        Hit,
        Sunken
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class WarshipsGame
    {

        public static Tuple<CellType[,], CellType[,]> CreateMaps(int size, CellType cellType)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("Size should be positive");
            }
            CellType[,] firstMap = new CellType[size, size];
            CellType[,] secondMap = new CellType[size, size];
            for (int i = 0; i < (int)Math.Sqrt(firstMap.Length); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(firstMap.Length); j++)
                {

                    firstMap.SetValue(cellType, i, j);
                    secondMap.SetValue(cellType, i, j);

                }

            }

            return Tuple.Create(firstMap, secondMap);
        }


        public static bool CanPlaceShip(CellType[,] map, int xCoordinate, int yCoordinate, Direction direction, int length)
        {
            Ship ship = new(length, direction, new Tuple<int, int>(xCoordinate, yCoordinate));
            return CanPlaceShip(map, ship);
        }

        public static bool CanPlaceShip(CellType[,] map, Ship ship)
        {

            if (ship.length < 1)
            {
                throw new ArgumentOutOfRangeException("Lenght shoud be positive");
            }
            List<Tuple<int, int>> shipCoordinates = ship.GetCoordinates();

            foreach (var coord in shipCoordinates)
            {
                if (Occupied(map, coord))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool Occupied(CellType[,] map, Tuple<int, int> coord)
        {
            return Occupied(map, coord.Item1, coord.Item2);
        }

        private static bool Occupied(CellType[,] map, int xCoordinate, int yCoordinate)
        {
            int mapSize = (int)Math.Sqrt(map.Length);
            if (xCoordinate < 0 || yCoordinate < 0 || xCoordinate >= mapSize || yCoordinate >= mapSize)
            {
                return true;
            }
            else
            {
                if (map[xCoordinate, yCoordinate] != CellType.Water && map[xCoordinate, yCoordinate] != CellType.Unknown)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void PlaceShip(CellType[,] map, Ship ship)
        {
            if (CanPlaceShip(map, ship))
            {
                List<Tuple<int, int>> shipCoordinates = ship.GetCoordinates();

                foreach (var coord in shipCoordinates)
                {
                    map[coord.Item1, coord.Item2] = CellType.Ship;
                }
            }

        }

    }



}


