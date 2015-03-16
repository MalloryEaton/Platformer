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
        public WaddleDee(World world, Texture2D texture, Vector2 position)
        {
            frameSize = new Point(46, 40);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(8, 1);

            timeSinceLastFrame = 0;
            millisecondsPerFrame = 150;

            Texture = texture;
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(frameSize.X),
                ConvertUnits.ToSimUnits(frameSize.Y), 1f, position);

            Origin = new Vector2(ConvertUnits.ToSimUnits(frameSize.X / 2),
                ConvertUnits.ToSimUnits(frameSize.Y / 2));

            Body.UserData = this;

            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.Cat3;
            Body.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat10;

            Body.SleepingAllowed = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!dead && movingRight)
            {
                Body.Position += new Vector2(0.015f, 0f);
            }
            else if (!dead)
            {
                Body.Position -= new Vector2(0.015f, 0f);
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
                effects,
                0f);
        }


    }
}