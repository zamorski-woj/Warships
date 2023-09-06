using Warships;
using static Warships.NPC;

namespace TestWarships
{
    public class ChangeNeighbouringUnitTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 5)]
        [InlineData(4, 3, -1000)]
        [InlineData(7, 6, 345)]
        [InlineData(8, 4, 1.92348)]
        [InlineData(8, 8, 5.999)]
        [InlineData(8, 6, 0.52341)]
        [InlineData(8, 2, 0.0001)]
        public void ChangeNeighbouring_ShouldChange4Surrounding(int x, int y, int howMuch)
        {
            double[,] array = new double[10,10];
            array= ChangeNeighbouring(array, x, y, howMuch);
            array[x, y].Should().Be(0);
            array[x + 1, y + 1].Should().Be(0);//diagonals are not changed
            array[x - 1, y - 1].Should().Be(0);
            array[x - 1, y + 1].Should().Be(0);
            array[x + 1, y - 1].Should().Be(0);

            array[x - 1, y].Should().Be(howMuch);
            array[x + 1, y].Should().Be(howMuch);
            array[x, y-1].Should().Be(howMuch);
            array[x, y+1].Should().Be(howMuch);
        }
    }
}
