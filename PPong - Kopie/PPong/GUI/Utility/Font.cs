using System;
using SDL2;

class Font
{
    private IntPtr _font;

    public Font(string fontPath, int fontSize)
    {
        _font = SDL_ttf.TTF_OpenFont(fontPath, fontSize);
        if (_font == IntPtr.Zero)
        {
            throw new Exception("Failed to load font: " + SDL_ttf.TTF_GetError());
        }
    }

    ~Font()
    {
        SDL_ttf.TTF_CloseFont(_font);
    }

    public void RenderText(IntPtr renderer, string text, SDL.SDL_Color color, int x, int y)
    {
        // Create a surface with the text rendered in the specified color
        IntPtr textSurface = SDL_ttf.TTF_RenderUTF8_Blended(_font, text, color);
        if (textSurface == IntPtr.Zero)
        {
            throw new Exception("Failed to render text: " + SDL_ttf.TTF_GetError());
        }

        // Create a texture from the surface
        IntPtr textTexture = SDL.SDL_CreateTextureFromSurface(renderer, textSurface);
        if (textTexture == IntPtr.Zero)
        {
            throw new Exception("Failed to create texture from surface: " + SDL.SDL_GetError());
        }

        // Get the size of the text
        int width, height;
        SDL_ttf.TTF_SizeUTF8(_font, text, out width, out height);

        // Create a rectangle to specify the position and size of the text
        SDL.SDL_Rect textRect = new SDL.SDL_Rect
        {
            x = x,
            y = y,
            w = width,
            h = height
        };

        // Render the texture to the screen
        SDL.SDL_RenderCopy(renderer, textTexture, IntPtr.Zero, ref textRect);
        // Clean up
        SDL.SDL_DestroyTexture(textTexture);
        SDL.SDL_FreeSurface(textSurface);
    }
}