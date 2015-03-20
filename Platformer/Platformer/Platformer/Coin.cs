using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Platformer
{
    class Coin : CollectableItems
    {
        //Texture
        Point frameSize;
        Point currentFrame;
        Point sheetSize;

        //Framerate
        int timeSinceLastFrame;
        int millisecondsPerFrame;

        public Vector2 Origin { get; set; }

        public Coin(World world, Texture2D texture, Vector2 position)
        {
            isAlive = true;

            frameSize = new Point(32, 32);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(8, 1);

            timeSinceLastFrame = 0;
            millisecondsPerFrame = 150;

            Texture = texture;
            Body = BodyFactory.CreateCircle(world,
                ConvertUnits.ToSimUnits(20),
                1f,
                position);

            Body.UserData = this;

            Body.IgnoreGravity = true;
            Body.BodyType = BodyType.Static;
            Body.IsSensor = true;
            Body.CollisionCategories = Category.Cat8;
            Body.CollidesWith = Category.Cat10;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                Color.White,
                0f,
                new Vector2(frameSize.X / 2, frameSize.Y / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
