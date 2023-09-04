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

        internal void PlayOneTurn(Player opponent)
        {

            Console.Clear();
            Console.WriteLine(this.name + "'s turn. Press any Key");
            Console.ReadKey();
            
            string? order =null;
            while (order==null || NotValidOrder(order))
            {
                Console.Clear();
                ShowBothMaps();
                Console.WriteLine("Your orders, Admiral " + name + "?");
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

        private static bool NotValidOrder(string order)
        {
            if(Regex.IsMatch(order, @"^[a-jA-J][0-9]$") || Regex.IsMatch(order, @"^[0-9][a-jA-J]$"))
            {
                return false;
            }
            else
            {
                return true;
            }
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
            int x=0, y=0;
            if (Regex.IsMatch(order, @"^[a-jA-J][0-9]$"))
            {

                 x= IntFromLetter(order[0].ToString());
                 y = int.Parse(order[1].ToString());
            }
            if (Regex.IsMatch(order, @"^[0-9][a-jA-J]$"))
            {

                 x = int.Parse(order[0].ToString());
                 y = IntFromLetter(order[1].ToString());
            }

            return new Tuple<int, int>(x, y);
        }

        private static int IntFromLetter(string v)
        {
            switch(v)
            {
                case "a":
                case "A":
                    return 0;
                case "b":
                case "B":
                    return 1;
                case "c":
                case "C":
                    return 2;
                case "d":
                case "D":
                    return 3;
                case "e":
                case "E":
                    return 4;
                case "f":
                case "F":
                    return 5;
                    case "g":
                case "G":
                    return 6;
                    case "h":
                    case "H":
                    return 7;
                    case "i":
                case "I":
                    return 8;
                default:
                case "j":
                case "J":
                    return 9;

            }

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
