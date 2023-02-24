using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Monster.Game;
using PPong.GUI.Sprite;

namespace Monster.GUI.Sprite
{
    public class Zombie : Enemy
    {
        private Random random = new Random();
        public List<Coordinaten> Coordinaten;

        public Zombie(IntPtr _texture ) : base(_texture, 0, 0, 50, 50)
        {
            coor = new Coordinaten();
            Coordinaten = coor.GetCoordinatens();
        }
        public Zombie(IntPtr _texture, int x, int y, char richtung) : base(_texture, x, y, 50, 50)
        {
            this.richtung = richtung;
            speed = 0.03f;
        }

        public Zombie(IntPtr _texture, int x, int y) : base(_texture, x, y, 50, 50)
        {

        }
        public void loadContent(List<PPong.GUI.Sprite.Sprite> sprites)
        {
            enemyRadius = 30;
            foreach (Enemy s in Enemies)
                s.LoadContent();
            if (Enemies.Count == 0)
                CreateEnemy(sprites);
        }
        public void update(List<PPong.GUI.Sprite.Sprite> sprites)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                //TODO Rotation 

                if (Enemies[i].richtung == 'h')
                {
                    Enemies[i].Velocity.Y = Enemies[i].speed;

                }
                else if (Enemies[i].richtung == 'v')
                {
                    Enemies[i].Velocity.X = Enemies[i].speed;
                }

                foreach (var sprite in sprites)
                {

                    if (sprite == Enemies[i])
                        continue;

                    if ((Enemies[i].Velocity.X > 0 && Enemies[i].IsTouchingLeft(sprite)) ||
                        (Enemies[i].Velocity.X < 0 & Enemies[i].IsTouchingRight(sprite)))
                    {
                        Enemies[i].Velocity.X *= -1;
                        Enemies[i].speed *= -1;
                    }


                    else if ((Enemies[i].Velocity.Y > 0 && Enemies[i].IsTouchingTop(sprite)) ||
                        (Enemies[i].Velocity.Y < 0 & Enemies[i].IsTouchingBottom(sprite)))
                    {
                        Enemies[i].Velocity.Y *= -1;
                        Enemies[i].speed *= -1;
                    }


                }



                Enemies[i].Y += Enemies[i].Velocity.Y;
                Enemies[i].X += Enemies[i].Velocity.X;
                Enemies[i].Velocity.Y = 0;
                Enemies[i].Velocity.X = 0;

            }


        }
        public void CreateEnemy(List<PPong.GUI.Sprite.Sprite> sprites)
        {

            bool ok = false;
            for (int i = 0; i < 10; i++)
            {
                Zombie zombie = null;
                ok = false;
                while (!ok)
                {
                    int randomIndex = random.Next(0, Coordinaten.Count);
                    Coordinaten c = Coordinaten[randomIndex];
                    int randomNumber = random.Next(0, 2);
                    char randomChar = randomNumber == 0 ? 'v' : 'h';
                    foreach (PPong.GUI.Sprite.Sprite s in sprites)
                    {
                        zombie = new Zombie(_texture, c.getX(), c.getY(), randomChar);
                        if (!zombie.IsCollidingWith(s))
                            ok = true;
                        else
                        {
                            ok = false;
                            break;
                        }
                    }
                    
                }
                Enemies.Add(zombie);
                sprites.Add(zombie);
            }

        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            foreach (Enemy s in Enemies)
                s.Draw(surface, renderer);
        }
    }
}
