using Warships;
using static Warships.WarshipsGame;

namespace TestWarships
{
    public class GetShipFromCoordinatesUnitTests
    {
        [Fact]
        public void GeShipFromtCoordinates_ShouldGivecorrectAnswers()
        {
            Player p1 = CreateTwoPlayers().Item1;
            Ship ship = new(3, Direction.East, new Tuple<int, int>(0, 4));
            p1.map.PlaceShip(ship);
            ShipInRandomPosition(p1.map, 4);
            ShipInRandomPosition(p1.map, 5);
            ShipInRandomPosition(p1.map, 6);
            ShipInRandomPosition(p1.map, 3);

            List<Tuple<int, int>> coordinates = new() { new Tuple<int, int>(0, 4), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 4) };
           foreach (var coordinate in coordinates)
            {
                Ship ship1 = p1.GetShipFromCoordinates(coordinate);
                ship1.Should().Be(ship);
            }
        }
    }
}
