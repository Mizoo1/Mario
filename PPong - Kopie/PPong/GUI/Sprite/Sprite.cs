using Monster.Game;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PPong.GUI.Sprite
{
	public class Sprite
	{
		public Vector2D Velocity;
        public Vector2 Position;
        public int WSize; public int HSize;
        public int Left; public int Right; public int Top; public int Bottom;

        public SDL.SDL_Rect sprite;
		public IntPtr _texture;
		public float X;
		public float Y;
		public Sprite(IntPtr _texture, float x, float y, int WSize, int HSize)
		{

			this.WSize= WSize;
			this.HSize= HSize;
			this.X = x;
			this.Y = y;
			this._texture = _texture;
			Velocity = new Vector2D();

        }
        public void setTexture(IntPtr texture)
        {
            _texture= texture;
        }
		public void LoadContent()
		{
			sprite = new SDL.SDL_Rect()
			{
				x = (int)X,
				y = (int)Y,
				w = WSize,
				h = HSize
			};
            Left = (int)X;
            Right = (int)X + sprite.w;
            Top = (int)Y;
            Bottom = (int)Y + sprite.h;
        }
		public void Draw(IntPtr surface, IntPtr renderer)
		{
			// Erstellen Sie eine Texture aus der Surface
			SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref sprite); ;
		}
	
		public SDL.SDL_Rect getSprite() { return sprite; }

        #region Collisoin
        public bool IsTouchingLeft(Sprite sprite)
        {
            return Right + Velocity.X > sprite.Left &&
                Left < sprite.Left &&
                Bottom > sprite.Top &&
                Top < sprite.Bottom;
        }
        public bool IsTouchingRight(Sprite sprite)
        {
            return Left + Velocity.X < sprite.Right &&
              Right > sprite.Right &&
              Bottom > sprite.Top &&
              Top < sprite.Bottom;
        }
        public bool IsTouchingTop(Sprite sprite)
        {
            return Bottom + Velocity.Y > sprite.Top &&
              Top < sprite.Top &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }
        public bool IsTouchingBottom(Sprite sprite)
        {
            return Top + Velocity.Y < sprite.Bottom &&
              Bottom > sprite.Bottom &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }
        #endregion

    }
}
