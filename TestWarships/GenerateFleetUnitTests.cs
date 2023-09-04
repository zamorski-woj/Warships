using Warships;
using static Warships.WarshipsGame;

namespace TestWarships
{
    public class GenerateFleetUnitTests
    {

    
        [Fact]
    public void GenerateFleet_ShouldGiveAsManyShipsAsRequested()
    {
        var players = CreateTwoPlayers();
        List<int> list = new() { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1 };
        players.Item1.GenerateFleet(list);
        players.Item2.GenerateFleet(list);

        players.Item1.fleet.Count.Should().Be(12);
        players.Item2.fleet.Count.Should().Be(12);
    }
}
}
