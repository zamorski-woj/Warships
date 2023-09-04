using Warships;
using static Warships.WarshipsGame;


namespace TestWarships
{
    public class ShipPlacementUnitTests
    {

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void CanPlaceShip_ShouldReturnTrue(int x, int y, Direction direction, int length)
        {
            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x,y));
            bothMaps.Item1.CanPlaceShip(ship).Should().BeTrue();
            bothMaps.Item2.CanPlaceShip(ship).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void CanPlaceShip_ShouldReturnFalseWhenOnAnotherShip(int x, int y, Direction direction, int length)
        {
            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Ship);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            bothMaps.Item1.CanPlaceShip(ship).Should().BeFalse();
            bothMaps.Item2.CanPlaceShip(ship).Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void PlacingShipTwice_ShouldBeImpossible(int x, int y, Direction direction, int length)
        {
            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            bothMaps.Item1.CanPlaceShip( ship).Should().BeTrue();
            bothMaps.Item1.PlaceShip(ship);
            bothMaps.Item1.CanPlaceShip(ship).Should().BeFalse();
        }  

        [Theory]
        [InlineData(0, 0, Direction.South, 2)]
        [InlineData(0, 4, Direction.East, 7)]
        [InlineData(11, 3, Direction.North, 1)]
        [InlineData(2, 9, Direction.West, 3)]
        public void CanPlaceShip_ShouldReturnFalseWhenOutsideMap(int x, int y, Direction direction, int length)
        {
            Tuple<Map, Map> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));

            bothMaps.Item1.CanPlaceShip(ship).Should().BeFalse();
            bothMaps.Item2.CanPlaceShip(ship).Should().BeFalse();
        }

      

    }
}
