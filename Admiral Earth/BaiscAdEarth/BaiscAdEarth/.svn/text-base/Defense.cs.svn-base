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
    class Defense
    {
            int downtime;
            bool isDisabled;
            Alarm recharge;
            public Texture2D image;
            public Node attached;
            public string loadImage;
            public Texture2D shieldIcon;

            public Defense(Node upgraded, Game game)
            {
                attached = upgraded;
                loadImage = "Images/ShieldTrans";
                image = game.Content.Load<Texture2D>(loadImage);
                isDisabled = false;
                downtime = 350;
                recharge = new Alarm(downtime, false, "recharge");
                Load(game);
            }

            public void Load(Game game)
            {
                image = game.Content.Load<Texture2D>(loadImage);
                shieldIcon = game.Content.Load<Texture2D>("Images/shieldIcon");
            }

            public void Update(EnemyManager director)
            {
                if (!isDisabled)
                {
                    //collision detection between enemy list and bounding box of shield
                    LinkedList<Enemy> removals = new LinkedList<Enemy>();
                    foreach (Enemy test in director.minions)
                    {
                        Rectangle box = new Rectangle(attached.Xpos - 15, attached.Ypos - 30,
                                                      image.Width, image.Height);

                        if (box.Contains((int)test.position.X, (int)test.position.Y))
                        {
                            removals.AddLast(test);
                            isDisabled = true;
                            recharge.set(downtime, true);
                        }
                    }
                    foreach (Enemy curr in removals)
                    {
                        director.minions.Remove(curr);
                    }
                }
                if (isDisabled)
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
                    sp.Draw(image, new Vector2(attached.Xpos - 15, attached.Ypos - 30), Color.White);
                }
                sp.Draw(shieldIcon, new Vector2(attached.Xpos + 15, attached.Ypos + 50), Color.White);
            }
        }
}
