/* 2D Platformer Project
 * Super Kirbario
 * Created by Mallory Eaton and Sam Hipp
 * 
 * Sprites and Images
 * http://spritedatabase.net/custom/126
 * http://4.bp.blogspot.com/-LIQG1czOYtI/UGOEIE_Rd5I/AAAAAAAARrQ/4dQdV_5Rnx0/s1600/kirby+wallpaper.jpg
 * http://fc07.deviantart.net/fs40/f/2009/026/4/2/Sad_Kirby_by_supernintendogirl.png
 * http://wiiuforums.com/attachments/wii-wii-mini/1570d1361208467-***win-free-kirby-game-wii-vc-contest-details-inside-***-20130120070658-kirby.jpg
 * http://images4.alphacoders.com/359/35916.jpg
 * http://2.envato-static.com/assets/common/icons-buttons/rating/star-on-24a8d2589eca9f34bc2ff72a59bd9af7.png
 * http://www.spriters-resource.com/ds/kirbysqueaksquad/sheet/60929/
 * http://www.spriters-resource.com/snes/kirbydream3/sheet/2843/
 * http://www.spriters-resource.com/snes/kirbydream3/sheet/2818/
 * http://www.spriters-resource.com/snes/kirbydream3/sheet/2842/
 * http://spritedatabase.net/file/3519
 * http://spritedatabase.net/file/1153
 * http://www.nindb.net/gba/kirby-nightmare-in-dreamland/index.html
 * http://www.polyvore.com/invincibility_candy_kirby/thing?id=62396938
 * http://ksr.smackjeeves.com/comics/1441945/some-kdl3-tile-rips/
 * http://melonjs.github.io/tutorial-platformer/media/spinning_coin_gold.png
 * http://www.pixeljoint.com/files/icons/original.gif
 * 
 * Sounds and Music
 * http://downloads.khinsider.com/game-soundtracks/album/kirby-s-dreamland-3-original-soundtrack
 * http://www.sounds-resource.com/game_boy_advance/mariobros/
 * 
 * Code
 * http://rbwhitaker.wikidot.com/2d-particle-engine-1
 */

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
        #region Public Components
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static SpriteFont font;
        public static SpriteFont smallFont;

        public int timeFromLevelStart = 0;
        public double timeAdjustment = 0.0;
        public int screenWidth;
        public int screenHeight;
        public static float characterY { get; private set; }
        public static float HalfScreenWidth { get; private set; }
        public static int lives { get; private set; }
        public static double timerSec { get; private set; }
        public static double startTimeSec { get; private set; }
        public static int score { get; private set; }
        public static bool titleScreenIsPlaying { get; private set; }
        public static int currentLevel { get; private set; }
        public static int enemyNum { get; private set; }
        public static int coinNum { get; private set; }
        #endregion

        #region Private Components
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
        private InvincibleCandy invincibleCandy;
        private Platform platform;
        private List<Platform> platforms;
        private Coin coin;
        private List<Coin> coins;

        //temp variables
        private int startingScore;
        private int enemyStartingNum;
        private int coinStartingNum;

        //particles
        private ParticleEngine particleEngine;
        private Texture2D particleTexture;

        //text
        private DrawText drawText;

        //keyboard handling
        private KeyboardState oldKeyState;

        //backgrounds
        private Texture2D background1;
        private Texture2D background2;
        private Texture2D background3;

        //level cleared Screen
        private Texture2D levelClearedScreen;

        //lose screen
        private Texture2D loseScreen;

        //win screen
        private Texture2D winScreen;

        //title screen
        private Texture2D titleScreen;

        //sound effects and music
        private SoundEffect jump;
        private SoundEffect attack;
        private SoundEffect enemyDie;
        private SoundEffect collectCoin;
        private SoundEffect grassLandMusic;
        private SoundEffect iceLandMusic;
        private SoundEffect sandLandMusic;
        SoundEffectInstance level1Instance;
        SoundEffectInstance level2Instance;
        SoundEffectInstance level3Instance;
        private SoundEffect levelCleared;
        SoundEffectInstance levelClearedInstance;
        private SoundEffect invincible;
        SoundEffectInstance invincibleInstance;
        private SoundEffect gameOverSound;
        SoundEffectInstance gameOverSoundInstance;
        private SoundEffect die;
        SoundEffectInstance dieInstance;
        private SoundEffect win;
        SoundEffectInstance winInstance;
        private SoundEffect titleScreenSound;
        SoundEffectInstance titleScreenInstance;

        //sound stuff
        private bool LCHasBeenPlayed = false;
        private bool deadHasBeenPlayed = false;
        private bool winHasBeenPlayed = false;
        private bool lostLifeHasBeenPlayed = false;
        private int invincibleNumberOfTimesPlayed = 0;

        //game over
        private bool gameOver = false;

        //win screen
        private bool gameWon = false;

        //level cleared screen
        private bool levelIsCleared = false;

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
            currentLevel = 1;
            lives = 3;

            titleScreenIsPlaying = true;

            HalfScreenWidth = graphics.GraphicsDevice.Viewport.Width / 2;

            //screen width and height
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            drawText = new DrawText();

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
            //particles
            particleTexture = Content.Load<Texture2D>(@"images\starParticle");
            particleEngine = new ParticleEngine(particleTexture, new Vector2(400, 240));

            //fonts
            font = Content.Load<SpriteFont>(@"font\myFont");
            smallFont = Content.Load<SpriteFont>(@"font\small");

            //backgrounds
            background1 = Content.Load<Texture2D>(@"images/grassBackground");
            background2 = Content.Load<Texture2D>(@"images/iceBackground");
            background3 = Content.Load<Texture2D>(@"images/sandBackground");

            //screens
            levelClearedScreen = Content.Load<Texture2D>(@"images/kirbyLevelCleared");
            loseScreen = Content.Load<Texture2D>(@"images/kirbyLose");
            winScreen = Content.Load<Texture2D>(@"images/kirbyWin");
            titleScreen = Content.Load<Texture2D>(@"images/titleScreen");

            //sound effects and music
            jump = Content.Load<SoundEffect>(@"sounds/jump");
            attack = Content.Load<SoundEffect>(@"sounds/stone");
            enemyDie = Content.Load<SoundEffect>(@"sounds/enemyDie");
            collectCoin = Content.Load<SoundEffect>(@"sounds/coin");

            grassLandMusic = Content.Load<SoundEffect>(@"sounds/grassLandMusic");
            level1Instance = grassLandMusic.CreateInstance();

            iceLandMusic = Content.Load<SoundEffect>(@"sounds/iceLandMusic");
            level2Instance = iceLandMusic.CreateInstance();

            sandLandMusic = Content.Load<SoundEffect>(@"sounds/sandLandMusic");
            level3Instance = sandLandMusic.CreateInstance();

            levelCleared = Content.Load<SoundEffect>(@"sounds/levelCleared");
            levelClearedInstance = levelCleared.CreateInstance();

            invincible = Content.Load<SoundEffect>(@"sounds/invincible");
            invincibleInstance = invincible.CreateInstance();

            gameOverSound = Content.Load<SoundEffect>(@"sounds/gameOver");
            gameOverSoundInstance = gameOverSound.CreateInstance();

            die = Content.Load<SoundEffect>(@"sounds/die");
            dieInstance = die.CreateInstance();

            win = Content.Load<SoundEffect>(@"sounds/win");
            winInstance = win.CreateInstance();

            titleScreenSound = Content.Load<SoundEffect>(@"sounds/titleScreen");
            titleScreenInstance = titleScreenSound.CreateInstance();

            //create world
            CreateGameComponents();
        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {}
        #endregion

        #region ResetLevel
        private void ResetLevel()
        {
            if(gameOver || gameWon)
            {
                startingScore = 0;
                enemyStartingNum = 0;
                coinStartingNum = 0;

                lives = 3;
                currentLevel = 1;

                timeAdjustment = 0;
            }

            gameOver = false;
            gameWon = false;
            cheatIsOn = false;
            character.IsInvincible = false;
            character.LosesLife = false;
            levelIsCleared = false;

            LCHasBeenPlayed = false;
            deadHasBeenPlayed = false;
            winHasBeenPlayed = false;
            lostLifeHasBeenPlayed = false;

            score = startingScore;
            enemyNum = enemyStartingNum;
            coinNum = coinStartingNum;

            StopMusic();

            levelClearedInstance.Stop();
            gameOverSoundInstance.Stop();
            dieInstance.Stop();
            winInstance.Stop();

            invincibleNumberOfTimesPlayed = 0;

            CreateGameComponents();
        }
        #endregion

        #region PlayMusic
        private void PlayMusic()
        {
            if (!character.LosesLife && !gameOver)
            {
                if (currentLevel == 1 && level1Instance.State == SoundState.Stopped && !titleScreenIsPlaying)
                {
                    level2Instance.Stop();
                    level3Instance.Stop();
                    level1Instance.Play();
                }
                if (currentLevel == 2 && level2Instance.State == SoundState.Stopped && !titleScreenIsPlaying)
                {
                    level1Instance.Stop();
                    level3Instance.Stop();
                    level2Instance.Play();
                }
                if (currentLevel >= 3 && level3Instance.State == SoundState.Stopped && !titleScreenIsPlaying)
                {
                    level1Instance.Stop();
                    level2Instance.Stop();
                    level3Instance.Play();
                }
            }
        }
        #endregion

        #region StopMusic
        private void StopMusic()
        {
            invincibleInstance.Stop();
            level1Instance.Stop();
            level2Instance.Stop();
            level3Instance.Stop();
        }
        #endregion

        #region PlayScreens
        private void PlayScreens()
        {
            //title screen
            if (titleScreenInstance.State == SoundState.Stopped && titleScreenIsPlaying)
            {
                titleScreenInstance.Play();
            }

            //level cleared screen
            if (levelIsCleared)
            {
                StopMusic();
                if (!LCHasBeenPlayed)
                {
                    levelClearedInstance.Play();
                    LCHasBeenPlayed = true;
                }
            }

            //win screen
            if (gameWon)
            {
                StopMusic();
                if (!winHasBeenPlayed)
                {
                    winInstance.Play();
                    winHasBeenPlayed = true;
                }
            }

            //game over screen
            if (gameOver && lostLifeHasBeenPlayed)
            {
                if (!deadHasBeenPlayed)
                {
                    gameOverSoundInstance.Play();
                    deadHasBeenPlayed = true;
                }
            }
        }
        #endregion

        #region Collisions
        bool Character_Collision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            //pit
            if (fixtureB.Body.UserData == "pit" && !character.LosesLife)
            {
                character.LosesLife = true;
                character.IsInvincible = false;
                lives--;
                character.Body.ResetDynamics();
                character.Body.CollisionCategories = Category.Cat5;
                character.Body.ApplyLinearImpulse(new Vector2(0, -0.25f));
            }

            //ground
            if (fixtureB.Body.UserData == "ground")
            {
                character.jumpNum = 0;
            }

            //platform
            if (fixtureB.Body.UserData == "platform")
            {

                if (character.Body.LinearVelocity.Y > 0 || character.Body.LinearVelocity.Y == 0)
                {
                    character.jumpNum = 0;
                }
            }

            //goal
            if (fixtureA.Body.UserData == "player" && fixtureB.Body.UserData == "goal")
            {
                character.OnGoal = true;
            }

            //invincible candy
            if (fixtureB.Body.UserData == "candy" && !character.IsInvincible)
            {
                invincibleCandy.Destroy();
                character.IsInvincible = true;
            }

            //enemy
            if (fixtureB.Body.UserData.GetType().BaseType == typeof(Enemy))
            {
                if (!character.IsStone && !character.IsInvincible && !cheatIsOn)
                {
                    character.LosesLife = true;
                    lives--;
                    dieInstance.Play();
                    character.Body.ResetDynamics();
                    character.Body.CollisionCategories = Category.Cat5;
                    character.Body.ApplyLinearImpulse(new Vector2(0, -0.25f));
                }
                else if (character.IsStone && character.Body.LinearVelocity.Y > 0)
                {
                    character.Body.ResetDynamics();
                    character.Body.ApplyLinearImpulse(new Vector2(0f, -0.2f));
                }
                else if (character.IsStone && fixtureB.Body.Position.X > character.Body.Position.X)
                {
                    character.Body.ApplyLinearImpulse(new Vector2(-0.05f, -0.05f));
                }
                else if (character.IsStone && fixtureB.Body.Position.X < character.Body.Position.X)
                {
                    character.Body.ApplyLinearImpulse(new Vector2(0.05f, -0.05f));
                }
            }

            return true;
        }

        bool WaddleDee_Collision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            WaddleDee w = (WaddleDee)fixtureA.Body.UserData;
            if (fixtureB.Body.UserData == "player" &&
                ((character.IsStone && character.Body.LinearVelocity.Y > 0) || character.IsInvincible) && !cheatIsOn)
            {
                w.Die();
                enemyDie.Play();
                score += 100;
                enemyNum++;
                //return false;
            }

            else if (fixtureB.Body.UserData != "ground" && fixtureB.Body.UserData != "platform")
            {
                w.ChangeDirection();
            }

            return true;
        }

        bool Burt_Collision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            Burt b = (Burt)fixtureA.Body.UserData;
            if (fixtureB.Body.UserData == "player" &&
                ((character.IsStone && character.Body.LinearVelocity.Y > 0) || character.IsInvincible) && !cheatIsOn)
            {
                enemyDie.Play();
                b.Die();
                score += 200;
                enemyNum++;
            }
            return true;
        }

        bool Coin_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            Coin c = (Coin)fixtureA.Body.UserData;
            if (fixtureB.Body.UserData == "player")
            {
                c.Destroy();
                score += 50;
                coins.Remove(c);
                collectCoin.Play();
                coinNum++;
            }
            return true;
        }
        #endregion

        #region CreateGameComponents
        private void CreateGameComponents()
        {
            world.Clear();

            startingScore = score;
            coinStartingNum = coinNum;
            enemyStartingNum = enemyNum;

            if (invincibleCandy != null)
            {
                invincibleCandy.isCandy = false;
            }

            ReadInLevels();

            // Setup the camera
            Camera.Current.StartTracking(character.Body);
            Camera.Current.CenterPointTarget = ConvertUnits.ToDisplayUnits(
                goal.Body.Position.X);

            // event listeners
            character.Body.OnCollision += Character_Collision;
            foreach (WaddleDee w in waddleDees)
            {
                w.Body.OnCollision += WaddleDee_Collision;
            }
            foreach (Burt b in burts)
            {
                b.Body.OnCollision += Burt_Collision;
            }
            foreach (Coin c in coins)
            {
                c.Body.OnCollision += Coin_OnCollision;
            }
        }
        #endregion

        #region ReadInLevels
        private void ReadInLevels()
        {
            grounds = new List<Ground>();
            pits = new List<Pit>();
            borders = new List<Border>();
            barriers = new List<Barrier>();
            waddleDees = new List<WaddleDee>();
            burts = new List<Burt>();
            platforms = new List<Platform>();
            coins = new List<Coin>();

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

                    // coin
                    else if (piece == 'c')
                    {
                        texture = Content.Load<Texture2D>(@"images\coin");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        coin = new Coin(world, texture, location);
                        coins.Add(coin);
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

                    // platform
                    else if (piece == '-')
                    {
                        string img = "";
                        switch (currentLevel)
                        {
                            case 1:
                                {
                                    img = "images\\grassPlatform";
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
                        platform = new Platform(world, texture, location);
                        platforms.Add(platform);
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

                    // invincibility candy
                    else if (piece == 'i')
                    {
                        texture = Content.Load<Texture2D>(@"images\invincibleCandy");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        invincibleCandy = new InvincibleCandy(world, texture, location);
                        invincibleCandy.isCandy = true;
                    }

                    //waddleDee
                    else if (piece == 'W')
                    {
                        texture = Content.Load<Texture2D>(@"images\waddleDee");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        waddleDee = new WaddleDee(world, texture, location);
                        waddleDees.Add(waddleDee);
                    }

                    //burt
                    else if (piece == 'B')
                    {
                        texture = Content.Load<Texture2D>(@"images\burt");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        burt = new Burt(world, texture, location);
                        burts.Add(burt);
                    }

                    // player/character
                    else if (piece == 'P')
                    {
                        texture = Content.Load<Texture2D>(@"images\kirby");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        Texture2D invincible = Content.Load<Texture2D>(@"images/kirbyInvincible");
                        Texture2D stone = Content.Load<Texture2D>(@"images/kirbyStone");
                        character = new Character(world, texture, invincible, stone, location);
                        character.characterInitPos = location;
                    }
                }
            }
        }
        #endregion

        #region HandleKeyboard
        private void HandleKeyboard(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //press enter
            if (state.IsKeyDown(Keys.Enter) && (titleScreenIsPlaying || levelIsCleared 
                || gameOver || (character.LosesLife && lives != 0) || gameWon))
            {
                if (titleScreenIsPlaying)
                {
                    titleScreenInstance.Stop();
                    titleScreenIsPlaying = false;
                    CreateGameComponents();
                    startTimeSec = gameTime.TotalGameTime.TotalSeconds;
                }

                if (gameOver)
                {
                    startTimeSec = gameTime.TotalGameTime.TotalSeconds;
                }

                if (gameWon)
                {
                    startTimeSec = gameTime.TotalGameTime.TotalSeconds;
                }

                ResetLevel();
            }

            if (!titleScreenIsPlaying && !levelIsCleared && !gameOver 
                && !character.LosesLife && !gameWon)
            {
                //move left
                if (state.IsKeyDown(Keys.Left) && !character.IsStone)
                {
                    if (state.IsKeyDown(Keys.LeftShift))
                    {
                        if (character.IsInvincible)
                        {
                            character.Body.Position -= new Vector2(0.08f, 0f);
                        }
                        else
                        {
                            character.Body.Position -= new Vector2(0.06f, 0f);
                        }
                    }
                    else
                    {
                        if (character.IsInvincible)
                        {
                            character.Body.Position -= new Vector2(0.06f, 0f);
                        }
                        else
                        {
                            character.Body.Position -= new Vector2(0.04f, 0f);
                        }
                    }
                }

                //move right
                if (state.IsKeyDown(Keys.Right) && !character.IsStone)
                {
                    if (state.IsKeyDown(Keys.LeftShift))
                    {
                        if (character.IsInvincible)
                        {
                            character.Body.Position += new Vector2(0.08f, 0f);
                        }
                        else
                        {
                            character.Body.Position += new Vector2(0.06f, 0f);
                        }
                    }
                    else
                    {
                        if (character.IsInvincible)
                        {
                            character.Body.Position += new Vector2(0.06f, 0f);
                        }
                        else
                        {
                            character.Body.Position += new Vector2(0.04f, 0f);
                        }
                    }
                }

                //jump
                if (!character.IsStone && (state.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up))
                     && character.jumpNum < 6)
                {
                    character.jumpNum++;
                    if (character.IsInvincible)
                    {
                        character.Jump(new Vector2(0, -0.3f));
                    }
                    else
                    {
                        character.Jump(character.jumpForce);
                    }

                    jump.Play();
                }

                //enter goal
                if (!character.IsStone && character.OnGoal && (state.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space)))
                {
                    if (currentLevel <= 2)
                    {
                        levelIsCleared = true;
                    }
                    currentLevel++;
                    if (currentLevel > 3)
                    {
                        gameWon = true;
                        currentLevel = 3;
                    
                        CreateGameComponents();
                        character.Body.CollisionCategories = Category.Cat5;
                    }
                    else
                    {
                    
                        CreateGameComponents();
                        character.Body.CollisionCategories = Category.Cat5;
                    }
                }

                //win cheat
                if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.LeftShift))
                {
                    if (currentLevel <= 2)
                    {
                        levelIsCleared = true;
                    }
                    currentLevel++;
                    if (currentLevel > 3)
                    {
                        gameWon = true;
                        currentLevel = 3;
                        enemyNum = 23;
                        coinNum = 47;
                        CreateGameComponents();
                        character.Body.CollisionCategories = Category.Cat5;
                    }
                    else
                    {
                    
                        CreateGameComponents();
                        character.Body.CollisionCategories = Category.Cat5;
                    }
                }

                //stone
                if (state.IsKeyDown(Keys.Down) && oldKeyState.IsKeyUp(Keys.Down) && !character.IsInvincible)
                {
                    if (!character.IsStone)
                    {
                        attack.Play();
                    }
                    character.IsStone = true;
                    character.Body.ResetDynamics();
                    character.Body.Rotation = 0;
                    character.Body.ApplyForce(new Vector2(0, 30f));
                }

                //is not stone
                if (state.IsKeyUp(Keys.Down))
                {
                    character.IsStone = false;
                }

                //change levels
                if (state.IsKeyDown(Keys.D1) && state.IsKeyDown(Keys.LeftShift))
                {
                    currentLevel = 1;
                    ResetLevel();
                }
                if (state.IsKeyDown(Keys.D2) && state.IsKeyDown(Keys.LeftShift))
                {
                    currentLevel = 2;
                    ResetLevel();
                }
                if (state.IsKeyDown(Keys.D3) && state.IsKeyDown(Keys.LeftShift))
                {
                    currentLevel = 3;
                    ResetLevel();
                }
               
                //reset character
                if (state.IsKeyDown(Keys.R))
                {
                    ResetLevel();
                }

                // ? remove enemies
                if (state.IsKeyDown(Keys.OemQuestion) && oldKeyState.IsKeyUp(Keys.OemQuestion))
                {
                    if(cheatIsOn)
                    {
                        cheatIsOn = false;
                        foreach (Burt b in burts)
                        {
                            b.Body.CollisionCategories = Category.Cat3;
                        }
                        foreach (WaddleDee w in waddleDees)
                        {
                            w.Body.CollisionCategories = Category.Cat3;
                        }
                    }
                    else
                    {
                        cheatIsOn = true;
                        foreach (Burt b in burts)
                        {
                            b.Body.CollisionCategories = Category.Cat4;
                        }
                        foreach (WaddleDee w in waddleDees)
                        {
                            w.Body.CollisionCategories = Category.Cat4;
                        }
                    }

                    
                }
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
            PlayMusic();
            PlayScreens();

            characterY = character.Body.Position.Y;
            timerSec = gameTime.TotalGameTime.TotalSeconds - startTimeSec - timeAdjustment;

            //particle engine
            particleEngine.EmitterLocation = new Vector2(ConvertUnits.ToDisplayUnits(character.Body.Position.X),
                ConvertUnits.ToDisplayUnits(character.Body.Position.Y));
            particleEngine.Update();

            //lose a life
            if (character.LosesLife)
            {
                StopMusic();

                if (!lostLifeHasBeenPlayed)
                {
                    dieInstance.Play();
                    lostLifeHasBeenPlayed = true;
                }

                else if (lives == 0 && dieInstance.State == SoundState.Stopped)
                {
                    CreateGameComponents();
                    character.Body.IsSensor = true;
                    gameOver = true;
                }
            }

            //adjust time
            if (character.LosesLife || levelIsCleared || gameWon)
            {
                timeAdjustment += gameTime.ElapsedGameTime.TotalSeconds;
            }

            #region invincible
            if (character.IsInvincible)
            {
                level1Instance.Pause();
                level2Instance.Pause();
                level3Instance.Pause();

                if (invincibleNumberOfTimesPlayed <= 2)
                {
                    if (invincibleInstance.State == SoundState.Stopped)
                    {
                        invincibleInstance.Play();
                        invincibleNumberOfTimesPlayed++;
                    }
                }
                else
                {
                    invincibleInstance.Stop();
                    character.IsInvincible = false;
                    invincibleNumberOfTimesPlayed = 0;

                    switch (currentLevel)
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
                }
            }
            #endregion

            #region enemy movement
            foreach (WaddleDee w in waddleDees)
            {
                w.Update(gameTime);               
            }

            foreach (Burt b in burts)
            {
                b.Update(gameTime);
            }

            foreach (Coin c in coins)
            {
                c.Update(gameTime);
            }
            #endregion

            HandleKeyboard(gameTime);

            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

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

            //parallax with value of 5
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null,
                null, Camera.Current.GetViewMatrix(new Vector2(.25f, .25f)));

            if(titleScreenIsPlaying)
            {
                DrawTitleScreen();
            }
            else if (levelIsCleared && currentLevel <= 3)
            {
                DrawLevelClearedScreen();
            }
            else if (gameOver)
            {
                DrawGameOverScreen();
            }
            else if(gameWon)
            {
                DrawWinScreen();
            }
            else
            {
                DrawBackground();
                spriteBatch.End();

                //no parallax for this layer
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null,
                null, Camera.Current.GetViewMatrix(Vector2.One));
                DrawWorld();
                spriteBatch.End();

                //no camera adjustment, relative only to screen
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null,
                null, Camera.Current.GetViewMatrix(Vector2.Zero));
                drawText.DrawLivesTimeScore(spriteBatch);
                
                if (character.LosesLife && lives != 0)
                {
                    Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
                    drawText.DrawLoseLife(spriteBatch, GraphicsDevice);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region DrawBackground
        private void DrawBackground()
        {
            Rectangle screen = new Rectangle(0, 0, 2000, screenHeight);
            if (currentLevel == 1)
            {
                spriteBatch.Draw(background1, screen, Color.White);
            }
            else if (currentLevel == 2)
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

            //draw platforms
            foreach(Platform p in platforms)
            {
                p.Draw(spriteBatch);
            }

            //draw coins
            foreach (Coin c in coins)
            {
                if (c.isAlive)
                {
                    c.Draw(spriteBatch);
                }
            }

            //draw pits
            foreach (Pit p in pits)
            {
                p.Draw(spriteBatch);
            }

            //draw candy
            if (invincibleCandy != null)
            {
                if (invincibleCandy.isCandy && invincibleCandy.isAlive)
                {
                    invincibleCandy.Draw(spriteBatch);
                }
            }

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

            //draw enemies
            if (!cheatIsOn)
            {
                foreach (WaddleDee w in waddleDees)
                {
                    w.Draw(spriteBatch);
                }
                foreach (Burt b in burts)
                {
                    b.Draw(spriteBatch);
                }
            }

            //draw particles
            if (character.IsInvincible)
            {
                particleEngine.Draw(spriteBatch);
            }

            //draw character           
            character.Draw(spriteBatch);
            
        }
        #endregion

        #region DrawLevelClearedScreen
        private void DrawLevelClearedScreen()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(levelClearedScreen, screen, Color.White);
            drawText.DrawLevelCleared(spriteBatch);
        }
        #endregion

        #region DrawGameOverScreen
        private void DrawGameOverScreen()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(loseScreen, screen, Color.White);
            drawText.DrawGameOver(spriteBatch);
        }
        #endregion

        #region DrawWinScreen
        private void DrawWinScreen()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(winScreen, screen, Color.White);
            drawText.DrawWinScreen(spriteBatch);
        }
        #endregion

        #region DrawTitleScreen
        private void DrawTitleScreen ()
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(titleScreen, screen, Color.White);
        }
        #endregion
    }
}