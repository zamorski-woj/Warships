using System.Collections.Generic;
using static Warships.WarshipsGame;


namespace Warships
{
    public class Ship
    {
        public int length;
        public Direction heading;
        public Tuple<int, int> mainCoordinate;
        public bool sunken = false;

        public Ship(int length, Direction heading, Tuple<int, int> mainCoordinate)//manually
        {
            this.length = length;
            this.heading = heading;
            this.mainCoordinate = mainCoordinate;
        }

        public Ship(Map map, int length)//Randomize
        {
            this.length = length;
            Random random = new();
            int xCoordinate, yCoordinate;
            Direction direction;

            do
            {
                xCoordinate = random.Next(0, (int)Math.Sqrt(map.grid.Length));
                yCoordinate = random.Next(0, (int)Math.Sqrt(map.grid.Length));
                direction = (Direction)random.Next(0, 4);
            }
            while (!map.CanPlaceShip(xCoordinate, yCoordinate, direction, length));

            this.heading = direction;
            this.mainCoordinate = new Tuple<int, int>(xCoordinate, yCoordinate);
            map.PlaceShip(this);
        }

        public List<Tuple<int, int>> GetCoordinates()
        {
            List<Tuple<int, int>> allCoordinates = new();
            int xCoordinate = this.mainCoordinate.Item1, yCoordinate = this.mainCoordinate.Item2;
            switch (this.heading)
            {
                case Direction.North:
                    for (int i = 0; i < this.length; i++)
                    {
                        allCoordinates.Add(new Tuple<int, int>(xCoordinate, yCoordinate + i));

                    }
                    break;
                case Direction.East:
                    for (int i = 0; i < this.length; i++)
                    {
                        allCoordinates.Add(new Tuple<int, int>(xCoordinate+i, yCoordinate));

                    }
                    break;
                case Direction.South:
                    for (int i = 0; i < this.length; i++)
                    {
                        allCoordinates.Add(new Tuple<int, int>(xCoordinate, yCoordinate - i));

                    }
                    break;
                case Direction.West:
                    for (int i = 0; i < this.length; i++)
                    {
                        allCoordinates.Add(new Tuple<int, int>(xCoordinate - i, yCoordinate));

                    }
                    break;
            }
            return allCoordinates;
        }

        public void Destroy(Map map)
        {
            var coordinates = this.GetCoordinates();
            foreach(var coordinate in coordinates)
            {
                map.grid[coordinate.Item1, coordinate.Item2] = CellType.Sunken; 
            }
            this.sunken = true;
        }

        internal bool ExistsHere(Tuple<int, int> where)
        {
            var coordinates = GetCoordinates();
            foreach (var coordinate in coordinates)
            {
                if(coordinate.Item1 == where.Item1 && coordinate.Item2 == where.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        internal CellType CheckIfSunken(Map map)
        {
            var coordinates = GetCoordinates();
            foreach (var coordinate in coordinates)
            {
                if (map.grid[coordinate.Item1, coordinate.Item2]== CellType.Ship)
                {
                    return CellType.Ship;
                }
            }
            this.Destroy(map);
            return CellType.Sunken;

        }
    }
}

