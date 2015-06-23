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
    class Upgrade_Node
    {
        public enum UpgradeType {Offense, Defense, Resource};
        public Texture2D image;
        public Node attached;
        public string loadImage;

        public void Load(Game game)
        {
            image = game.Content.Load<Texture2D>(loadImage);
        }
    }

    class Offense : Upgrade_Node
    {
        bool lockedOn;
        bool canFire;
        LinkedListNode<Projectile> target;
        Alarm intermitent;
        Alarm charge;
        int intermitentTime;
        int chargeTime;

        public Offense(Node upgraded, Game game)
        {
            attached = upgraded;
            loadImage = "Images/Uoff";
            lockedOn = false;
            canFire = false;
            intermitentTime = 100;
            chargeTime = 50;
            charge = new Alarm(chargeTime, false, "charge");
            intermitent = new Alarm(intermitentTime, true, "intermitent");
            Load(game);
        }

        public void Update(EnemyManager director)
        {
            if (!canFire)
            {
                // Incriment cooldown timer
                intermitent.update();
                if (intermitent.ring())
                {
                    canFire = true;
                }
            }

            if (canFire && !lockedOn)
            {

                // Select Target from director
                if (!director.isEmpty())
                {
                    LinkedListNode<Projectile> potential = director.minions.First;
                    LinkedListNode<Projectile> test = director.minions.First;
                    while (test != null)
                    {
                        if (test.Value.posy > potential.Value.posy)
                            potential = test;
                        test = test.Next;
                    }
                    target = potential;
                    charge.set(true);
                    lockedOn = true;
                }
            }

            if (lockedOn)
            {
                // Charge the laser
                charge.update();
                if (charge.ring())
                {
                    //FIRE THE LASER!!!
                    director.minions.Remove(target);
                }
            }
        }

        public void draw(SpriteBatch sp)
        {
        }
    }

    class Defense : Upgrade_Node
    {
        int downtime;
        //int radius;
        bool isDisabled;
        Alarm recharge;

        public Defense(Node upgraded, Game game)
        {
            attached = upgraded;
            loadImage = "Images/Udef";
            downtime = 150;
            recharge = new Alarm(downtime, false, "recharge");

            Load(game);
        }

        public void Update(EnemyManager director)
        {
            //collision detection between enemy list and bounding box
        }
    }

    class Resource : Upgrade_Node
    {
        Alarm replenish;
    }
}
