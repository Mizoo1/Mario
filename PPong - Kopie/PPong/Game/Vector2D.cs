using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster.Game
{
    public class Vector2D
    {
        public float X;
        public float Y;
        public Vector2D()
        {
            this.X = 0;
            this.Y = 0;
        }
        public Vector2D(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public void setX(float x)
        {
            X += x;
        }
        public void setY(float y)
        {
            Y += y;
        }
    }
    
}
