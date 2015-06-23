using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaiscAdEarth
{
    public class CurvingEnemy : Enemy
    {
        public double currentAngle;
        public double turnAngle;
        public double maxAngle;
        public double minAngle;
        public double turnSpeed;
        public bool increaseAngle;

        public LinkedList<Vector3> trails; //x is xpos, y is ypos, and z is angle
        public int counter;
        public int maxCount;

        public CurvingEnemy(Vector2 pos, Vector2 dest, float s, int r, Game game)
        {
            position = pos;
            destination = dest;
            speed = s*0.7;
            image = game.Content.Load<Texture2D>("Images/curvingenemy");

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

            currentAngle = angle;
            turnAngle = Math.PI / 4;
            maxAngle = angle + turnAngle;
            minAngle = angle - turnAngle;
            turnSpeed = Math.PI / 200;
            increaseAngle = true;

            trails = new LinkedList<Vector3>();
            counter = 0;
            maxCount = 3;
        }

        public override void Update()
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
            if (counter < maxCount)
                counter++;
            else 
            {
                counter = 0;
                trails.AddLast(new Vector3(position.X, position.Y, (float)currentAngle));
            }
        }

        public override void Move()
        {
            //double dx = destination.X - position.X;
            //double dy = destination.Y - position.Y;

            if (increaseAngle)
                currentAngle += turnSpeed;
            else
                currentAngle -= turnSpeed;

            if (currentAngle > maxAngle)
                increaseAngle = false;
            else if (currentAngle < minAngle)
                increaseAngle = true;

            double dx = speed * Math.Cos(currentAngle);
            double dy = speed * Math.Sin(currentAngle);

            position += new Vector2((float)dx, (float)dy);
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawTrail(sb);
            if (explode)
                sb.Draw(explosionImage, position, null, Color.White, 0, new Vector2(explosionImage.Width / 2, explosionImage.Height / 2), currentRadius / (explosionImage.Height / 2), SpriteEffects.None, 0);
            else
                sb.Draw(image, position, null, Color.White, (float)(currentAngle - Math.PI / 2), new Vector2(image.Width / 2, image.Height / 2), 1, SpriteEffects.None, 0);
        }

        public override void DrawTrail(SpriteBatch sb)
        {
            foreach (Vector3 v in trails)
            {
                sb.Draw(trailImage, new Vector2(v.X, v.Y), null, Color.White, v.Z, new Vector2(trailImage.Width / 2, trailImage.Height / 2), 1, SpriteEffects.None, 0);
            }
        }
    }
}
