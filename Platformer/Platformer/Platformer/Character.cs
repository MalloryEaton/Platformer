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

        public Vector2 Origin;

        public int lives;

        private Vector2 jumpForce = new Vector2(0, -0.3f); // applied force when jumping

        public Character(World world, Texture2D texture, Vector2 position)
        {
            this.characterTexture = texture;

            //create a body for the character
            Body = BodyFactory.CreateCircle(world,
                ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                1f, position);

            Origin = new Vector2(ConvertUnits.ToSimUnits(characterTexture.Width / 2),
                ConvertUnits.ToSimUnits(characterTexture.Height / 2));

            //Body.Rotation = 0;
            //Body.FixedRotation = true;

            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0f;
            Body.Friction = 1f;
            
        }

        public void Jump()
        {
            //if ((DateTime.Now - previousJump).TotalSeconds >= jumpInterval)
            //{
            //    Body.ApplyLinearImpulse(ref jumpForce);
            //    previousJump = DateTime.Now;
            //}
            Body.ApplyLinearImpulse(ref jumpForce);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(characterTexture.Width / 2, characterTexture.Height / 2),  // origin / middle point
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}