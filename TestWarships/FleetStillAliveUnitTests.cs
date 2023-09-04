using Warships;
using static Warships.WarshipsGame;


namespace TestWarships
{
    public class FleetStillAliveUnitTests
    {

        
            [Fact]
            public void FleetStillAlive_ShouldReturnFalseWhenEmpty()
            {
            var players = CreateTwoPlayers();
            players.Item1.FleetStillAlive().Should().BeFalse();
            players.Item2.FleetStillAlive().Should().BeFalse();
             }

        [Theory]
        [InlineData(0, 0, Direction.South, 1)]
        [InlineData(0, 4, Direction.East, 3)]
        [InlineData(3, 3, Direction.North, 2)]
        [InlineData(2, 3, Direction.West, 2)]
        public void FleetStillAlive_ShouldReturnTrueWhenAtLeastOneShip(int x, int y, Direction direction, int length)
        {
            var players = CreateTwoPlayers();
            Map map = players.Item1.map;
            Ship ship = new(length, direction, new Tuple<int, int>(x, y));
            map.PlaceShip(ship);
            players.Item1.FleetStillAlive().Should().BeTrue();
            players.Item2.FleetStillAlive().Should().BeFalse();
            players.Item2.map.PlaceShip(ship);
            players.Item2.FleetStillAlive().Should().BeTrue();

        }
    }

}

