﻿namespace Warships
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
            Player player = new (name1);
            Player player2 = new(name2);
            Tuple<Map, Map> maps = CreateMaps(size, CellType.Water, player, player2);
            Tuple<Map, Map> Enemymaps = CreateMaps(size, CellType.Water, player, player2);

            player.map = maps.Item1;
            player2.map = maps.Item2;
            player.enemyMap = Enemymaps.Item1;
            player2.enemyMap = Enemymaps.Item2;
            return new Tuple<Player, Player>(player, player2);
        }

        public static Tuple<Map, Map> CreateMaps(int size, CellType cellType, Player player1=null, Player player2= null)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if(player1==null && player2==null)//case only for testing purposes
            {
                player1 = new Player("player 1");
                player2 = new Player("player 2");
            }
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
                if (map.grid[x,y] == CellType.Ship)
                {
                    map.grid[x,y] = CellType.Hit;
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
            else return CellType.Water;//Should not happen
        }
    }



}


