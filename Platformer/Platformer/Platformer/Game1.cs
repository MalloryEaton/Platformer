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
        #region Components
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static SpriteFont font;

        public int screenWidth;
        public int screenHeight;
        public static float HalfScreenWidth { get; private set; }

        private Vector2 jumpForce = new Vector2(0, -0.25f); // applied force when jumping

        //world objects
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
        private WaddleDee waddleDee;
        private List<WaddleDee> waddleDees;
        private Burt burt;
        private List<Burt> burts;
        private InvinvibleCandy invincibleCandy;
        private LevelClearedScreen levelClearedScreen;
        private LoseGameScreen loseGameScreen;

        //keyboard handling
        private KeyboardState oldKeyState;

        //stone
        public bool isStone = false;
        private Texture2D stoneSprite;
        private Body stoneBody;
        private Vector2 stoneOrigin;

        //invincible
        private bool gotCandy = false;
        private bool isInvincible = false;

        //backgrounds
        private Texture2D background1;
        private Texture2D background2;
        private Texture2D background3;

        //victory Screen
        private Texture2D victoryScreen;

        //lose screen
        private Texture2D loseScreen;

        //sound effects and music
        private SoundEffect jump;
        private SoundEffect attack;
        private SoundEffect grassLandMusic;
        private SoundEffect iceLandMusic;
        private SoundEffect sandLandMusic;
        SoundEffectInstance level1Instance;
        SoundEffectInstance level2Instance;
        SoundEffectInstance level3Instance;
        private SoundEffect victory;
        SoundEffectInstance victoryInstance;
        private SoundEffect invincible;
        SoundEffectInstance invincibleInstance;
        private SoundEffect gameOver;
        SoundEffectInstance gameOverInstance;

        private Vector2 characterInitPos;

        private bool isCandy = false;

        public static int currentLevel = 1;

        private bool LCHasBeenPlayed = false;
        private bool deadHasBeenPlayed = false;

        //level cleared screen
        private bool levelCleared = false;
        private bool levelClearedScreenIsPlaying = false;

        //timer for invincibility
        private double invincibilityTimer = 9.8;
        private const double INVINCIBILITYTIMER = 9.8;
        private double invincibilityElapsedTime;

        //? cheat
        private bool cheatIsOn = false;
        #endregion

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
            levelClearedScreen = new LevelClearedScreen();
            loseGameScreen = new LoseGameScreen();

            font = Content.Load<SpriteFont>(@"font\myFont");

            //screen width and height
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            //stone
            stoneSprite = Content.Load<Texture2D>(@"images/kirbyStone");
            stoneBody = BodyFactory.CreateBody(world);
            stoneOrigin = new Vector2(ConvertUnits.ToSimUnits(stoneSprite.Width / 2),
                ConvertUnits.ToSimUnits(stoneSprite.Height / 2));

            //backgrounds
            background1 = Content.Load<Texture2D>(@"images/grassBackground");
            background2 = Content.Load<Texture2D>(@"images/iceBackground");
            background3 = Content.Load<Texture2D>(@"images/sandBackground");

            victoryScreen = Content.Load<Texture2D>(@"images/kirbyWin");
            loseScreen = Content.Load<Texture2D>(@"images/kirbyLose");

            //sound effects
            jump = Content.Load<SoundEffect>(@"sounds/jump");
            attack = Content.Load<SoundEffect>(@"sounds/stone");

            //music
            grassLandMusic = Content.Load<SoundEffect>(@"sounds/grassLandMusic");
            level1Instance = grassLandMusic.CreateInstance();

            iceLandMusic = Content.Load<SoundEffect>(@"sounds/iceLandMusic");
            level2Instance = iceLandMusic.CreateInstance();

            sandLandMusic = Content.Load<SoundEffect>(@"sounds/sandLandMusic");
            level3Instance = sandLandMusic.CreateInstance();
           
            victory = Content.Load<SoundEffect>(@"sounds/victory");
            victoryInstance = victory.CreateInstance();

            invincible = Content.Load<SoundEffect>(@"sounds/invincible");
            invincibleInstance = invincible.CreateInstance();

            gameOver = Content.Load<SoundEffect>(@"sounds/gameOver");
            gameOverInstance = gameOver.CreateInstance();

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
            gotCandy = false;
            isInvincible = false;
            character.Body.ResetDynamics();
            character.Body.Rotation = 0;
            character.Body.Position = characterInitPos;
        }
        #endregion

        #region Character_Collision
        bool Character_Collision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            //pit
            if (fixtureB.Body.UserData == "pit")
            {
                character.lives--;
                if (character.lives == 0)
                {
                    character.isDead = true;
                }
                ResetCharacter();
            }

            //ground
            if (fixtureB.Body.UserData == "ground")
            {
                character.jumpNum = 0;
            }

            //goal
            if (fixtureB.Body.UserData == "goal")
            {
                if (currentLevel <= 2)
                {
                    levelCleared = true;
                }
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
            
            //tomato
            else if (fixtureB.Body.UserData == "tomato")
            {
                gotCandy = true;
                invincibleCandy.Destroy();
            }

            return true;
        }
        #endregion

        #region CreateGameComponents
        private void CreateGameComponents()
        {
            world.Clear();

            HalfScreenWidth = graphics.GraphicsDevice.Viewport.Width / 2;

            gotCandy = false;
            isInvincible = false;
            levelCleared = false;
            LCHasBeenPlayed = false;
            deadHasBeenPlayed = false;

            ReadInLevels();

            // Setup the camera
            Camera.Current.StartTracking(character.Body);
            Camera.Current.CenterPointTarget = ConvertUnits.ToDisplayUnits(
                goal.Body.Position.X);

            // event listeners
            character.Body.OnCollision += Character_Collision;
        }
        #endregion

        #region ReadInLevels
        private void ReadInLevels()
        {
            isCandy = false;

            grounds = new List<Ground>();
            pits = new List<Pit>();
            borders = new List<Border>();
            barriers = new List<Barrier>();
            waddleDees = new List<WaddleDee>();

            Texture2D texture;
            Vector2 location;
            
            string level = "level" + currentLevel + ".txt";
            //read in the file
            System.IO.StreamReader worldFile = new System.IO.StreamReader(@"Content/levels/" + level);
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
                                    img = "images\\pinkBorder";
                                }
                                break;
                            case 2:
                                {
                                    img = "images\\iceBorder";
                                }
                                break;
                            case 3:
                                {
                                    img = "images\\sandBorder";
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

                    // tomato
                    else if (piece == 'T')
                    {
                        texture = Content.Load<Texture2D>(@"images\invincibleCandy");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        invincibleCandy = new InvinvibleCandy(world, texture, location);
                        isCandy = true;
                    }

                    //waddleDee
                    //else if (piece == 'W')
                    //{
                    //    texture = Content.Load<Texture2D>(@"images\waddleDee");
                    //    location = new Vector2((float)k / 2, (float)i / 2);
                    //    waddleDee = new WaddleDee(world, texture, location);
                    //    waddleDees.Add(waddleDee);
                    //}

                    //burt
                    //else if (piece == 'B')
                    //{
                    //    texture = Content.Load<Texture2D>(@"images\burt");
                    //    location = new Vector2((float)k / 2, (float)i / 2);
                    //    burt = new Burt(world, texture, location);
                    //    burts.Add(burt);
                    //}

                    // player/character
                    else if (piece == 'P')
                    {
                        texture = Content.Load<Texture2D>(@"images\kirby");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        characterInitPos = location;
                        character = new Character(world, texture, location);
                        //invincible
                        character.invincibleTexture = Content.Load<Texture2D>(@"images/kirbyInvincible");
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
            if(state.IsKeyDown(Keys.Left) && !isStone && !levelClearedScreenIsPlaying && !character.isDead)
            {
                if (gotCandy)
                {
                    character.Body.Position -= new Vector2(0.06f, 0f);
                }
                else
                {
                    character.Body.Position -= new Vector2(0.04f, 0f);
                }
            }

            //move right
            if (state.IsKeyDown(Keys.Right) && !isStone && !levelClearedScreenIsPlaying && !character.isDead)
            {
                if (gotCandy)
                {
                    character.Body.Position += new Vector2(0.06f, 0f);
                }
                else
                {
                    character.Body.Position += new Vector2(0.04f, 0f);
                }
            }

            //jump
            if (((state.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space)) 
                || (state.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up)))
                 && !levelClearedScreenIsPlaying && !character.isDead && character.jumpNum < 5)
            {
                character.jumpNum++;
                if (gotCandy)
                {
                    character.Jump(new Vector2 (0, -0.3f));
                }
                else
                {
                    character.Jump(jumpForce);
                }
                jump.Play();
            }

            //stone
            if (state.IsKeyDown(Keys.Down) && !levelClearedScreenIsPlaying && !character.isDead)
            {
                if(!isStone)
                {
                    attack.Play();
                }
                isStone = true;
                character.Body.ResetDynamics();
                character.Body.Rotation = 0;
                character.Body.ApplyForce(new Vector2(0, 30f));
            }

            //is not stone
            if (state.IsKeyUp(Keys.Down))
            {
                isStone = false;
            }

            //change levels
            if (state.IsKeyDown(Keys.D1) && state.IsKeyDown(Keys.LeftShift) && !levelClearedScreenIsPlaying && !character.isDead)
            {
                currentLevel = 1;
                CreateGameComponents();
            }
            if (state.IsKeyDown(Keys.D2) && state.IsKeyDown(Keys.LeftShift) && !levelClearedScreenIsPlaying && !character.isDead)
            {
                currentLevel = 2;
                CreateGameComponents();
            }
            if (state.IsKeyDown(Keys.D3) && state.IsKeyDown(Keys.LeftShift) && !levelClearedScreenIsPlaying && !character.isDead)
            {
                currentLevel = 3;
                CreateGameComponents();
            }

            //press enter
            if (state.IsKeyDown(Keys.Enter) && (levelCleared || character.isDead))
            {
                victoryInstance.Stop();
                gameOverInstance.Stop();

                if (character.isDead)
                {
                    currentLevel = 1;
                }
                CreateGameComponents();
            }

            //reset character
            if (state.IsKeyDown(Keys.R) && !levelClearedScreenIsPlaying)
            {
                ResetCharacter();
            }

            // ? remove enemies
            if (state.IsKeyDown(Keys.OemQuestion) && oldKeyState.IsKeyUp(Keys.OemQuestion))
            {
                cheatIsOn = true;
                //remove enemies
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
            //music
            if (level1Instance.State == SoundState.Stopped && currentLevel == 1)
            {
                level2Instance.Stop();
                level3Instance.Stop();
                level1Instance.Play();
            }
            if (level2Instance.State == SoundState.Stopped && currentLevel == 2)
            {
                level1Instance.Stop();
                level3Instance.Stop();
                level2Instance.Play();
            }
            if (level3Instance.State == SoundState.Stopped && currentLevel >= 3)
            {
                level1Instance.Stop();
                level2Instance.Stop();
                level3Instance.Play();
            }

            //victory screen
            if (levelCleared)
            {
                invincibleInstance.Stop();
                level1Instance.Stop();
                level2Instance.Stop();
                level3Instance.Stop();

                if (!LCHasBeenPlayed)
                {
                    victoryInstance.Play();
                    LCHasBeenPlayed = true;
                }
            }

            //invincible
            if(gotCandy)
            {
                level1Instance.Pause();
                level2Instance.Pause();
                level3Instance.Pause();

                isInvincible = true;
                invincibilityElapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(isInvincible)
            {
                invincibleInstance.Play();
                invincibilityTimer -= invincibilityElapsedTime;
                if (invincibilityTimer <= 0)
                {
                    invincibleInstance.Stop();
                    switch(currentLevel)
                    {
                        case 1:
                            level1Instance.Resume();
                            break;
                        case 2:
                            level2Instance.Resume();
                            break;
                        case 3:
                            level3Instance.Resume();
                            break;
                    }
                    isInvincible = false;
                    gotCandy = false;
                    invincibilityTimer = INVINCIBILITYTIMER;
                }
            }

            //dead
            if(character.isDead)
            {
                invincibleInstance.Stop();
                level1Instance.Stop();
                level2Instance.Stop();
                level3Instance.Stop();
                if (!deadHasBeenPlayed)
                {
                    gameOverInstance.Play();
                    deadHasBeenPlayed = true;
                }
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

            if (levelCleared && currentLevel <= 3)
            {
                DrawVictoryScreen();
            }
            else if(character.isDead)
            {
                DrawLoseScreen();
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

            //draw tomato
            if (isCandy && invincibleCandy.IsAlive)
            {
                invincibleCandy.Draw(spriteBatch);
            }

            //draw enemies
            //if (!cheatIsOn)
            //{
            //    foreach (WaddleDee w in waddleDees)
            //    {
            //        w.Draw(spriteBatch);
            //    }
            //    foreach (Burt b in burts)
            //    {
            //        b.Draw(spriteBatch);
            //    }
            //}

            //draw character
            if (isStone)
            {
                spriteBatch.Draw(stoneSprite,
                    ConvertUnits.ToDisplayUnits(character.Body.Position - stoneOrigin),
                    null, Color.White, 0,
                    stoneOrigin,
                    1f, SpriteEffects.None, 0f);
            }
            else if(isInvincible)
            {
                character.DrawInvincible(spriteBatch);
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
            levelClearedScreen.Draw(spriteBatch);
        }
        #endregion

        #region DrawLoseScreen
        private void DrawLoseScreen()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(loseScreen, screen, Color.White);
            loseGameScreen.Draw(spriteBatch);
        }
        #endregion
    }
}