using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPong.GUI
{
	public class Window
	{
		private IntPtr window;
		private IntPtr renderer;

		public Window()
		{
		}
		public IntPtr getWindow(){ return window;}
		public IntPtr getRenderer() { return renderer;}
		public void Setup(int screenWidth, int screenHeight)
		{
			// Erstelle Fenster
			window = SDL.SDL_CreateWindow("SDL Example", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
				screenWidth, screenHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
			if (window == IntPtr.Zero)
			{
				Console.WriteLine("SDL_CreateWindow Error: " + SDL.SDL_GetError());
				SDL.SDL_Quit();
				return;
			}

			// Erstelle Renderer
			renderer = SDL.SDL_CreateRenderer(window, -1,
				SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
			if (renderer == IntPtr.Zero)
			{
				Console.WriteLine("SDL_CreateRenderer Error: " + SDL.SDL_GetError());
				SDL.SDL_DestroyWindow(window);
				SDL.SDL_Quit();
				return;
			}

		}

	}
}
