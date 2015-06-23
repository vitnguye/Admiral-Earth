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
    class Production
    {
        int rate { get; set; }
        Alarm reload;
        bool isDisabled;
        public Texture2D image;
        public Texture2D mojoIcon;
        public Node attached;
        public string loadImage;
        int minerals;
        public int gather
        {
            get { return minerals; }

        }
        public void workHarderBy(int m)
        {
            minerals = m;
        }
        public Production(Node upgraded, Game game)
        {
            isDisabled = false;
            attached = upgraded;
            loadImage = "Images/Upro";
            rate = 115;
            reload = new Alarm(rate, true, "reload");
            minerals = 0;
            Load(game);
        }

        public void Load(Game game)
        {
            //image = game.Content.Load<Texture2D>(loadImage);
            mojoIcon = game.Content.Load<Texture2D>("Images/mojoIcon");
        }

        public void Update(Player p, NodeManager worker)
        {
            if (!isDisabled)
            {
                //increment cooldown timer
                reload.update();
                if (reload.ring())
                {
                    p.ammo++;
                    worker.cash += 10+minerals;
                    reload.set(rate);
                }
            }
           
        }
        public void draw(SpriteBatch sp)
        {
            
            sp.Draw(mojoIcon, new Vector2(attached.Xpos , attached.Ypos + 45), Color.White);
        }
    }
}
