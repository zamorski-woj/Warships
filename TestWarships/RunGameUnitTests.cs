using Warships;
using static Warships.WarshipsConsole;

namespace TestWarships
{
    public class RunGameUnitTests
    {
        [Fact]
        public  void RunGame_ShouldGiveAWinnerWhenOnFullAutomatic() 
        {
            Player p = RunGame(0, false, "Brajan", "Alan");
            p.Should().NotBe(null);
            p.name.Length.Should().NotBe(0);
            p.opponent.FleetStillAlive().Should().BeFalse();
            p.FleetStillAlive().Should().BeTrue();
        }

    }
}
