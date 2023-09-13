using System.Text.RegularExpressions;

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
        public static Tuple<Player, Player> CreateTwoPlayers(int size = 10)
        {
            return CreateTwoPlayers("player 1", "player 2", size);
        }

        public static Tuple<Player, Player> CreateTwoPlayers(string name1, string name2, int size = 10)
        {
            Player player = new(name1);
            Player player2 = new(name2);
            Tuple<Map, Map> maps = CreateMaps(size, CellType.Water, player, player2);
            Tuple<Map, Map> Enemymaps = CreateMaps(size, CellType.Unknown, player, player2);

            player.map = maps.Item1;
            player2.map = maps.Item2;
            player.enemyMap = Enemymaps.Item1;
            player2.enemyMap = Enemymaps.Item2;
            player.opponent = player2;
            player2.opponent = player;
            return new Tuple<Player, Player>(player, player2);
        }

        public static Tuple<Map, Map> CreateMaps(int size, CellType cellType, Player? player1 = null, Player? player2 = null)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            player1 ??= new Player("player 1");
            player2 ??= new Player("player 2");

            Map firstMap = new(player1, size);
            Map secondMap = new(player2, size);
            firstMap.FillWith(cellType);
            secondMap.FillWith(cellType);
            return Tuple.Create(firstMap, secondMap);
        }

        public static CellType Shoot(Map map, Tuple<int, int> coordinates)
        {
            int x = coordinates.Item1;
            int y = coordinates.Item2;
            if (map.IsOnMap(coordinates))
            {
                if (map.grid[x, y] == CellType.Ship || map.grid[x, y] == CellType.Hit)
                {
                    map.grid[x, y] = CellType.Hit;
                    return CheckIfSunken(map, coordinates);
                }
                if (map.grid[x, y] == CellType.Sunken)
                {
                    return CheckIfSunken(map, coordinates);
                }
                else
                {
                    return CellType.Water;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(coordinates));
            }
        }

        private static CellType CheckIfSunken(Map map, Tuple<int, int> where)
        {
            Player mapOwner = map.owner;
            Ship whatGotHit = mapOwner.GetShipFromCoordinates(where);
            if (whatGotHit != null)
            {
                return whatGotHit.CheckIfSunken(map);
            }
            else throw new Exception("This ship did not belong to any player");
        }

        public static Ship ShipInRandomPosition(Map map, int length)//Randomize
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Random random = new();
            int xCoordinate, yCoordinate;
            Direction direction;

            do
            {
                xCoordinate = random.Next(0, (int)Math.Sqrt(map.grid.Length));
                yCoordinate = random.Next(0, (int)Math.Sqrt(map.grid.Length));
                direction = (Direction)random.Next(0, 4);
            }
            while (!map.CanPlaceShip(xCoordinate, yCoordinate, direction, length));

            Direction heading = direction;
            var mainCoordinate = new Tuple<int, int>(xCoordinate, yCoordinate);
            Ship s = new(length, heading, mainCoordinate);
            map.PlaceShip(s);
            return s;
        }
    }
}