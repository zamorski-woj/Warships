using Warships;
using static Warships.WarshipsGame;

namespace TestWarships
{
    public class PlayerCreationUnitTests
    {
        [Fact]
        public void CreateTwoPlayers_ShouldHaveProperOwnersOfMaps()
        {
            Tuple<Player, Player> players = CreateTwoPlayers(10);
            Map map = players.Item1.map;
            map.owner.Should().Be(players.Item1);
            Map map2 = players.Item2.map;
            map2.owner.Should().Be(players.Item2);
        }
    }
}

