using Warships;
using static Warships.WarshipsGame;

namespace TestWarships
{
    public class GetCoordinatesUnitTests
    {
        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void GetCoordinates_ShouldGiveAllPositions(int x, int y, Direction direction, int length)
        {
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            List<Tuple<int, int>> coordinates =  ship.GetCoordinates();
            coordinates.Count.Should().Be(length);
        }

        [Fact]
        public void GetCoordinates_ShouldGivecorrectAnswers()
        {
            Ship ship = new(3, Direction.East, new Tuple<int, int>(0, 4));
            List<Tuple<int, int>> coordinates = ship.GetCoordinates();
            coordinates[0].Should().Be(new Tuple<int, int>(0, 4));
            coordinates[1].Should().Be(new Tuple<int, int>(1, 4));
            coordinates[2].Should().Be(new Tuple<int, int>(2, 4));
        }

    }
}
