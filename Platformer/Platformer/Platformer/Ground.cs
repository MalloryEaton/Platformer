using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Ground
    {
        public Body Body { get; private set; }

        private Texture2D groundTexture;

        public Ground(World world, Texture2D texture, Vector2 position)
        {
            this.groundTexture = texture;

            // Create a body for the ground
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(groundTexture.Width),
                ConvertUnits.ToSimUnits(groundTexture.Height - 4), 1f, position);
            Body.IsStatic = true;
            Body.Restitution = 0f;
            if (Game1.currentLevel == 2)
            {
                Body.Friction = 0.1f;
            }
            else
            {
                Body.Friction = 1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(groundTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(groundTexture.Width / 2, groundTexture.Height / 2),  // origin  / middle point
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}