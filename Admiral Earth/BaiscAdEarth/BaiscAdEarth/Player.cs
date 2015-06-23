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

// 2/22/2011
// Chase Khamashta
// Player class: This is the class where the user interacts with the game
// keeps track of the mouse, the player's nodes, resources, and upgrades, and friendly projectiles

namespace BaiscAdEarth
{
    public class Player
    {
        Vector2 position;
        public static SoundEffect PlayerShot; //-Vic
        int shotsfired;
        public int pylons;
        Texture2D player;
        Texture2D cursor;
        public Rectangle imageBox;
        public int ammo { get; set; } // ammo count
        private int stuntime;
        private Alarm stun;
        public bool isStunned { get; set; } // is player stunned?
        public int speed_upgrade { get; set; }
        public int radius_upgrade { get; set; }
        public int bomb_upgrade { get; set; }
        //public Point firingLoc;
        public LinkedList<Projectile> projectiles;
        public int projectileSpeed;
        public int projectileRadius;
        Vector2 fireOrigin;

        MouseState currentState; // previous state of the mouse
        MouseState prevState; // current state of the mouse

        public int Cost
        {
            get {return shotsfired; }
            
        }

        public Player(Game game)
        {
            shotsfired = 0;
            pylons = 0;
            ammo = 100;
            speed_upgrade = 0;
            radius_upgrade = 0;
            bomb_upgrade = 0;
            stuntime = 65;
            stun = new Alarm(stuntime, true, "stunna");
            isStunned = false;

            player = game.Content.Load<Texture2D>("Images/player");
            position = new Vector2(490, 575);
            imageBox = new Rectangle((int)position.X, (int)position.Y, player.Width, player.Height);
            isStunned = false;
            Load(game);

            fireOrigin = new Vector2(position.X + 24, position.Y + 50);
            projectiles = new LinkedList<Projectile>();
            projectileSpeed = 4;
            projectileRadius = 50;

            currentState = Mouse.GetState();
            prevState = currentState;
        }

        public void currentScore(int score)
        {
            pylons = score;
        }

        public void fireProjectile(Game game)
        {
            projectiles.AddLast(new Projectile(fireOrigin, new Vector2(currentState.X, currentState.Y), projectileSpeed, projectileRadius,  game));
            PlayerShot.Play(1.0f, 0f, 0f); //plays the sound -Vic
            ammo--;
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
                //if (test.position.X > Xpos - 10 && test.position.X < Xpos + image.Width + 10)
                if (test.position.X > position.X && test.position.X < position.Y + player.Width)
                    if (Collision(test))
                    {
                        director.minions.Remove(test);
                        isStunned = true;
                        return true;
                    }
            }
            return false;
        }

        public void Update(Game game, NodeManager nodes, EnemyManager director)
        {
            currentState = Mouse.GetState();

            //check for collisions with player, stun if there are collisions
            if (updateCollision(director) && !isStunned)
            {
                isStunned = true;
            }
            if (isStunned)
            {
                stun.update();
                if (stun.ring())
                {
                    isStunned = false;
                    stun.set(stuntime);
                }
            }
            if (prevState.LeftButton == ButtonState.Pressed && 
                currentState.LeftButton == ButtonState.Released)
            {                
                // Check to see if mouse click was on a node (if so make it active)
                if (!nodes.isClicked(currentState, game))
                {
                     if(!(prevState.Y > 625)&&!(currentState.Y > 625))
                     {   
                         // Else fire projectile
                         if (ammo >= 1 && !isStunned)
                         {
                             fireProjectile(game);
                             shotsfired++;
                         }
                     }
                }
            }
            prevState = currentState;

            LinkedList<Projectile> deadProjectiles = new LinkedList<Projectile>();
            foreach (Projectile p in projectiles)
            {
                p.Update();
                //new
                if (p.isDead)
                    deadProjectiles.AddLast(p);

            }

            // removes the dead projectiles from the active ones
            foreach (Projectile p in deadProjectiles)
                projectiles.Remove(p);
            //end new

        }

        public void Load(Game game)
        {
            cursor = game.Content.Load<Texture2D>("Images/cursor");
            PlayerShot = game.Content.Load<SoundEffect>(@"Sounds/pew"); //loadsound -Vic
        }

        public void Draw(SpriteBatch sp)
        {
            if (isStunned)
            {
                sp.Draw(player, new Vector2(position.X, position.Y), Color.Purple);
            }
            else{
            sp.Draw(player, new Vector2(position.X, position.Y), Color.White);
            }
            sp.Draw(cursor, new Vector2(currentState.X - cursor.Width / 2, currentState.Y - cursor.Height / 2), Color.White);
            foreach (Projectile p in projectiles)
            {
                //changed
                p.Draw(sp);
                /*if (p.isActive)
                {
                    sp.Draw(p.image, p.position, Color.White);
                }*/
            }

        }
    }
}
