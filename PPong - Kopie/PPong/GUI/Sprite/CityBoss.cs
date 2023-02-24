using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster.Font;
using System.Collections.Generic;
using SDL2;

namespace Monster.GUI.Sprite
{
    public class CityBoss
    {
        private IntPtr HpTexture;
        private int damage;
        //private Font einFont;
        private bool dead = false;
        private int directionX;
        private int directionY;
        double timer = 0;
        double interval = 1;
    }
}
