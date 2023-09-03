namespace Warships
{
    public class Map
    {
        public CellType[,] grid;
        public Player owner;

        public Map(Player p, int size)
        {
            grid = new CellType[size, size];
            owner = p;
        }
        public Map(int size=10)
        {
            grid = new CellType[size, size];
            owner = new Player("Ziutek");
        }
    }
}