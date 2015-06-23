using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaiscAdEarth
{
    public class SplitterEnemy : Enemy
    {
        public int spawn; // number of regulars it spawns
        EnemyManager director; // the enemymanager
        Game game; // necessary for spawning new enemies
        public int splitHeight; // height at which the splitter splits

        public SplitterEnemy(Vector2 pos, Vector2 dest, float s, int r, Game g, EnemyManager dir)
        {
            position = pos;
            destination = dest;
            speed = s*0.8;
            image = g.Content.Load<Texture2D>("Images/splitterenemy");

            //explosion
            explosionTime = 120;
            explosionRadius = r;
            currentTime = 0;
            currentRadius = 0;
            minRadius = image.Width / 2;
            explosionImage = g.Content.Load<Texture2D>("Images/enemyexplosion");
            explode = false;
            isDead = false;

            //trail
            start = pos;

            float width = destination.X - start.X;
            float height = destination.Y - start.Y;
            angle = Math.Atan2(height, width);
            length = 0;
            trailImage = g.Content.Load<Texture2D>("Images/smoketrail");

            spawn = 3;
            director = dir;
            game = g;
            splitHeight = game.Window.ClientBounds.Height / 2;
        }

        public override void Update()
        {
            
            if (position.Y > splitHeight)
            {
                Split();
                isDead = true;
                
            }
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

        new public void Draw(SpriteBatch sb)
        {
            DrawTrail(sb);
            if (explode)
                sb.Draw(explosionImage, position, null, Color.White, 0, new Vector2(explosionImage.Width / 2, explosionImage.Height / 2), currentRadius / (explosionImage.Height / 2), SpriteEffects.None, 0);
            else
                sb.Draw(image, position, null, Color.White, (float)(angle - Math.PI / 2), new Vector2(image.Width / 2, image.Height / 2), 1, SpriteEffects.None, 0);
        }

        new public void DrawTrail(SpriteBatch sb)
        {
            sb.Draw(trailImage, start, null, Color.White, (float)angle, new Vector2(0, trailImage.Height / 2), new Vector2((float)length / trailImage.Width, 1), SpriteEffects.None, 0);
        }

        public void Split()
        {
                for (int i = 0; i < spawn; i++)
                {
                    int Baseline = game.Window.ClientBounds.Height;
                    int randX = director.RandomNumber(0, game.Window.ClientBounds.Width);
                    Vector2 dest = new Vector2 (randX, Baseline);

                    director.minionsToBeAdded.AddLast(new Enemy(position, dest, director.MinionSpeed, director.minionExplosion, game));
                }
        }
    }
}
