using Galaga.GUI;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Monster.Font;
using static SDL2.SDL;
using Monster;
using Monster.GUI.Objects;
using System.Reflection.Emit;
using Monster.GUI.Sprite;

namespace PPong.GUI.Sprite
{
	public class Player : Sprite
	{
        private List<Enemy> enemy;
        private IntPtr HpTexture;
        private IntPtr ManaTexture;
        private Font einFont;
        private float Radius;
        public static int Mana;
        public static int counter;
        public int playerScore;
        private char Dirction;
        public bool hasKey = false;
        public bool doorOpened = false;

        IntPtr surface1 = SDL_image.IMG_Load("Assest\\hero01.png");
        IntPtr _texture1;
        public static bool running = true;
		public SDL.SDL_Rect smallSprite;
		public float speed;
		public Player(IntPtr _texture,IntPtr _renderer) : base(_texture,300, 420,50,50)
		{
			speed = 15;
            _texture1 = SDL.SDL_CreateTextureFromSurface(_renderer, surface1);

        }
		public void loadContent()
		{
			LoadContent();
			// Definieren Sie die Position und Größe der beiden Rechteckobjekte
			smallSprite = new SDL.SDL_Rect()
			{
				x = 10,
				y = 450,
				w = 20,
				h = 20
			};
		}
		public void draw(IntPtr surface, IntPtr renderer)
		{
			// Erstellen Sie eine Texture aus der Surface
			
			Draw(surface, renderer);
			SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite);
		}

		public void Update(List<Sprite>sprites, IntPtr surface, IntPtr _window, IntPtr _renderer, Laser laser, List<Items> item)
		{
            HandleInput(surface1, _window, _renderer, laser);
            bool manaReduced = false;
            foreach (var sprite in sprites)
            {

                if (sprite == this)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite))||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))
				{
					
                    Velocity.X = 0;
					Console.WriteLine("Bingo");
                }  
                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite))||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
				{
                    
                    Velocity.Y = 0;
                    Console.WriteLine("Bingo");
                }
            }
			int counter = 0;
            while (counter<item.Count)
            {
                
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(item[counter]))||
                    (this.Velocity.X < 0 & this.IsTouchingRight(item[counter]))||
					(this.Velocity.Y > 0 && this.IsTouchingTop(item[counter]))||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(item[counter])))
                {
                    if (item[counter].GetType() == typeof(Hole))
                    {
                        item.Remove(item[counter]);
                    }


                    
                }
				counter++;
            }
           

            Y += Velocity.Y;
            X += Velocity.X;
            Velocity.Y = 0;
            Velocity.X = 0;
        }
		public bool getRunning() { return running; }
		public SDL.SDL_Rect getSmallSprite() { return smallSprite; }
        public void MoveUp()
        {
            Velocity.Y = -speed;
			Dirction = 'u';
        }
        public void MoveDown()
        {
           Velocity.Y = speed;
           Dirction = 'd';
        }
        public void MoveLeft()
        {
            Velocity.X = -speed;
            Dirction = 'l';
        }
        public void MoveRight()
        {
            Velocity.X = speed;
            Dirction = 'r';
        }
		public char getDirction()
		{
			return Dirction;
		}
       
		public IntPtr HandleInput( IntPtr surface, IntPtr _window, IntPtr _renderer,Laser laser)
		{
			
			// Variablen für die Tasten
			bool upPressed = false;
			bool downPressed = false;
			bool leftPressed = false;
			bool rightPressed = false;

			// Verarbeite Ereignisse
			SDL.SDL_Event e;
			while (SDL.SDL_PollEvent(out e) != 0)
			{
				if (e.type == SDL.SDL_EventType.SDL_QUIT)
				{
					running = false;
				}
				if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
				{
					// Quit the game
					SDL.SDL_Quit();
					Environment.Exit(0);
				}
				// Check if the player pressed the Esc key
				if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
				{
					// Transition to the main menu state
					GameState.SetState(new MainMenu(_window, _renderer));
				}
				// Check if the player pressed the f key
				if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_f)
				{
					// Toggle fullscreen mode
					if ((SDL.SDL_GetWindowFlags(_window) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) == 0)
					{
						SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
					}
					else
					{
						SDL.SDL_SetWindowFullscreen(_window, 0);
					}
				}
				else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
				{
					
					switch (e.key.keysym.sym)
					{
						case SDL.SDL_Keycode.SDLK_UP:
							upPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_DOWN:
							downPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_LEFT:
							leftPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_RIGHT:
							rightPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_PLUS:
							speed++; // Erhöhe Geschwindigkeit um 1
							break;
						case SDL.SDL_Keycode.SDLK_MINUS:
							speed--; // Verringere Geschwindigkeit um 1
							break;
						case SDL_Keycode.SDLK_SPACE:
							laser.FireLaser();
							break;

                    }
				}
				else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
				{
					switch (e.key.keysym.sym)
					{
						case SDL.SDL_Keycode.SDLK_UP:
							upPressed = true;
							Console.WriteLine(rightPressed);
							break;
						case SDL.SDL_Keycode.SDLK_DOWN:
							downPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_LEFT:
							leftPressed = true;
							break;
						case SDL.SDL_Keycode.SDLK_RIGHT:
							rightPressed = true;
							break;
					}
				}
			}
			// Bewegung des Rechtecks
			if (upPressed && !downPressed)
			{
				MoveUp(); // Erhöhe Y-Koordinate um speed
				if(Dirction == 'r')
				{
					_texture =_texture1;
				}else if(Dirction == 'l')
				{

                    _texture = _texture1;
                }
				
            }
			if (downPressed && !upPressed)
			{
				MoveDown(); // Verringere Y-Koordinate um speed


            }
			if (leftPressed && !rightPressed)
			{
				MoveLeft();// Verringere X-Koordinate um speed


            }
			if (rightPressed && !leftPressed)
			{
				MoveRight();
            }
			
			return surface;
		}
	}
}
