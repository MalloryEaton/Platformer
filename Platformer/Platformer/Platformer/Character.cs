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
        public Texture2D invincibleTexture;

        public Vector2 Origin;

        public int lives = 3;

        public int jumpNum = 6;

        

        public Character(World world, Texture2D texture, Vector2 position)
        {
            this.characterTexture = texture;
            
            //create a body for the character
            Body = BodyFactory.CreateCircle(world,
                ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                1f, position);

            Origin = new Vector2(ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                ConvertUnits.ToSimUnits(characterTexture.Height / 2));

            Body.Rotation = 0;
            Body.FixedRotation = true;

            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0f;
            Body.Friction = 1f;

            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat3;

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