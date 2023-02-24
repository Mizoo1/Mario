using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPong.GUI.Sprite;

namespace Monster.GUI
{
    public class StoneOpject : PPong.GUI.Sprite.Sprite
    {
        public StoneOpject(IntPtr _texture,int x, int y) : base(_texture, x, y, 50, 50)
        {

        }
    }
}
