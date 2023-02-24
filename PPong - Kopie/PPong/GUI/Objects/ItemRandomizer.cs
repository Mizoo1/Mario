using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Monster.Game;
using SDL2;

namespace Monster.GUI.Objects
{
    public class ItemRandomizer
    {
        #region initializing and constructor 
        Random random = new Random();
        public Items item;
        private Coordinaten coor;
        private IntPtr _randerer;
        public ItemRandomizer(IntPtr _randerer)
        {
            item= new Items();
            coor = new Coordinaten();
            this._randerer = _randerer;
        }
        #endregion initializing and constructor 

        #region creating Items	
        public void CreateItem()
        {
            item.surface = SDL_image.IMG_Load("Assest\\hole.png");
            IntPtr holeTexture = SDL.SDL_CreateTextureFromSurface(_randerer, item.surface);
            Hole hole = new Hole(holeTexture,250,250,50,50);
            item.items.Add(hole);
        }

        public void CreateDoor(IntPtr _texture, List<PPong.GUI.Sprite.Sprite> _sprites)
        {

        }

        public void CreateKey(IntPtr mapKeyTexture, Vector2 position, List<PPong.GUI.Sprite.Sprite> _sprites)
        {
           /* var MapKeyTexture = mapKeyTexture;
            Items item = new MapKey(MapKeyTexture, 250, 250, 50, 50)
            {
                Position = new Vector2(81 * position.X, 81 * position.Y),
            };
            item.items.Add(item);*/
        }

        public void CreateTraps(IntPtr _texture, List<PPong.GUI.Sprite.Sprite> _sprites)
        {

        }
        public void Draw()
        {
            foreach (Items items in item.items)
                items.Draw(item.surface, _randerer);
        }
        public void LoadContent()
        {
            foreach (Items items in item.items)
                items.LoadContent();

        }
        #endregion creating Items
    }
}
