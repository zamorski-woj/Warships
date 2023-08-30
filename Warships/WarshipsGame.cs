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
            int xCoordinate = ship.mainCoordinate.Item1;
            int yCoordinate = ship.mainCoordinate.Item2;

            if (ship.length < 1)
            {
                return false;
            }
            switch (ship.heading)
            {
                case Direction.North:
                    for (int i = 0; i < ship.length; i++)
                    {
                        if (Occupied(map, xCoordinate, yCoordinate + i))
                        {
                            return false;
                        }
                    }

                    break;
                case Direction.East:
                    for (int i = 0; i < ship.length; i++)
                    {
                        if (Occupied(map, xCoordinate + i, yCoordinate))
                        {
                            return false;
                        }
                    }

                    break;
                case Direction.South:
                    for (int i = 0; i < ship.length; i++)
                    {
                        if (Occupied(map, xCoordinate, yCoordinate - i))
                        {
                            return false;
                        }
                    }

                    break;
                case Direction.West:
                    for (int i = 0; i < ship.length; i++)
                    {
                        if (Occupied(map, xCoordinate - i, yCoordinate))
                        {
                            return false;
                        }
                    }

                    break;
            }

            return true;
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
            if(CanPlaceShip(map, ship))
            {

            }

            throw new NotImplementedException();
        }

    }



}


