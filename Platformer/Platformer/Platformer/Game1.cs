using FarseerPhysics;
using FarseerPhysics.Dynamics;
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

        private KeyboardState oldKeyState;
        private MouseState lastMouseState;

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

            CreateGameComponents();
        }
        #endregion

        #region CreateGameComponents (contains reading from file)
        private void CreateGameComponents()
        {
            world.Clear();
            grounds = new List<Ground>();

            Texture2D texture;
            Vector2 location;

            //read in the file
            System.IO.StreamReader worldFile = new System.IO.StreamReader("level1.txt");
            for (int i = 0; i < 10; i++) //number of lines
            {
                string line = worldFile.ReadLine();
                for (int k = 0; k < 16; k++) //length of each line
                {
                    char piece = line[k];
                    if (piece == ' ')
                    {

                    }
                    else if (piece == '#')
                    {
                        texture = Content.Load<Texture2D>(@"images\yellowGround");
                        location = new Vector2((float)k / 2, (float)i / 2 + ConvertUnits.ToSimUnits(25f));
                        ground = new Ground(world, texture, location);
                        grounds.Add(ground);
                    }
                    else if (piece == 'P')
                    {
                        texture = Content.Load<Texture2D>(@"images\kirby");
                        location = new Vector2((float)(k / 2) + ConvertUnits.ToSimUnits(25f), (float)(i / 2));
                        characterInitPos = location;
                        character = new Character(world, texture, location);
                    }
                }
            }

            // Setup the camera
            Camera.Current.StartTracking(character.Body);
            //Camera.Current.CenterPointTarget = ConvertUnits.ToDisplayUnits(
                //target.Body.Position.X);
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

        #region HandleMouseInput
        private void HandleMouseInput()
        {
            MouseState mouseState = Mouse.GetState();

            // Check for initial right click
            if (lastMouseState.RightButton == ButtonState.Released &&
                mouseState.RightButton == ButtonState.Pressed)
            {
                CreateGameComponents();
            }

            lastMouseState = mouseState;
        }
        #endregion

        #region HandleKeyboard
        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.Left))
            {
                character.Body.ApplyTorque(-0.1f);
            }

            if (state.IsKeyDown(Keys.Right))
            {
                character.Body.ApplyTorque(0.1f);
            }

            if ((state.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space)) || (state.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up)))
            {
                character.Jump();
            }

            //reset character
            if (state.IsKeyDown(Keys.R))
            {
                character.Body.ResetDynamics();
                character.Body.Rotation = 0;
                character.Body.Position = characterInitPos;
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
            HandleMouseInput();
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

            character.Draw(spriteBatch);
            foreach(Ground g in grounds)
            {
                g.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}