using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            PlaceShip(bothMaps.Item1, ship);
            PlaceShip(bothMaps.Item1, ship);
            ship.Destroy(bothMaps.Item1);
            List < Tuple<int, int> > coordinates= ship.GetCoordinates();
            foreach(Tuple<int,int> c in coordinates)
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

            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Water);
            Player player1 = new();

            bothMaps.Item1.owner = player1;
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            PlaceShip(bothMaps.Item1, ship);
            PlaceShip(bothMaps.Item1, ship);

            List<Tuple<int, int>> coordinates = ship.GetCoordinates();
            foreach (Tuple<int, int> c in coordinates)
            {
                Shoot(bothMaps.Item1, c).Should().NotBe(CellType.Water);
                Shoot(bothMaps.Item1, c).Should().NotBe(CellType.Unknown);
            }
            foreach (Tuple<int, int> c in coordinates)
            {
                bothMaps.Item1.grid[c.Item1, c.Item2].Should().Be(CellType.Sunken);
            }
            ship.sunken.Should().BeTrue();
        }

     
    }
}
