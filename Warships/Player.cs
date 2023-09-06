using System.Text.RegularExpressions;
using static Warships.WarshipsGame;
namespace Warships
{
    public class Player
    {
        public List<Ship> fleet = new();
        public string name;
        public Map map, enemyMap;
        public Player opponent;
        internal Tuple<int, int>? lastMove;

        public Player()
        {
            this.name = "Ziutek";
        }

        public Player(string v)
        {
            this.name = v;
        }

        public void GenerateFleet(List<int>? list = null)
        {
            if(list == null || list.Count<1)
            {
                list = new List<int>() { 5, 4, 3, 3, 3, 2, 2, 2 };
            }

            foreach (int i in list)
            {
                ShipInRandomPosition(map, i);
            }
        }

        public Ship? GetShipFromCoordinates(Tuple<int, int> where)
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



        public bool FleetStillAlive()
        {
            foreach (Ship s in fleet)
            {
                if (s.sunken == false)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
