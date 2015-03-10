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
            Body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(frameSize.X / 2), ConvertUnits.ToSimUnits(frameSize.Y / 2), 1f, position);
            //Origin = new Vector2(ConvertUnits.ToSimUnits(frameSize.X / 2),
            //    ConvertUnits.ToSimUnits(frameSize.Y / 2));
            Origin = new Vector2(0f, 0f);
        }

        public void MoveRight()
        {
            //TODO: adjust spritesheet to make him appear to move right. Actual movement will be done in the Game class.
        }

        public void MoveLeft()
        {
            //TODO: adjust spritesheet to make him appear to move left.  Actual movement will be done in the Game class.
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                new Rectangle(0, 0, frameSize.X, frameSize.Y),
                Color.White,
                0f,
                Origin,
                1f,
                SpriteEffects.None,
                0f);
        }


    }
}