using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster.GUI.Objects
{
    internal class Hole : Items
    {
        public List<Items> itemsList= new List<Items>();
        public Hole(IntPtr _texture, float x, float y, int WSize, int HSize) : base(_texture, x, y, WSize, HSize)
        {

        }
    }
}
