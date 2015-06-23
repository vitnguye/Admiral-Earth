using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BaiscAdEarth
{
    //this is the broad class definition for the projectiles fired ||||by the player.|||||
    public class Projectile
    {

        bool pActive; //-Vic
        public Vector2 position { get; set; }
        public float speed { get; set; }
        public bool isActive { get { return pActive; } set { pActive = value; } }//changed -Vic
        public bool explode { get; set; }
        public Texture2D image { get; set; }

        //explosion stuff
        public float explosionTime;
        public float explosionRadius;
        public float currentTime;
        public float currentRadius { get; set; }
        public float minRadius;
        public Texture2D explosionImage;
        public bool isDead { get; set; }
        public static SoundEffect BoomSnd;
        // end explosion stuff

        //trail stuff
        public Texture2D trailImage;
        public Vector2 start;
        public double angle;
        public double length;

        public Vector2 destination { get; set; }

        public Projectile()
        {
            isActive = false;
            position = new Vector2(50, 50);
            destination = new Vector2(0, 0);
        }

        public Projectile(Vector2 pos, Vector2 dest, float s, int r, Game game)
        {
            isActive = true;
            position = pos;
            destination = dest;
            speed = s;
            image = game.Content.Load<Texture2D>("Images/projectile");

            //explosion
            explosionTime = 80;
            explosionRadius = r;
            currentTime = 0;
            currentRadius = 0;
            minRadius = image.Width / 2;
            explosionImage = game.Content.Load<Texture2D>("Images/explosion");
            BoomSnd = game.Content.Load<SoundEffect>(@"Sounds\flak"); //load Sound FX -V

            //trail
            start = pos;

            float width = destination.X - start.X;
            float height = destination.Y - start.Y;
            angle = Math.Atan2(height, width);
            length = 0;
            trailImage = game.Content.Load<Texture2D>("Images/trail");

            
        }

        public void Update()
        {
            //if projectile location = destination, deactivate projectile and EXPLODE
            if (((position.Y <= destination.Y) && (position.X <= destination.X)) 
                || ((position.X > destination.X) && (position.Y <= destination.Y)))
            {
                isActive = false;
                explode = true;
            }

            // new
            if (explode)
            {
                Explode();
            }
            else
            {
                float width = position.X - start.X;
                float height = position.Y - start.Y;
                length = Math.Sqrt(width * width + height * height);
                Move();
                //BoomSnd.Play(1.0f, 0f, 0f); //plays the explosion sound -Vic
            }

        }

        public void Move()
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
                if (currentRadius == minRadius)
                {
                    BoomSnd.Play(1.0f, 0f, 0f); //I moved the explosion sound here -Kyle
                }
                currentRadius = minRadius + (currentTime / explosionTime) * (explosionRadius - minRadius);
                currentTime++;
            }
            else
                isDead = true;
        }

        public void Draw(SpriteBatch sb)
        {
            DrawTrail(sb);
            if (isActive)
            {
                sb.Draw(image, position, null, Color.White, (float)(angle + Math.PI / 2), new Vector2(image.Width / 2, image.Height / 2), 1, SpriteEffects.None, 0);
            }
            else
            {
                sb.Draw(explosionImage, position, null, Color.White, 0, new Vector2(explosionImage.Width / 2, explosionImage.Height / 2), currentRadius / (explosionImage.Height / 2), SpriteEffects.None, 0);                
            }
            
        }
        public void DrawTrail(SpriteBatch sb)
        {
            sb.Draw(trailImage, start, null, Color.White, (float)angle, new Vector2(0, trailImage.Height / 2), new Vector2((float)length / trailImage.Width, 1), SpriteEffects.None, 0);
        }
    }
}
