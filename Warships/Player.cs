using static Warships.WarshipsGame;
namespace Warships
{
    public class Player
    {
        public List<Ship> fleet = new();
        public string name;
        public Map map, enemyMap;
        public Player opponent;
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
            list ??= new List<int>() {5,4,4,3,3,3,2,2,2,2,1,1,1,1,1 };

            foreach (int i in list)
            {
                new Ship(map, i);
            }
        }

        public Ship GetShipFromCoordinates(Tuple<int, int> where)
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

        internal void PlayOneTurn(Player opponent)
        {

            Console.Clear();
            Console.WriteLine(this.name + "'s turn. Press any Key");
            Console.ReadKey();
            Console.Clear();
            ShowBothMaps();
            
            string? order =null;
            while (order==null)
            {
                order = Console.ReadLine();
            }
            Tuple<int, int> whereToShoot = CoordinatesFromString(order);
            CellType outcome = Shoot(opponent.map, whereToShoot);
            enemyMap.PlotOutcome(whereToShoot, outcome);
            Console.Clear();
            ShowBothMaps();
            Console.WriteLine(OutcomeToString(outcome));
            Console.WriteLine("Press any key");

            Console.ReadKey();
        }

        private static string OutcomeToString(CellType outcome)
        {
            return outcome switch
            {
                CellType.Hit => "It'a a hit!",
                CellType.Ship => "It'a a hit!",
                CellType.Sunken => "Hit and sunken!",
                CellType.Water => "Missed!",
                _ => "Did you enter right coordinates?",
            };
        }

        private void ShowBothMaps()
        {
            Console.WriteLine("Your map");
            this.map.ShowMap();

            Console.WriteLine("Enemy map");
            this.enemyMap.ShowMap();
        }

        private Tuple<int, int> CoordinatesFromString(string order)
        {
            int x = int.Parse(order[0].ToString());
            int y = int.Parse(order[1].ToString());
            return new Tuple<int, int>(x, y);
        }

        public bool FleetStillAlive()
        {
            foreach(Ship s in fleet)
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
