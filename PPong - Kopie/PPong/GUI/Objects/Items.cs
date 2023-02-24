using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPong.GUI.Sprite;

namespace Monster.GUI.Objects
{
    public  class Items : PPong.GUI.Sprite.Sprite
    {
        public List<Items>items= new List<Items>();
        public IntPtr surface;
        public Items() : base(IntPtr.Zero, 0, 0, 0, 0)
        {

        }

        public Items(IntPtr _texture, float x, float y, int WSize, int HSize) : base(_texture, x, y, WSize, HSize)
        {
        }

        internal bool IsCollidingWith(PPong.GUI.Sprite.Sprite sprite)
        {
            bool ok = false;
            if (sprite == this)
                ok = false;
            if ((Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                (Velocity.X < 0 & this.IsTouchingRight(sprite)))
                ok = true;

            if ((Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                (Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                ok = true;
            return ok;
        }
    }
}
