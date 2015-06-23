using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BaiscAdEarth
{
    class ProjectileManager
    {
        public int radius { get; set; }
        public int speed { get; set; }
        public int bombs { get; set; }
        public LinkedList<Projectile> projectiles; //Player passes in the map of destinations.

        public ProjectileManager()
        {
            speed = 5;
            radius = 5; //placeholder values
            bombs = 0;
            projectiles = new LinkedList<Projectile>();
        }

     /*   private void CalcSpeed(Player p)
        {
            //some algorithm using p.speed_upgrade to calculate projectile speed
        }

        private void CalcRadius(Player p)
        {
            //some algorithm using p.radius_upgrade to calculate projectile blast radius
        }

        private void NumBombs(Player p)
        {
            bombs = p.bomb_upgrade;
        }*/

        public void addProjectile(Projectile p)
        {
            projectiles.AddLast(p);
            //p.IsActive;
        }

        public void Update()
        {
            foreach (Projectile p in projectiles)
            {
                p.Move();
            }            

        }

        public void Draw(SpriteBatch sp)
        {
            foreach (Projectile p in projectiles)
            {
                p.Draw(sp);
            }
        }

    }
}
