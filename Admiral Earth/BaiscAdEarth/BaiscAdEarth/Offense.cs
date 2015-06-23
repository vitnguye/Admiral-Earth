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
    class Offense
    {
            bool lockedOn;
            bool canFire;
            LinkedListNode<Enemy> target;
            Alarm intermitent;
            Alarm charge;
            int intermitentTime;
            int chargeTime;
            
            public Node attached;
            public string loadImage;
            Vector2 laserTarget;
            Texture2D laser;
            Texture2D laserIcon;

            public Offense(Node upgraded, Game game)
            {
                attached = upgraded;
                loadImage = "Images/Uoff";
                lockedOn = false;
                canFire = false;
                intermitentTime = 60;
                chargeTime = 30;
                charge = new Alarm(chargeTime, false, "charge");
                intermitent = new Alarm(intermitentTime, true, "intermitent");
                Load(game);

            }

            public void Load(Game game)
            {
                //image = game.Content.Load<Texture2D>(loadImage);
                laser = game.Content.Load<Texture2D>("Images/laser");
                laserIcon = game.Content.Load<Texture2D>("8bit/Monkey");
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
                        intermitent.set(intermitentTime);
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
                            if (test.Value.position.X > attached.Xpos - 100 && test.Value.position.X < attached.Xpos + 200)
                            {
                                potential = test;
                                lockedOn = true;
                                
 
                            }
                            test = test.Next;
                        }
                        target = potential;
                        charge.set(chargeTime, true);
                        
                    }
                }

                if (lockedOn)
                {
                    // Charge the laser
                    charge.update();
                    laserTarget = target.Value.position;
                    if (charge.ring())
                    {
                        //FIRE THE LASER!!!
                        if (director.minions.Contains(target.Value))
                        {
                            director.minions.Remove(target);
                        }
                        lockedOn = false;
                        canFire = false;
                    }
                }
            }

            public void draw(SpriteBatch sp)
            {
                sp.Draw(laserIcon, new Vector2(attached.Xpos + 16, attached.Ypos + 5), Color.White);
                if (lockedOn)
                {
                    Vector2 currpos = new Vector2(attached.Xpos+35, attached.Ypos+20);
                    float distance = Vector2.Distance(currpos, laserTarget);
                    float angle = (float)Math.Atan2((double)(laserTarget.Y - currpos.Y),
                                                     (double)(laserTarget.X - currpos.X));
                    sp.Draw(laser, currpos, null, Color.White, angle, new Vector2(0, laser.Width / 2), new Vector2(distance / laser.Width, 1) , SpriteEffects.None, 0);

                }
                             
            }
        }
 
}
