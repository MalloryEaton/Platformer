using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class WaddleDee : Enemy
    {
        
        // Texture stuff
        Point frameSize = new Point(46, 40);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(8, 1);
        
        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 200;

        public WaddleDee(World world, Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Body = BodyFactory.CreateRectangle(world, 
                ConvertUnits.ToSimUnits(frameSize.X), 
                ConvertUnits.ToSimUnits(frameSize.Y), 1f, position);

            //Origin = new Vector2(ConvertUnits.ToSimUnits(frameSize.X / 2),
                //ConvertUnits.ToSimUnits(frameSize.Y / 2));
            Origin = new Vector2(0f, 0f);

            Body.CollisionCategories = Category.Cat3;
            Body.CollidesWith = Category.Cat1 | Category.Cat2;
        }

        public void MoveRight()
        {
            //TODO: adjust spritesheet to make him appear to move right. Actual movement will be done in the Game class.
        }

        public void MoveLeft()
        {
            //TODO: adjust spritesheet to make him appear to move left.  Actual movement will be done in the Game class.
        }

        //protected void Update(GameTime gameTime)
        //{
        //    timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; if (timeSinceLastFrame > millisecondsPerFrame)
        //    {
        //        timeSinceLastFrame -= millisecondsPerFrame;
        //        ++currentFrame.X;
        //        if (currentFrame.X >= sheetSize.X)
        //        {
        //            currentFrame.X = 0;
        //            ++currentFrame.Y;
        //            if (currentFrame.Y >= sheetSize.Y)
        //                currentFrame.Y = 0;
        //        }
        //    }
        //}

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
                Origin,
                1f,
                SpriteEffects.None,
                0f);
        }


    }
}