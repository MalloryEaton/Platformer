using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    abstract class Enemy
    {
        //Texture
        protected Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;

        //Framerate
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;

        public Body Body { get; protected set; }
        public Vector2 Origin { get; set; }
        protected Texture2D Texture { get; set; }
        protected bool movingRight = false;
        protected SpriteEffects effects = SpriteEffects.None;
        protected bool dead;

        public void ChangeDirection()
        {
            movingRight = !movingRight;
            if (movingRight)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
        }

        public virtual void Die()
        {
            dead = true;
            Body.ResetDynamics();
            Body.CollisionCategories = Category.Cat5;
            //Body.CollidesWith = Category.None;
            Body.ApplyLinearImpulse(new Vector2(0, -0.25f));
            //Body.SleepingAllowed = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; 
            if (timeSinceLastFrame > millisecondsPerFrame && !dead)
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
    }
}
