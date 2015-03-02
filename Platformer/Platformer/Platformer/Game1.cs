using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private World world;
        private Character character;
        private Ground ground;
        private List<Ground> grounds;
        private Goal goal;
        private Pit pit;
        private List<Pit> pits;

        private KeyboardState jumpOldKeyState;

        public bool isStone = false;
        private Texture2D stoneSprite;
        private Body stoneBody;
        private Vector2 stoneOrigin;

        public static float HalfScreenWidth { get; private set; }

        private Vector2 characterInitPos;

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

            stoneSprite = Content.Load<Texture2D>(@"images/kirbyStone");
            stoneBody = BodyFactory.CreateBody(world);
            stoneOrigin = new Vector2(ConvertUnits.ToSimUnits(stoneSprite.Width / 2),
                ConvertUnits.ToSimUnits(stoneSprite.Height / 2));

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

        #region CreateGameComponents (contains reading from file)
        private void CreateGameComponents()
        {
            world.Clear();
            grounds = new List<Ground>();
            pits = new List<Pit>();
            HalfScreenWidth = graphics.GraphicsDevice.Viewport.Width / 2;

            Texture2D texture;
            Vector2 location;

            //read in the file
            System.IO.StreamReader worldFile = new System.IO.StreamReader("level1.txt");
            for (int i = 0; i < 11; i++) //number of lines
            {
                string line = worldFile.ReadLine();
                for (int k = 0; k < 35; k++) //length of each line
                {
                    char piece = line[k];
                    if (piece == ' ')
                    { }
                    // yellow ground
                    else if (piece == '#')
                    {
                        texture = Content.Load<Texture2D>(@"images\yellowGround");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // grassy ground
                    else if (piece == '^')
                    {
                        texture = Content.Load<Texture2D>(@"images\grassyGround");
                        location = new Vector2((float)k / 2, (float)i / 2);
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }

                    // platform
                    else if (piece == 't')
                    {
                        texture = Content.Load<Texture2D>(@"images\transparentGround");
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

            // Setup the camera
            Camera.Current.StartTracking(character.Body);
            Camera.Current.CenterPointTarget = ConvertUnits.ToDisplayUnits(
                goal.Body.Position.X);

            // event listeners
            character.Body.OnCollision += Character_Falls;
        }
        #endregion

        #region HandleKeyboard
        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();

            //move left
            if(state.IsKeyDown(Keys.Left))
            {
                character.Body.ApplyTorque(-0.01f);
            }

            //move right
            if (state.IsKeyDown(Keys.Right))
            {
                character.Body.ApplyTorque(0.01f);
            }

            //jump
            if ((state.IsKeyDown(Keys.Space) && jumpOldKeyState.IsKeyUp(Keys.Space)) || (state.IsKeyDown(Keys.Up) && jumpOldKeyState.IsKeyUp(Keys.Up)))
            {
                character.Jump();
            }

            //stone
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                isStone = true;
                character.Body.ResetDynamics();
                character.Body.Rotation = 0;
                character.Body.ApplyForce(new Vector2(0, 5));
            }

            //reset character
            if (state.IsKeyDown(Keys.R))
            {
                ResetCharacter();
            }

            jumpOldKeyState = state;
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

            //draw goal
            goal.Draw(spriteBatch);

            //draw character
            if(isStone)
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
            isStone = false;
            
            //draw grounds
            foreach(Ground g in grounds)
            {
                g.Draw(spriteBatch);
            }

            //draw pit
            foreach(Pit p in pits)
            {
                p.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}