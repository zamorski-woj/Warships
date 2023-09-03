namespace Warships
{
    public class Player
    {
        public List<Ship> fleet;
        public string name;
        public Map map, enemyMap;

        public Player()
        {
            this.name = "Ziutek";
        }

        public Player(string v)
        {
            this.name = v;
        }

        internal Ship GetShipFromCoordinates(Tuple<int, int> where)
        {
            foreach (var ship in fleet)
            {
                if (ship.ExistsHere(where))
                {
                    return ship;
                }
               
            }
            return null;
        }
    }
}
