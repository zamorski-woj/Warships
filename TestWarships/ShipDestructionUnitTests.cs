﻿
using Warships;
using static Warships.WarshipsGame;


namespace TestWarships
{
    public class ShipDestructionUnitTests
    {

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void Destroy_ShouldSinkCells(int x, int y, Direction direction, int length)
        {
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers();
            Tuple<Map, Map> bothMaps = new(bothPlayers.Item1.map, bothPlayers.Item2.map);
            Ship ship = new(length, direction, new(x, y));
            bothMaps.Item1.PlaceShip(ship);

            ship.Destroy(bothMaps.Item1);
            List<Tuple<int, int>> coordinates = ship.GetCoordinates();
            foreach (Tuple<int, int> c in coordinates)
            {
                bothMaps.Item1.grid[c.Item1, c.Item2].Should().Be(CellType.Sunken);
            }
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void ShootingShip_ShouldDestroyIt(int x, int y, Direction direction, int length)
        {
            Tuple<Player, Player> players = CreateTwoPlayers(10);
            Map map = players.Item1.map;
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            map.PlaceShip(ship);

            List<Tuple<int, int>> coordinates = ship.GetCoordinates();
            foreach (Tuple<int, int> whereToShoot in coordinates)
            {
                Shoot(map, whereToShoot).Should().NotBe(CellType.Water);
                Shoot(map, whereToShoot).Should().NotBe(CellType.Unknown);
            }
            foreach (Tuple<int, int> c in coordinates)
            {
                map.grid[c.Item1, c.Item2].Should().Be(CellType.Sunken);
            }
            ship.sunken.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void ShootingShip_ShouldDealDamage(int x, int y, Direction direction, int length)
        {
            Tuple<Player, Player> players = CreateTwoPlayers(10);
            Map map = players.Item1.map;
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            List<Tuple<int, int>> coordinates = ship.GetCoordinates();

            map.PlaceShip(ship);

            foreach (Tuple<int, int> whereToShoot in coordinates)
            {
                if (whereToShoot.Item1 == ship.mainCoordinate.Item1 && whereToShoot.Item2 == ship.mainCoordinate.Item2)
                {
                    continue;//skip not to destroy it completely
                }
                else
                {
                    map.grid[whereToShoot.Item1, whereToShoot.Item2].Should().Be(CellType.Ship);
                    Shoot(map, whereToShoot);
                    map.grid[whereToShoot.Item1, whereToShoot.Item2].Should().Be(CellType.Hit);
                }
            }
            foreach (Tuple<int, int> c in coordinates)
            {
                if (c.Item1 == ship.mainCoordinate.Item1 && c.Item2 == ship.mainCoordinate.Item2)
                {
                    map.grid[c.Item1, c.Item2].Should().Be(CellType.Ship);

                    continue;//
                }
                else
                {
                    map.grid[c.Item1, c.Item2].Should().Be(CellType.Hit);
                }
            }
            ship.sunken.Should().BeFalse();
        }


    }


}

