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
    public class EnemyManager
    {

        public LinkedList<Enemy> minions;
        public float MinionSpeed { get; set; }
        Alarm MinionSpawn;
        int MinionSpawnTime;
        Alarm Difficulty;
        int DiffTime;
        Random random;
        int points;

        //new
        public int minionExplosion { get; set; }

        public LinkedList<Enemy> minionsToBeAdded { get; set; }

        public EnemyManager()
        {
            minions = new LinkedList<Enemy>();
            MinionSpeed = .4f;
            MinionSpawnTime = 100;
            minionExplosion = 50;
            DiffTime = 300;
            MinionSpawn = new Alarm(MinionSpawnTime, true, "spawn");
            Difficulty = new Alarm(DiffTime, true, "diff");
            random = new Random();

            minionsToBeAdded = new LinkedList<Enemy>();
        }

        public void add(Game game)
        {
            //int Baseline = game.Window.ClientBounds.Height - 100;
            int Baseline = game.Window.ClientBounds.Height;
            int randX = RandomNumber(0, game.Window.ClientBounds.Width);

            Vector2 dest = new Vector2 (randX, Baseline);
            Enemy temp = new Enemy(new Vector2(RandomNumber(0, game.Window.ClientBounds.Width), 0), dest, MinionSpeed, minionExplosion, game);
            int number = RandomNumber(0, 100);
            if (number < 50)
                temp = new Enemy(new Vector2(RandomNumber(0, game.Window.ClientBounds.Width), 0), dest, MinionSpeed, minionExplosion, game);
            else if (number < 80)
                temp = new CurvingEnemy(new Vector2(RandomNumber(0, game.Window.ClientBounds.Width), 0), dest, MinionSpeed, minionExplosion, game);// Add curving enemy
            else
                temp = new SplitterEnemy(new Vector2(RandomNumber(0, game.Window.ClientBounds.Width), 0), dest, MinionSpeed, minionExplosion, game, this);//Add splitting enemy
            
            minions.AddLast(temp);

        }
        
        public int totalPointsForKills
        {
            get { return points; } //return total points per enemy destroyed -V
        }

        public bool isEmpty()
        {
            if (minions.First == null) return true;
            return false;
        }

        public void update(Game game, LinkedList<Projectile> projectiles, NodeManager nodes)
        {
            foreach (Enemy e in minionsToBeAdded)
            {
                minions.AddLast(e);
            }
            minionsToBeAdded = new LinkedList<Enemy>();

            foreach (Enemy curr in minions)
            {
                curr.Update();
            }

            MinionSpawn.update();
            Difficulty.update();
            if (MinionSpawn.ring())
            {
                MinionSpawn.set(MinionSpawnTime);
                add(game);
            }

            if (Difficulty.ring())
            {
                if (MinionSpawnTime > 25) MinionSpawnTime--;
                if (DiffTime > 100) DiffTime -= 10;
                if (MinionSpeed < 2) MinionSpeed += 0.1f;
                Difficulty.set(DiffTime);
            }

            //new
            LinkedList<Enemy> killed = new LinkedList<Enemy>();
            foreach (Enemy e in minions)
            {
                //Detects Collision for Explosion
                foreach (Projectile p in projectiles)
                {
                    if (p.explode)
                    {
                        double dx = e.position.X - p.position.X;
                        double dy = e.position.Y - p.position.Y;
                        if (Math.Sqrt(dx * dx + dy * dy) < (p.currentRadius + e.image.Width / 2))
                            e.explode = true;                        
                    }
                }
                if (e.explode)
                    foreach (Enemy x in minions)
                    {
                        double dx = x.position.X - e.position.X;
                        double dy = x.position.Y - e.position.Y;
                        if (Math.Sqrt(dx * dx + dy * dy) < (e.currentRadius + x.image.Width / 2))
                            x.explode = true;
                    }   //<-------semi colon?

                if (e.isDead)
                {
                    killed.AddLast(e);
                    points += 100; //points per kill -V
                    nodes.cash += 100;
                }
                if (e.position.Y > 650) killed.AddLast(e);
            }
            // Removes Killed Enemies
            foreach (Enemy e in killed)
                minions.Remove(e);
        }

        public void draw(SpriteBatch sp)
        {
            foreach (Enemy curr in minions)
            {
                curr.Draw(sp);
            }
        }

        public int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
