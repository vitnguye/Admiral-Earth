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
/*
namespace BaiscAdEarth
{
    class UpgradeNode
    {
        public enum UpgradeType {None, Offense, Defense, Production};
        public Texture2D image;
        public Node attached;
        public string loadImage;
        public UpgradeType type;

        public void Load(Game game)
        {
            image = game.Content.Load<Texture2D>(loadImage);
        }
    }

    class Offense : UpgradeNode
    {
        bool lockedOn;
        bool canFire;
        LinkedListNode<Enemy> target;
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
            type = UpgradeType.Offense;
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
                    LinkedListNode<Enemy> potential = director.minions.First;
                    LinkedListNode<Enemy> test = director.minions.First;
                    while (test != null)
                    {
                        if (test.Value.position.Y > potential.Value.position.Y)
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

    class Defense : UpgradeNode
    {
        int downtime;
        int radius;
        bool isDisabled;
        Alarm recharge;

        public Defense(Node upgraded, Game game)
        {
            attached = upgraded;
            loadImage = "Images/Shield";
            image = game.Content.Load<Texture2D>(loadImage);
            downtime = 150;
            recharge = new Alarm(downtime, false, "recharge");
            type = UpgradeType.Defense;
            Load(game);
        }

        public void Update(EnemyManager director)
        {
            if (!isDisabled)
            {
                //collision detection between enemy list and bounding box of shield
            }
            else
            {
                recharge.update();
                if (recharge.ring())
                {
                    isDisabled = false;
                }
            }
        }

        public void draw(SpriteBatch sp, Game game)
        {
            if (!isDisabled)
            {
                sp.Draw(image, new Vector2(attached.Xpos + 15, attached.Ypos + 15), Color.White);
            }
        }
    }

    class Production : UpgradeNode
    {
        int rate { get; set; }
        Alarm reload;
        bool isDisabled;

        public Production(Node upgraded, Game game)
        {
            isDisabled = false;
            attached = upgraded;
            loadImage = "Images/Upro";
            rate = 15;
            reload = new Alarm(rate, true, "reload");
            type = UpgradeType.Production;
            Load(game);
        }

        public void Update(Player p)
        {
            if (!isDisabled)
            {
                //increment cooldown timer
                reload.update();
                if (reload.ring())
                {
                    p.ammo++;
                }
            }
        }
    }
}
*/