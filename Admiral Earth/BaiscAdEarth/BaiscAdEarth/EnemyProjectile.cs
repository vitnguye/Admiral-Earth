using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaiscAdEarth
{
    public class EnemyProjectile
    {
        static Random rndGen = new Random();
        int posx;
        int posy;
        bool alive = true;
        int speed = 3;
        int points;

        public bool isdead
        {
        }
        //This is a place holder
        //random starting posistion
        //random remaining target
        //create missile from starting posisition
        //set speed
        //if(missle.y <= target.y)
        //missile.y += speed;
        //images
    }
}
