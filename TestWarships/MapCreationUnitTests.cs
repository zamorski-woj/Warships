using Warships;
using static Warships.WarshipsGame;


namespace TestWarships
{
    public class MapCreationUnitTests
    {

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(10)]
        public void CreateMaps_ShouldHaveProperDimiensions(int size)
        {
            Tuple<Player, Player > players= CreateTwoPlayers("name", "name", size);
            Tuple<Map, Map>  bothMaps = new(players.Item1.map, players.Item2.map);
            bothMaps.Item1.grid.Length.Should().Be(size * size);
            bothMaps.Item2.grid.Length.Should().Be(size * size);
            bothMaps.Item1.grid.GetLength(0).Should().Be(size);
            bothMaps.Item2.grid.GetLength(0).Should().Be(size);
            bothMaps.Item1.grid.GetLength(1).Should().Be(size);
            bothMaps.Item2.grid.GetLength(1).Should().Be(size);
        } 

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void CreateMaps_ShouldThrowException(int size)
        {
            Action act = () => CreateMaps(size, CellType.Water);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(CellType.Unknown)]
        [InlineData(CellType.Water)]
        public void FillMaps_ShouldBeOfProperContent(CellType cellType)
        {
            Tuple<Map, Map> bothMaps = CreateMaps(4, cellType);
            foreach (CellType cell in bothMaps.Item1.grid)
            {
                cell.Should().NotBe(null);
                cell.Should().NotBe(CellType.Ship);
                cell.Should().NotBe(CellType.Hit);
                cell.Should().NotBe(CellType.Sunken);
                cell.Should().Be(cellType);
            }
        }
    }
}
