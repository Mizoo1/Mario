using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPong.GUI.Sprite
{
	public class EnemSprite : Enemy
	{
		private float speed;
		public EnemSprite(IntPtr _texture,int x, int y) : base(_texture,x, y,20,20)
		{
			speed = 5;
		}
		public void update(IntPtr surface, IntPtr renderer)
		{
			if(Enemies.Count==0)
				createEnemy(surface, renderer);
			
		}
		public void draw(IntPtr surface, IntPtr renderer)
		{
			foreach(Enemy e in Enemies)
				e.Draw(surface, renderer);
		}
		public void createEnemy(IntPtr surface, IntPtr renderer)
		{
			
			int space = 40;
			for (int i=0; i<5; i++)
			{
				EnemSprite e = new EnemSprite(_texture,20+i* space, 50);
				Enemies.Add(e);
			}
		}
		public void EnemySound()
		{

		}
		public void loadContent()
		{
			foreach (Enemy e in Enemies)
				e.LoadContent();
		}
	}
}
