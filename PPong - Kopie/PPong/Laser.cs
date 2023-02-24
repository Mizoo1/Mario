using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Monster.Game;
using PPong;
using PPong.GUI.Sprite;

namespace Monster
{
    public class Laser : Sprite
    {
        private Player player;
        private float laserSpeed = 1f;
        public List<Laser> lasers = new List<Laser>();
        public char richtung;
        public Laser(IntPtr _texture, Player player) : base(_texture, 0, 0, 10, 10)
        {
            this.player = player;
        }
        public Laser(IntPtr _texture, Player player, int x, int y, char richtung) : base(_texture, x, y, 10, 10)
        {
            this.player = player;
            this.richtung = richtung;
        }
        public void loadContent()
        {
            foreach (Sprite s in lasers)
                s.LoadContent();
            // TODO
            // Sounds laden
        }
        public void FireLaser()
        {
            lasers.Add(new Laser(_texture, player, (int)player.X + player.WSize / 2 ,(int) player.Y - player.HSize / 2 + 54,player.getDirction()));
            PlayLaserSound();
        }
        public void PlayLaserSound()
        {
            // TODO
        }
        public void DrawLaser(IntPtr surface, IntPtr randerer)
        {
            // TODO
            // Die Liste mit den Laser-Schüssen (laserShots) durchlaufen
            // und alle Schüsse (LaserTexture) zeichnen
            foreach (Laser v in lasers)
            {
                v.Draw(surface, randerer);
            }

        }
        public void UpdateLaserShots(List<Sprite> _sprites,Enemy enemy)
        {

            int laserIndex = 0;

            while (laserIndex < lasers.Count)
            {
                // hat der Schuss den Bildschirm verlassen?

                if (lasers[laserIndex].Y < 50|| lasers[laserIndex].Y > DisplaySetting.Display_Height-50 || lasers[laserIndex].X < 50|| lasers[laserIndex].X > DisplaySetting.Display_Width-50)
                {
                    lasers.RemoveAt(laserIndex);
                }
                
                else
                {
                    // Position des Schusses aktualiesieren
                    Vector2D temp = new Vector2D((int)lasers[laserIndex].X,(int) lasers[laserIndex].Y);
                    switch(lasers[laserIndex].richtung)
                    {
                        case 'u':
                            temp.Y -= (int)laserSpeed;
                            break;
                        case 'd':
                            temp.Y += (int)laserSpeed;
                            break;
                        case 'l':
                            temp.X -= (int)laserSpeed;
                            break;
                        default :
                            temp.X += (int)laserSpeed;
                            break;
                    }

                    lasers[laserIndex].X = temp.X;
                    lasers[laserIndex].Y = temp.Y;

                    // Überprüfen ob ein Treffer vorliegt
                    int enemyIndex = 0;

                    while (enemyIndex < enemy.Enemies.Count)
                    {
                        // Abstand zwischen Feind-Position und Schuss-Position ermitteln
                        float distance = Vector2.Distance(new Vector2(enemy.Enemies[enemyIndex].X, enemy.Enemies[enemyIndex].Y)
                            , new Vector2(lasers[laserIndex].X, lasers[laserIndex].Y));

                        // Treffer?
                        if (distance < enemy.getEnemyRadius())
                        {
                            // Schuss entfernen
                            lasers.RemoveAt(laserIndex);
                            // Feind entfernen
                            Enemy zombie= enemy.Enemies[enemyIndex];
                            enemy.Enemies.RemoveAt(enemyIndex);
                            _sprites.Remove(zombie);
                            // Punkte erhöhen

                            PlayExplosionSound();

                            // Schleife verlassen
                            break;
                        }
                        else
                        {
                            enemyIndex++;
                        }
                    }
                    laserIndex++;
                }
            }
        }
        public void PlayExplosionSound()
        {
            //TODO
        }
    }
}
