using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster.Game;

namespace PPong.GUI.Sprite
{
	public class Enemy : Sprite
	{
		public List<Enemy> Enemies;
        public Coordinaten coor;
        public List<Coordinaten> Coordinaten;
		public char richtung;
        public float speed;
        public float enemyRadius;

        public Enemy(IntPtr _texture,int x, int y, int WSize, int HSize) : base(_texture,x, y, WSize, HSize)
		{
			Enemies= new List<Enemy>();
		}
        internal bool IsCollidingWith(Sprite sprite)
        {
            bool ok = false;
            if (sprite == this)
                ok = false;
            if ((this.IsTouchingLeft(sprite)) ||
                (this.IsTouchingRight(sprite)))
                ok = true;

            if (( this.IsTouchingTop(sprite)) ||
                ( this.IsTouchingBottom(sprite)))
                ok = true;
            return ok;
        }
        public float getEnemyRadius()
        {
            return enemyRadius;
        }
        #region Collisoin
        public bool IsAnyLeft(Sprite sprite)
        {
            return Right  == sprite.Left &&
                Left == sprite.Left &&
                Bottom == sprite.Top &&
                Top == sprite.Bottom;
        }
        public bool IsAnyRight(Sprite sprite)
        {
            return Left  == sprite.Right &&
              Right == sprite.Right &&
              Bottom == sprite.Top &&
              Top == sprite.Bottom;
        }
        public bool IsAnyTop(Sprite sprite)
        {
            return Bottom == sprite.Top &&
              Top == sprite.Top &&
              Right == sprite.Left &&
              Left == sprite.Right;
        }
        public bool IsAnyBottom(Sprite sprite)
        {
            return Top == sprite.Bottom &&
              Bottom == sprite.Bottom &&
              Right == sprite.Left &&
              Left == sprite.Right;
        }
        #endregion
    }

}
