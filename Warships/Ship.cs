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

        public Ship(CellType[,] map, int length)//Randomize
        {
            this.length = length;
            Random random = new Random();
            int xCoordinate, yCoordinate;
            Direction direction;

            do
            {
                xCoordinate = random.Next(0, (int)Math.Sqrt(map.Length));
                yCoordinate = random.Next(0, (int)Math.Sqrt(map.Length));
                direction = (Direction)random.Next(0, 4);
            }
            while (!CanPlaceShip(map, xCoordinate, yCoordinate, direction, length));

            this.heading = direction;
            this.mainCoordinate = new Tuple<int, int>(xCoordinate, yCoordinate);
            PlaceShip(map, this);
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
    }
}

