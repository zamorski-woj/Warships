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

        public static Tuple<Map, Map> CreateMaps(int size, CellType cellType)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("Size should be positive");
            }
            Map firstMap = new(size);
            Map secondMap = new(size);

            for (int i = 0; i < (int)Math.Sqrt(firstMap.grid.Length); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(firstMap.grid.Length); j++)
                {

                    firstMap.grid.SetValue(cellType, i, j);
                    secondMap.grid.SetValue(cellType, i, j);

                }
            }

            return Tuple.Create(firstMap, secondMap);
        }


        public static bool CanPlaceShip(Map map, int xCoordinate, int yCoordinate, Direction direction, int length)
        {
            Ship ship = new(length, direction, new Tuple<int, int>(xCoordinate, yCoordinate));
            return CanPlaceShip(map, ship);
        }

        public static bool CanPlaceShip(Map map, Ship ship)
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
        
        public static bool Occupied(Map map, Tuple<int, int> coord)
        {
            return Occupied(map, coord.Item1, coord.Item2);
        }

        public static bool Occupied(Map map, int xCoordinate, int yCoordinate)
        {
            int mapSize = (int)Math.Sqrt(map.grid.Length);
            if (xCoordinate < 0 || yCoordinate < 0 || xCoordinate >= mapSize || yCoordinate >= mapSize)
            {
                return true;
            }
            else
            {
                if (map.grid[xCoordinate, yCoordinate] != CellType.Water && map.grid[xCoordinate, yCoordinate] != CellType.Unknown)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void PlaceShip(Map map, Ship ship)
        {
            if (CanPlaceShip(map, ship))
            {
                List<Tuple<int, int>> shipCoordinates = ship.GetCoordinates();

                foreach (var coord in shipCoordinates)
                {
                    map.grid[coord.Item1, coord.Item2] = CellType.Ship;
                }

                map.owner.fleet.Add(ship);
            }


        }

        public static CellType Shoot(Map map, Tuple<int, int> c)
        {
            if (map.grid[c.Item1, c.Item2] == CellType.Ship)
            {
                map.grid[c.Item1, c.Item2] = CellType.Hit;
                return CheckIfSunken(map, c);
                
            }
            else
            {
                return CellType.Water;
            }
        }

        private static CellType CheckIfSunken(Map map, Tuple<int, int> where)
        {
            Player mapOwner = map.owner;
            Ship whatGotHit = mapOwner.GetShipFromCoordinates(where);
            if (whatGotHit != null)
            {
                return whatGotHit.CheckIfSunken(map, whatGotHit);
            }
            else return CellType.Water;//Should not happen
        }
    }



}


