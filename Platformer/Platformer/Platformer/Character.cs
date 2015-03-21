using FarseerPhysics;
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
        private Texture2D invincibleTexture;

        //stone
        private Texture2D stoneTexture;
        private Vector2 stoneOrigin;

        public Vector2 characterInitPos;

        public Vector2 Origin;

        public Vector2 jumpForce = new Vector2(0, -0.25f); // applied force when jumping

        public bool IsStone { get; set; }
        public bool LosesLife { get; set; }
        public bool IsInvincible { get; set; }
        public bool OnGoal { get; set; }

        public int jumpNum = 0;

        public Character(World world, Texture2D texture, Texture2D invincible, Texture2D stone, Vector2 position)
        {
            LosesLife = false;
            IsInvincible = false;
            OnGoal = false;
            IsStone = false;
            this.characterTexture = texture;

            invincibleTexture = invincible;
            stoneTexture = stone;
            stoneOrigin = new Vector2(ConvertUnits.ToSimUnits(stoneTexture.Width / 2),
                ConvertUnits.ToSimUnits(stoneTexture.Height / 2));

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
            if (IsInvincible)
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
            else if (IsStone)
            {
                spriteBatch.Draw(stoneTexture,
                    ConvertUnits.ToDisplayUnits(Body.Position - stoneOrigin),
                    null, Color.White, 0,
                    stoneOrigin,
                    1f, SpriteEffects.None, 0f);
            }
            else
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
        }
    }
}