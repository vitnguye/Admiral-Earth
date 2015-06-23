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
    class Node
    {
        public int Xpos;
        public int Ypos;
        private int resource;
        public Texture2D image;
        public Texture2D OButton;
        public Vector2 OPos;
        public bool hasO;
        public Texture2D DButton;
        public Vector2 DPos;
        public bool hasD;
        public Texture2D RButton;
        public Vector2 RPos;
        public bool hasP;
        public Offense Trait1;
        public Defense Trait2;
        public Production Trait3;
        public Rectangle imageBox;
        public bool isActive;
        public bool isHit;
        public int numberUpgrades;
        Alarm ResourceRegen;
        int timer;

        public Node()
        {
            Xpos = 0;
            Ypos = 0;
        }

        public Node(int x, int y, Game game)
        {
            Xpos = x;
            Ypos = y;
            //image = game.Content.Load<Texture2D>("images/node");
            image = game.Content.Load<Texture2D>("8bit/node");
            OButton = game.Content.Load<Texture2D>("images/OButton");
            OPos = new Vector2(Xpos-30, 690);
            DButton = game.Content.Load<Texture2D>("images/DButton");
            DPos = new Vector2(Xpos+30, 690);
            RButton = game.Content.Load<Texture2D>("images/RButton");
            RPos = new Vector2(Xpos+80, 690);

            imageBox = new Rectangle(Xpos,Ypos, image.Width, image.Height);
            isHit = false;
            numberUpgrades = 0;
            hasO = false;
            hasD = false;
            hasP = false;
            timer = 300;
            ResourceRegen = new Alarm(timer, true, "booyea");
        }

        public Rectangle BoundingBox
        {
            get
            {             
               return new Rectangle(Xpos, Ypos, 50, 50);
            }
        }

        //private void Load(Game game)
        //{
        //    image = game.Content.Load<Texture2D>("Images/node");
        //    imageBox = new Rectangle(Xpos, Ypos, image.Width, image.Height);
        //}

        public void updateUpgrades(EnemyManager director, Player p, NodeManager worker)
        {
            ResourceRegen.update();
            if (ResourceRegen.ring())
            {
                p.ammo++;
                ResourceRegen.set(timer);
            }

            if (Trait1 != null)
            {
                Trait1.Update(director);
            }

            if (Trait2 != null)
            {
                Trait2.Update(director);
            }

            if (Trait3 != null)
            {
                Trait3.Update(p, worker);
                Trait3.workHarderBy(40);
            }
                
        }

        public void drawUpgrades(SpriteBatch sp, Game game)
        {
            if (Trait1 != null)
            {
                Trait1.draw(sp);
            }
            if (Trait2 != null)
            {
                Trait2.draw(sp, game);
            }
            if (Trait3 != null)
            {
                Trait3.draw(sp);
            }
        }

        private bool Collision(Enemy test) 
        {
            Rectangle temp = new Rectangle((int)test.position.X, (int)test.position.Y, test.image.Width, test.image.Height);
            if (imageBox.Intersects(temp)) return true;
            return false;
        }

        public bool updateCollision(EnemyManager director)
        {
            foreach (Enemy test in director.minions)
            {              
                if (test.position.X > Xpos && test.position.X < Xpos + image.Width)
                    if (Collision(test))
                    {
                        director.minions.Remove(test);
                        isHit = true;
                        return true;
                    }
            }
            return false;
        }

        public void draw(SpriteBatch sp, Game game, SpriteFont sf)
        {
           
            sp.Draw(image, new Vector2(Xpos, Ypos), Color.Gray);
            if (isActive)
            {
                //Draw Buttons
                sp.Draw(OButton, OPos, Color.White);
                sp.DrawString(sf, "Cost: 1000", new Vector2(OPos.X-60, OPos.Y+10), Color.Black);
                sp.Draw(DButton, DPos, Color.White);
                sp.DrawString(sf, "Cost: 600", new Vector2(DPos.X-30, DPos.Y+25), Color.Black);
                sp.Draw(RButton, RPos, Color.White);
                sp.DrawString(sf, "Cost: 300", new Vector2(RPos.X, RPos.Y+10), Color.Black);
                sp.Draw(image, new Vector2(Xpos, Ypos), Color.Wheat);
            }
        }
    }
}
