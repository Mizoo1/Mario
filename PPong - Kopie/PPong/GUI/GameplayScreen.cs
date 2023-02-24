using Galaga.GUI;
using Microsoft.VisualBasic;
using PPong.GUI.Sprite;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PPong.GUI.Sprite;
using Monster.GUI;
using static System.Formats.Asn1.AsnWriter;
using Monster.GUI.Sprite;
using Monster;
using Monster.GUI.Objects;

namespace PPong.GUI
{
	public class GameplayScreen
    {
		static int screenWidth = DisplaySetting.Display_Width;
		static int screenHeight = DisplaySetting.Display_Height;

		IntPtr window;
		IntPtr randerer;
		IntPtr[] surface;
        IntPtr backgroundTexture;

        private Player player;
		private Zombie zombie;
		private Robot robot;
        private Laser laser;
        private ItemRandomizer items;
		private SDL.SDL_Rect _rect;
        float deltaTime = 0;
        float lastFrameTime = (float)SDL.SDL_GetTicks();

        IntPtr stoneTexture;
        #region Fields
        private List<PPong.GUI.Sprite.Sprite> _sprites;
        #endregion Fields
        public GameplayScreen(IntPtr window, IntPtr randerer) 
		{
            // Definieren Sie die Position und Größe des Rechtecks
            _rect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = DisplaySetting.Display_Width,
                h = DisplaySetting.Display_Height
            };
            this.window=window;
			this.randerer=randerer;
			surface = new IntPtr[8];
            Initialize();

        }

		public void Initialize()
		{
            items= new ItemRandomizer(randerer);
            for (int i = 0; i<surface.Length;i++)
			{
				switch(i)
				{
					case 0:
                        surface[i] = SDL_image.IMG_Load("Assest\\Zombie.png");
                        IntPtr zombieTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        zombie = new Zombie(zombieTexture);
                        break;
					case 1:
						surface[i] = SDL_image.IMG_Load("Assest\\robot.png");
                        IntPtr robotTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        robot = new Robot(robotTexture);
                        break;
					case 2:
						surface[i] = SDL_image.IMG_Load("Assest\\hero01.png");
                        IntPtr _texture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        player = new Player(_texture , randerer);
                        break;
					case 3:
						surface[i] = SDL_image.IMG_Load("Assest\\forest01.png");
                        backgroundTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
						break;
                    case 4:
                        surface[i] = SDL_image.IMG_Load("Assest\\stone.jpg");
                        stoneTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        break;
                    case 5:
                        surface[i] = SDL_image.IMG_Load("Assest\\bullet.png");
                        IntPtr laserTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        laser = new Laser(laserTexture, player);
                        break;
                    case 6:
                        surface[i] = SDL_image.IMG_Load("Assest\\hole.png");
                        IntPtr holeTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        //hole = new Hole(holeTexture, 495, 540, 50, 50);
                        
                        break;
                    case 7:
                        surface[i] = SDL_image.IMG_Load("Assest\\heal.png");
                        IntPtr healTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        //heal = new Heal(healTexture, 610, 600, 50, 50);
                        break;
                    case 8:
                        surface[i] = SDL_image.IMG_Load("Assest\\heal.png");
                        IntPtr keyTexture = SDL.SDL_CreateTextureFromSurface(randerer, surface[i]);
                        break;

                   
                }

				
				
				
			}
        }
		public void Setup()
		{
			createMap();
         for (int i=0; i<_sprites.Count;i++)
				_sprites[i].LoadContent();
				Update();
				cleanUp();
		}
		public void createMap()
		{
			_sprites = new List<PPong.GUI.Sprite.Sprite>();
			_sprites.Add(player);
			for (int i = 0; i < 27; i++)
			{
				StoneOpject up = new StoneOpject(stoneTexture, 50 * i, 0);
				_sprites.Add(up);
				StoneOpject left = new StoneOpject(stoneTexture, 0, 50 * i);
				_sprites.Add(left);
				StoneOpject right = new StoneOpject(stoneTexture, 1300, 51 * i);
				_sprites.Add(right);
				StoneOpject down = new StoneOpject(stoneTexture, 50 * i, (48 * 15) + 3);
				_sprites.Add(down);
			}
			StoneOpject temp;

			for (int i = 0; i < 22; i += 3)
			{
				for (int j = 0; j < 12; j += 2)
				{
					temp = new StoneOpject(stoneTexture, 55 * i, 60 * j);
                    _sprites.Add(temp);
                }
			}
            items.CreateItem();
        }
		public void LoadContent()
		{
            player.loadContent();
            zombie.loadContent(_sprites);
            robot.loadContent(_sprites);	
            laser.loadContent();
            foreach(PPong.GUI.Sprite.Sprite s in _sprites)
                s.LoadContent();
            items.LoadContent();
        }
        public void HandleInput()
		{
			player.HandleInput(surface[2],window,randerer,laser);
        }	
		public void Update()
		{
            
            

            bool running = true;
			while (running)
			{
                // Berechnen Sie die vergangene Zeit seit dem letzten Frame
                float currentTime = (float)SDL.SDL_GetTicks();
                deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                LoadContent();

                player.Update(_sprites, surface[1], window,randerer,laser, items.item.items);
                zombie.update(_sprites);
                robot.update(_sprites);
                laser.UpdateLaserShots(_sprites, zombie);
                laser.UpdateLaserShots(_sprites, robot);
                
                HandleInput();
				running = player.getRunning();

				Draw();

            }
		}
		public void cleanUp()
		{
			// Räume auf
			SDL.SDL_DestroyRenderer(randerer);
			SDL.SDL_DestroyWindow(randerer);
			SDL.SDL_Quit();
		}
		public void Draw()
		{
            // Aktualisiere Anzeige
            SDL.SDL_RenderPresent(randerer);
            // Kopieren Sie die Texture auf den Renderer an der spezifizierten Position
            SDL.SDL_RenderCopy(randerer, backgroundTexture, IntPtr.Zero, ref _rect);
            player.draw(surface[2], randerer);
			//zombie.draw(surface[0], randerer);
			//robot.draw(surface[1], randerer);
            for (int i = 0; i < _sprites.Count; i++)
                _sprites[i].Draw(surface[1], randerer);
            items.Draw();  
            laser.DrawLaser(surface[5], randerer);

        }
	}
	


}
