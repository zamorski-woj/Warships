using static Warships.WarshipsGame;

namespace Warships
{
    public class Player
    {
        public List<Ship> Fleet { get; set; } = new();
        public string Name { get; set; }
        public Map Map { get; set; }
        public Map EnemyMap { get; set; }
        public Player Opponent { get; set; }
        public Tuple<int, int>? LastMove { get; set; }

        public Player()
        {
            this.Name = "Ziutek";
        }

        public Player(string v)
        {
            this.Name = v;
        }

        public void GenerateFleet(List<int>? list = null)
        {
            if (list == null || list.Count < 1)
            {
                list = new List<int>() { 5, 4, 4, 3, 3, 3, 2, 2 };
            }

            foreach (int i in list)
            {
                ShipInRandomPosition(Map, i);
            }
        }

        public Ship? GetShipFromCoordinates(Tuple<int, int> where)
        {
            foreach (var ship in Fleet)
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
            foreach (Ship s in Fleet)
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
