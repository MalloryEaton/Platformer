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

        private Texture2D characterTexture;
        public Texture2D invincibleTexture;

        public Vector2 characterInitPos;

        public Vector2 Origin;

        public Vector2 jumpForce = new Vector2(0, -0.25f); // applied force when jumping

        public bool losesLife = false;
        public bool gameOver = false;
        public bool isInvincible = false;
        public bool onGoal = false;

        public int jumpNum = 0;

        public Character(World world, Texture2D texture, Vector2 position)
        {
            this.characterTexture = texture;
            
            //create a body for the character
            Body = BodyFactory.CreateCircle(world,
                ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                1f, position);

            Origin = new Vector2(ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                ConvertUnits.ToSimUnits(characterTexture.Height / 2));

            Body.UserData = "player";

            Body.Rotation = 0;
            Body.FixedRotation = true;

            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0f;
            Body.Friction = 1f;

            if (Game1.titleScreenIsPlaying)
            {
                Body.CollisionCategories = Category.Cat15;
                Body.CollidesWith = Category.Cat1;
            }
            else
            {
                Body.CollisionCategories = Category.Cat10;
                Body.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat6 | Category.Cat8;
            }

            Body.SleepingAllowed = false;
        }

        public void Jump(Vector2 jumpForce)
        {
            Body.ApplyLinearImpulse(jumpForce);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(characterTexture.Width / 2, characterTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
        public void DrawInvincible(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(invincibleTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(invincibleTexture.Width / 2, invincibleTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}