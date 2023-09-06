using Warships;
using static Warships.WarshipsGame;
using static Warships.WarshipsConsole;
using static Warships.NPC;
using static Warships.Map;
using static Warships.Player;
using Microsoft.VisualBasic;
using Xunit.Sdk;

namespace TestWarships
{
    public class NPCIntegrationTests
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        [InlineData(2, 9)]
        [InlineData(9, 3)]
        [InlineData(8, 0)]
        public void NPC_ShouldAlwaysTargetAKnownShip(int x, int y)
        {
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers();
            Player p = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            Ship ship = new(1, Direction.North, new(x, y));
            p.enemyMap.PlaceShip(ship);
            Tuple<int, int> move = PlayAutomaticTurn(p, p2, false);
            move.Item1.Should().Be(x);
            move.Item2.Should().Be(y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(9, 3)]
        [InlineData(8, 0)]
        [InlineData(0, 8)]
        [InlineData(6, 5)]
        public void NPC_ShouldPreferUnknownToSunken(int x, int y)
        {
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers();
            Player p = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            p.enemyMap.FillWith(CellType.Sunken);
            p.enemyMap.grid[x, y] = CellType.Unknown;
            Tuple<int, int> move = PlayAutomaticTurn(p, p2, false);
            move.Item1.Should().Be(x);
            move.Item2.Should().Be(y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(9, 3)]
        [InlineData(8, 0)]
        [InlineData(0, 8)]
        [InlineData(6, 5)]
        public void NPC_ShouldPreferUnknownToHit(int x, int y)
        {
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers();
            Player p = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            p.enemyMap.FillWith(CellType.Hit);
            p.enemyMap.grid[x, y] = CellType.Unknown;
            Tuple<int, int> move = PlayAutomaticTurn(p, p2, false);
            move.Item1.Should().Be(x);
            move.Item2.Should().Be(y);
        }

        [Theory]
        [InlineData(3, 3)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        [InlineData(2, 9)]
        [InlineData(6, 3)]
        [InlineData(8, 1)]
        public void NPC_ShouldTarget4CellsAroundAHit(int x, int y)
        {
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers();
            Player p = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;

            p.enemyMap.grid[x, y] = CellType.Hit;
            List<Tuple<int, int>> moves = new()
            {
                PlayAutomaticTurn(p, p2, false),
                PlayAutomaticTurn(p, p2, false),
                PlayAutomaticTurn(p, p2, false),
                PlayAutomaticTurn(p, p2, false)
            };
            foreach (Tuple<int, int> move in moves)
            {
                if(move.Item1 ==x)//same row
                {
                    Math.Abs(move.Item2 - y).Should().Be(1);
                }
                if (move.Item2 == y)//same column
                {
                    Math.Abs(move.Item1 - x).Should().Be(1);
                }
            }

        }
    }
}
