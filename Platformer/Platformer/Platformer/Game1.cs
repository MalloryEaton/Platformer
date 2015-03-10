using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int screenWidth;
        public int screenHeight;

        private World world;
        private Character character;
        private Ground ground;
        private List<Ground> grounds;
        private Border border;
        private List<Border> borders;
        private Goal goal;
        private Pit pit;
        private List<Pit> pits;
        private Barrier barrier;
        private List<Barrier> barriers;

        private KeyboardState oldKeyState;

        public bool isStone = false;
        private Texture2D stoneSprite;
        private Body stoneBody;
        private Vector2 stoneOrigin;

        private Texture2D background1;
        private Texture2D background2;
        private Texture2D background3;

        private Texture2D victoryScreen;

        private SoundEffect jump;
        private SoundEffect grassLandMusic;
        SoundEffectInstance instance;
        private SoundEffect attack;
        private SoundEffect victory;

        public static float HalfScreenWidth { get; private set; }

        private Vector2 characterInitPos;
        public static int currentLevel = 1;

        private bool levelCleared = false;
        private bool victoryScreenIsPlaying = false;

        //private GameTime time;
        //private double elapsedSeconds;

        #region Game1
        public Game1()
        {
            this.IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            world = new World(new Vector2(0, 9.82f));
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            stoneSprite = Content.Load<Texture2D>(@"images/kirbyStone");
            stoneBody = BodyFactory.CreateBody(world);
            stoneOrigin = new Vector2(ConvertUnits.ToSimUnits(stoneSprite.Width / 2),
                ConvertUnits.ToSimUnits(stoneSprite.Height / 2));

            background1 = Content.Load<Texture2D>(@"images/grassBackground");
            background2 = Content.Load<Texture2D>(@"images/iceBackground");
            background3 = Content.Load<Texture2D>(@"images/sandBackground");

            victoryScreen = Content.Load<Texture2D>(@"images/victoryScreen");

            jump = Content.Load<SoundEffect>(@"sounds/jump");
            grassLandMusic = Content.Load<SoundEffect>(@"sounds/grasslandMusic");
            instance = grassLandMusic.CreateInstance();
            instance.IsLooped = true;

            attack = Content.Load<SoundEffect>(@"sounds/stone");
            victory = Content.Load<SoundEffect>(@"sounds/victory");

            CreateGameComponents();
        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region ResetCharacter
        private void ResetCharacter()
        {
            character.Body.ResetDynamics();
            character.Body.Rotation = 0;
            character.Body.Position = characterInitPos;
        }
        #endregion

        #region Character_Falls
        bool Character_Falls(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.Body.UserData == "pit")
            {
                // reset the character
                ResetCharacter();
            }

            return true;
        }
        #endregion

        #region Character_Reaches_Goal
        bool Character_Reaches_Goal(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.Body.UserData == "goal")
            {
                ResetCharacter();
                //levelCleared = true;

                currentLevel++;
                if (currentLevel > 3)
                {
                    ResetCharacter();
                }
                else
                {
                    CreateGameComponents();
                }
            }

            return true;
        }
        #endregion

        #region CreateGameComponents
        private void CreateGameComponents()
        {
            world.Clear();
            HalfScreenWidth = graphics.GraphicsDevice.Viewport.Width / 2;

            ReadInLevels();

            // Setup the camera
            Camera.Current.StartTracking(character.Body);
            Camera.Current.CenterPointTarget = ConvertUnits.ToDisplayUnits(
                goal.Body.Position.X);

            // event listeners
            character.Body.OnCollision += Character_Falls;
            character.Body.OnCollision += Character_Reaches_Goal;
        }
        #endregion

        #region ReadInLevels
        private void ReadInLevels()
        {
            grounds = new List<Ground>();
            pits = new List<Pit>();
            borders = new List<Border>();
            barriers = new List<Barrier>();

            Texture2D texture;
            Vector2 location;
            
            string level = "level" + currentLevel + ".txt";
            //read in the file
            System.IO.StreamReader worldFile = new System.IO.StreamReader(Content.RootDirectory + @"/levels/" + level);
            string numberOfLines = worldFile.ReadLine();
            string lengthOfLine = worldFile.ReadLine();

            for (int i = 0; i < Convert.ToInt32(numberOfLines); i++)
            {
                string line = worldFile.ReadLine();
                for (int k = 0; k < Convert.ToInt32(lengthOfLine); k++)
                {
                    char piece = line[k];
                    if (piece == ' ')
                    { }

                    // borders
                    else if (piece == '#')
                    {
                        string img = "";
                        switch (currentLevel)
                        {
                            case 1:
                                {
                                    img = "images\\sideBorder";
                                }
                                break;
                            case 2:
                                {
                                    img = "images\\iceBorder";
                                }
                                break;
                            case 3:
                                {
                                    img = "images\\sandGround";
                                }
                                break;
                        }
                        texture = Content.Load<Texture2D>(img);
                        location = new Vector2((float)k / 2, (float)i / 2);
                        border = new Border(world, texture, location);
                        borders.Add(border);
                    }

                    // ground
                    else if (piece == '^')
                    {
                        string img = "";
                        switch (currentLevel)
                        {
                            case 1:
                                {
                                    img = "images\\grassyGround";
                                }
                                break;
                            case 2:
                                {
                                    img = "images\\iceGround";
                                }
                                break;
                            case 3:
                                {
                                    img = "images\\sandGround";
                                }
                                break;
                        }
                        texture = Content.Load<Texture2D>(img);
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // yellow ground
                    else if (piece == 'Y')
                    {
                        texture = Content.Load<Texture2D>("images\\yellowGround");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // orange ground
                    else if (piece == 'O')
                    {
                        texture = Content.Load<Texture2D>("images\\orangeGround");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // platform
                    else if (piece == 't')
                    {
                        string img = "";
                        switch (currentLevel)
                        {
                            case 1:
                                {
                                    img = "images\\transparentGround";
                                }
                                break;
                            case 2:
                                {
                                    img = "images\\icePlatform";
                                }
                                break;
                            case 3:
                                {
                                    img = "images\\sandPlatform";
                                }
                                break;
                        }
                        texture = Content.Load<Texture2D>(img);
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // pit
                    else if (piece == '_')
                    {
                        texture = Content.Load<Texture2D>(@"images\pitSensor");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        pit = new Pit(world, texture, location);
                        pits.Add(pit);
                    }

                    // goal
                    else if (piece == 'G')
                    {
                        texture = Content.Load<Texture2D>(@"images\goal");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        goal = new Goal(world, texture, location);
                    }

                    // barrier
                    else if (piece == 'b')
                    {
                        texture = Content.Load<Texture2D>("images\\barrier");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        barrier = new Barrier(world, texture, location);
                        barriers.Add(barrier);
                    }

                    // player/character
                    else if (piece == 'P')
                    {
                        texture = Content.Load<Texture2D>(@"images\kirby");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        characterInitPos = location;
                        character = new Character(world, texture, location);
                    }
                }
            }
        }
        #endregion

        #region HandleKeyboard
        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();

            //move left
            if(state.IsKeyDown(Keys.Left)&& !isStone)
            {
                //character.Body.ApplyTorque(-0.05f);
                character.Body.Position -= new Vector2(0.05f, 0f);
                //if (character.Taps > -2)
                //{
                //    character.Body.ApplyForce(new Vector2(-15f, 0f));
                //    character.Taps -= 1;
                //}
                
            }

            //move right
            if (state.IsKeyDown(Keys.Right) && !isStone)
            {
                //character.Body.ApplyTorque(0.05f);
                character.Body.Position += new Vector2(0.05f, 0f);
                //if (character.Taps < 2)
                //{
                //    character.Body.ApplyForce(new Vector2(15f, 0f));
                //    character.Taps += 1;
                //}
            }

            //jump
            if ((state.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space)) || (state.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up)))
            {
                character.Jump();
                jump.Play();
            }

            //stone
            if (state.IsKeyDown(Keys.Down))
            {
                if(!isStone)
                {
                    attack.Play();
                }
                isStone = true;
                character.Body.ResetDynamics();
                character.Body.Rotation = 0;
                character.Body.ApplyForce(new Vector2(0, 30f));
                //character.Taps = 0;
            }

            //is not stone
            if (state.IsKeyUp(Keys.Down))
            {
                isStone = false;
            }

            //change levels
            if (state.IsKeyDown(Keys.D1) && state.IsKeyDown(Keys.LeftShift))
            {
                currentLevel = 1;
                CreateGameComponents();
            }
            if (state.IsKeyDown(Keys.D2) && state.IsKeyDown(Keys.LeftShift))
            {
                currentLevel = 2;
                CreateGameComponents();
            }
            if (state.IsKeyDown(Keys.D3) && state.IsKeyDown(Keys.LeftShift))
            {
                currentLevel = 3;
                CreateGameComponents();
            }

            //reset character
            if (state.IsKeyDown(Keys.R))
            {
                ResetCharacter();
            }

            oldKeyState = state;
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (instance.State == SoundState.Stopped)
            {
                instance.Play();
            }

            HandleKeyboard();

            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            // Allow camera to update
            Camera.Current.Update();

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null,
                null, Camera.Current.TransformationMatrix);
            
            if (victoryScreenIsPlaying)
            {
                DrawVictoryScreen();
            }
            else
            {
                DrawBackground();
                DrawWorld();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region DrawBackground
        private void DrawBackground()
        {
            Rectangle screen = new Rectangle(0, 0, 3000, screenHeight);
            if (currentLevel == 1)
            {
                spriteBatch.Draw(background1, screen, Color.White);
            }
            else if(currentLevel == 2)
            {
                spriteBatch.Draw(background2, screen, Color.White);
            }
            else if (currentLevel >= 3)
            {
                spriteBatch.Draw(background3, screen, Color.White);
            }
        }
        #endregion

        #region DrawWorld
        private void DrawWorld()
        {
            //draw goal
            goal.Draw(spriteBatch);

            //draw borders
            foreach (Border b in borders)
            {
                b.Draw(spriteBatch);
            }

            //draw grounds
            foreach (Ground g in grounds)
            {
                g.Draw(spriteBatch);
            }

            //draw pits
            foreach (Pit p in pits)
            {
                p.Draw(spriteBatch);
            }

            //draw barriers
            foreach (Barrier b in barriers)
            {
                b.Draw(spriteBatch);
            }

            //draw character
            if (isStone)
            {
                spriteBatch.Draw(stoneSprite,
                    ConvertUnits.ToDisplayUnits(character.Body.Position - stoneOrigin),
                    null, Color.White, 0,
                    stoneOrigin,
                    1f, SpriteEffects.None, 0f);
            }
            else
            {
                character.Draw(spriteBatch);
            }
        }
        #endregion

        #region DrawVictoryScreen
        private void DrawVictoryScreen()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(victoryScreen, screen, Color.White);
        }
        #endregion
    }
}