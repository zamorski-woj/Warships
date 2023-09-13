using Warships;
using static Warships.WarshipsConsole;

namespace TestWarships
{
    public class RunGameUnitTests
    {
        [Fact]
        public void RunGame_ShouldGiveAWinnerWhenOnFullAutomatic()
        {
            Player p = RunGame(0, false, "Brajan", "Alan");
            p.Should().NotBe(null);
            p.Name.Length.Should().NotBe(0);
            p.Opponent.FleetStillAlive().Should().BeFalse();
            p.FleetStillAlive().Should().BeTrue();
        }
    }
}