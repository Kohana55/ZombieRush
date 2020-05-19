using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    public class Buildings
    {
        public List<Coordinates> northBuilding = new List<Coordinates>();
        public List<Coordinates> eastBuilding = new List<Coordinates>();
        public List<Coordinates> southBuilding = new List<Coordinates>();
        public List<Coordinates> westBuilding = new List<Coordinates>();


        public Buildings()
        {
            //  Shouth 1 Layout
            //
            //  X X X X X
            //  X       X
            //  X       X
            //  X X   X X
            //    ^
            // Start Building from here

            // Add each wall segment
            southBuilding.Add(new Coordinates(3, 1));
            southBuilding.Add(new Coordinates(3, 0));
            southBuilding.Add(new Coordinates(2, 0));
            southBuilding.Add(new Coordinates(1, 0));
            southBuilding.Add(new Coordinates(0, 0));
            southBuilding.Add(new Coordinates(0, 1));
            southBuilding.Add(new Coordinates(0, 2));
            southBuilding.Add(new Coordinates(0, 3));
            southBuilding.Add(new Coordinates(0, 4));
            southBuilding.Add(new Coordinates(1, 4));
            southBuilding.Add(new Coordinates(2, 4));
            southBuilding.Add(new Coordinates(3, 4));
            southBuilding.Add(new Coordinates(3, 3));
        }

    }
}
