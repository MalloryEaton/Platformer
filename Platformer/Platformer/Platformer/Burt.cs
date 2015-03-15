using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class Burt : Enemy
    {
        public Burt(World world, Texture2D texture, Vector2 position)
        {
            frameSize = new Point(44, 35);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(4, 1);

            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;

            Texture = texture;
            //Body = BodyFactory.CreateRectangle(world, 
            //    ConvertUnits.ToSimUnits(frameSize.X), 
            //    ConvertUnits.ToSimUnits(frameSize.Y), 1f, position);
            Body = BodyFactory.CreateCircle(world,
                ConvertUnits.ToSimUnits(19),
                1f,
                position);

            Origin = new Vector2(ConvertUnits.ToSimUnits(frameSize.X / 2),
                ConvertUnits.ToSimUnits(frameSize.Y / 2));
            //Origin = new Vector2(0f, 0f);

            Body.UserData = this;

            //Body.IsSensor = true;
            Body.IgnoreGravity = true;
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.Cat6;
            Body.CollidesWith = Category.Cat10;

            Body.SleepingAllowed = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!dead && movingRight)
            {
                Body.Position += new Vector2(0.03f, 0f);
            }
            else if (!dead)
            {
                Body.Position -= new Vector2(0.03f, 0f);
                if (Game1.characterY > Body.Position.Y)
                {
                    Body.Position += new Vector2(0f, 0.01f);
                }
                if (Game1.characterY < Body.Position.Y)
                {
                    Body.Position -= new Vector2(0f, 0.01f);
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
                effects,
                0f);
        }

        public override void Die()
        {
            Body.IgnoreGravity = false;
            base.Die();
        }
    }
}