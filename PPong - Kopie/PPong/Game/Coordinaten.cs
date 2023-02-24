using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster.Game
{
    public class Coordinaten
    {
        private int X;
        private int Y;
        public List<Coordinaten> Coor;
        public Coordinaten(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Coordinaten()
        {
            Coor = SetupArray();
        }
        public List<Coordinaten> SetupArray()
        {
            List<Coordinaten> coordinaten = new List<Coordinaten>();
            for (int i = 1; i < 16; i += 2)
            {
                for (int j = 1; j < 14; j += 2)
                {
                    coordinaten.Add(new Coordinaten(81 * i, 60 * j));
                }
            }
            return coordinaten;
        }
        public int getX() { return this.X; }
        public int getY() { return this.Y; }
        public List<Coordinaten> GetCoordinatens()
        {
            return Coor;
        }
    }
}
