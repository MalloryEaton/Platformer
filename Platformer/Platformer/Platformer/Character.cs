﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class Character
    {
        public Body Body { get; private set; }

        private Texture2D texture;

        private DateTime previousJump = DateTime.Now;   // time at which we previously jumped
        private const float jumpInterval = 1.0f;        // in seconds
        private Vector2 jumpForce = new Vector2(0, -0.1f); // applied force when jumping

        public Character(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            //create a body for the character
            Body = BodyFactory.CreateCircle(world, 
                ConvertUnits.ToSimUnits(texture.Width / 2),
                ConvertUnits.ToSimUnits(texture.Height / 2), position);

            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0.5f;
            Body.Friction = 1000f;
        }

        public void Jump()
        {
            if ((DateTime.Now - previousJump).TotalSeconds >= jumpInterval)
            {
                Body.ApplyLinearImpulse(ref jumpForce);
                previousJump = DateTime.Now;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),  // origin / middle point
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
