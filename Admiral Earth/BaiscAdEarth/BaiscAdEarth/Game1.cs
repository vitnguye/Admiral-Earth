using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media.Song;
using Microsoft.Xna.Framework.Media;

namespace BaiscAdEarth
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        NodeManager nodes;
        EnemyManager director;
        ProjectileManager projMan;
        Player player;
        bool gameOver;
        bool paused;
        KeyboardState keystate;
        KeyboardState prevstate;
        protected Song bgm;
        bool songstart = false;

        int GameStarted = 0;
        Texture2D titleScreen;
        Texture2D HUD;
        SpriteFont spriteFont;
        
        Vector2 levelTextpos = new Vector2(850, 5);
        Vector2 scoreTextpos = new Vector2(20, 5);
        Vector2 ammoTextpos = new Vector2(850, 5);
        Vector2 startTextpos = new Vector2(400, 550);
        Vector2 gameOverTextpos = new Vector2(425, 230);


        //String lvltext = "IT'S OVER 9000!!!";
        int ammoCount;
        int starterAmmo = 1000;
        int score;
        //int evilwave = 0;

        //private static SoundEffect PlayerShot;
        //private static SoundEffect ExplosionSound;
        //private static SoundEffect UpgradeSound;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            initializeGame();
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //background = Content.Load<Texture2D>(@"Images\background");          
            //HUD = Content.Load<Texture2D>(@"Images\GameHUD");
            background = Content.Load<Texture2D>(@"8bit\blackbackground");
            HUD = Content.Load<Texture2D>(@"8bit\GameHUD");
            titleScreen = Content.Load<Texture2D>(@"Images\TitleScreen");
            spriteFont = Content.Load<SpriteFont>(@"Fonts\Pericles");
            bgm = Content.Load<Song>(@"Sounds\Gideon Wrath Part I");
            MediaPlayer.IsRepeating = true; 
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevstate = keystate;
            keystate = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keystate.IsKeyDown(Keys.Escape))
                    this.Exit();
            if (gameOver && keystate.IsKeyDown(Keys.Space))
            {
                initializeGame();
            }
            if (keystate.IsKeyUp(Keys.P) && prevstate.IsKeyDown(Keys.P)) paused = !paused;
            if (!paused)
            {
                if (!gameOver)
                {
                    if (GameStarted == 1)
                    {
                        if (nodes.isGameOver()) gameOver = true;
                        director.update(this, player.projectiles, nodes);
                        nodes.update(director, player);
                        player.Update(this, nodes, director);
                        //keeps ammo count and subtract the player's shots
                        //ammoCount = starterAmmo - player.Cost;

                        //the score is the score when game 1st starts + points from kills - your shots -V
                        score = director.totalPointsForKills + nodes.cash; //- player.Cost;
                        //player.currentScore(ammoCount); //for player class to read, but not used for now -V
                        if (!songstart)
                        {
                            MediaPlayer.Play(bgm);
                            songstart = true;
                        }  
                    }
                    else
                    {
                        if (keystate.IsKeyDown(Keys.Space))
                        {
                            //StartGame();
                            GameStarted = 1;
                        }
                    }
                    base.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (GameStarted == 1)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 768), Color.White);
                nodes.draw(spriteBatch, this, spriteFont);
                director.draw(spriteBatch);
                player.Draw(spriteBatch);

                if (paused)
                    spriteBatch.DrawString(spriteFont, "P A U S E D", gameOverTextpos, Color.Gold);
                spriteBatch.Draw(HUD, new Rectangle(0, 0, 1024, 720), Color.White);
                
                spriteBatch.DrawString(spriteFont, "Score: " + score.ToString(), scoreTextpos, Color.White);
                
                spriteBatch.DrawString(spriteFont, "Mojo: " + player.ammo.ToString(), ammoTextpos, Color.White);
                
                spriteBatch.DrawString(spriteFont, "Cash: " + nodes.cash.ToString(), new Vector2(852, 32), Color.White);

                if (gameOver)
                {
                    spriteBatch.DrawString(spriteFont, "G A M E   O V E R", gameOverTextpos, Color.Gold);
                    if (gameTime.TotalGameTime.Milliseconds % 1000 < 500)
                    {
                        spriteBatch.DrawString(spriteFont, "Press Space to try again", new Vector2(425, 270), Color.Gold);
                    }
                }
            }
            else
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, 1024, 768), Color.White);
                /*if (gameTime.TotalGameTime.Milliseconds % 1000 < 500)
                {
                    spriteBatch.DrawString(spriteFont, "Press SPACE to Begin", startTextpos, Color.Gold);
                }*/
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void initializeGame()
        {
            nodes = new NodeManager(100, 615, 250, 400, 600, 750, 900, this);
            director = new EnemyManager();
            projMan = new ProjectileManager();
            player = new Player(this);
            gameOver = false;
            paused = false;
            keystate = Keyboard.GetState();
        }
    }
}
