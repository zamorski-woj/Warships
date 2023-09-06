using Warships;
using static Warships.NPC;

namespace TestWarships
{
    public class StringFromCoordinatesUnitTest
    {

        [Theory]
        [InlineData(0, 0, "a0")]
        [InlineData(0, 4, "a4")]
        [InlineData(3, 3, "d3")]
        [InlineData(2, 3, "c3")]
        [InlineData(4, 7, "e7")]
        [InlineData(2, 9, "c9")]
        [InlineData(9, 3, "j3")]
        [InlineData(8, 0, "i0")]
        [InlineData(0, 8, "a8")]
        [InlineData(6, 5, "g5")]
        public void StringFromCoordinates_ShouldReturn(int x, int y, string outcome)
        {
            StringFromCoordinates(new Tuple<int, int>(x, y)).Should().Be(outcome);
        }
    }
}
