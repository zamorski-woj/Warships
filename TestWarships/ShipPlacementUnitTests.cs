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
            Tuple<CellType[,], CellType[,]> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x,y));
            CanPlaceShip(bothMaps.Item1, ship).Should().BeTrue();
            CanPlaceShip(bothMaps.Item2, ship).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void CanPlaceShip_ShouldReturnFalseWhenOnAnotherShip(int x, int y, Direction direction, int length)
        {
            Tuple<CellType[,], CellType[,]> bothMaps = CreateMaps(5, CellType.Ship);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            CanPlaceShip(bothMaps.Item1, ship).Should().BeFalse();
            CanPlaceShip(bothMaps.Item2, ship).Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void PlacingShipTwice_ShouldBeImpossible(int x, int y, Direction direction, int length)
        {
            Tuple<CellType[,], CellType[,]> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            CanPlaceShip(bothMaps.Item1, ship).Should().BeTrue();
            PlaceShip(bothMaps.Item1, ship);
            CanPlaceShip(bothMaps.Item1, ship).Should().BeFalse();
        }  

        [Theory]
        [InlineData(0, 0, Direction.South, 2)]
        [InlineData(0, 4, Direction.East, 7)]
        [InlineData(11, 3, Direction.North, 1)]
        [InlineData(2, 9, Direction.West, 3)]
        public void CanPlaceShip_ShouldReturnFalseWhenOutsideMap(int x, int y, Direction direction, int length)
        {
            Tuple<CellType[,], CellType[,]> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));

            CanPlaceShip(bothMaps.Item1, ship).Should().BeFalse();
            CanPlaceShip(bothMaps.Item2, ship).Should().BeFalse();
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void CanPlaceShip_ShouldThrowException(int length)
        {
            Tuple<CellType[,], CellType[,]> bothMaps = CreateMaps(5, CellType.Water);
            Ship ship = new(length, Direction.North, new Tuple<int, int>(0, 0));

            Action act = () => CanPlaceShip(bothMaps.Item1, ship).Should().BeTrue();
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

    }
}
