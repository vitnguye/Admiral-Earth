using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaiscAdEarth
{
    public class Enemy
    {
        //int x;
        //int y;
        public int points;
        public Vector2 position { get; set; }
        public double speed { get; set; }

        public Texture2D image { get; set; }

        public Vector2 destination { get; set; }

        //new
        public bool isDead { get; set; }
        public bool explode { get; set; }

        //explosion stuff
        public float explosionTime;
        public float explosionRadius;
        public float currentTime;
        public float currentRadius { get; set; }
        public float minRadius;
        public Texture2D explosionImage;
        // end explosion stuff

        //trail stuff
        public Texture2D trailImage;
        public Vector2 start;
        public double angle;
        public double length;

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X - image.Width / 2, (int)position.Y - image.Height / 2, image.Width, image.Height); }
        }

        //public Rectangle CollisionBox
        //{
        //    get{return new Rectangle(x,y, 45, 45);}
        //}

        public Enemy()
        {
            position = new Vector2(50, 50);
            destination = new Vector2(0, 0);
            points = 10;
        }

        public Enemy(Vector2 pos, Vector2 dest, float s, int r, Game game)
        {
            position = pos;
            destination = dest;
            speed = s;
            image = game.Content.Load<Texture2D>("Images/enemy");

            //explosion
            explosionTime = 120;
            explosionRadius = r;
            currentTime = 0;
            currentRadius = 0;
            minRadius = image.Width / 2;
            explosionImage = game.Content.Load<Texture2D>("Images/enemyexplosion");
            explode = false;
            isDead = false;

            //trail
            start = pos;

            float width = destination.X - start.X;
            float height = destination.Y - start.Y;
            angle = Math.Atan2(height, width);
            length = 0;
            trailImage = game.Content.Load<Texture2D>("Images/smoketrail");
        }


        // newly added

        public virtual void Update()
        {
            if (explode)
                Explode();
            else
            {
                float width = position.X - start.X;
                float height = position.Y - start.Y;
                length = Math.Sqrt(width * width + height * height);
                Move();
            }
        }

        public virtual void Move()
        {
            double dx = destination.X - position.X;
            double dy = destination.Y - position.Y;

            double angle = Math.Atan2(dy, dx);

            dx = speed * Math.Cos(angle);
            dy = speed * Math.Sin(angle);

            position += new Vector2((float)dx, (float)dy);
        }

        public void Explode()
        {
            if (currentTime <= explosionTime)
            {
                currentRadius = minRadius + (currentTime / explosionTime) * (explosionRadius - minRadius);
                currentTime++;
            }
            else
                isDead = true;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            DrawTrail(sb);
            if (explode)
                sb.Draw(explosionImage, position, null, Color.White, 0, new Vector2(explosionImage.Width / 2, explosionImage.Height / 2), currentRadius / (explosionImage.Height / 2), SpriteEffects.None, 0); 
            else
                sb.Draw(image, position, null, Color.White, (float)(angle - Math.PI / 2), new Vector2(image.Width / 2, image.Height / 2), 1, SpriteEffects.None, 0);
        }

        public virtual void DrawTrail(SpriteBatch sb)
        {
            sb.Draw(trailImage, start, null, Color.White, (float)angle, new Vector2(0, trailImage.Height / 2), new Vector2((float)length / trailImage.Width, 1), SpriteEffects.None, 0);
        }
    }
}
