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
                ConvertUnits.ToSimUnits(groundTexture.Height - 5), 1f, position);
            Body.IsStatic = true;
            Body.Restitution = 0f;
            if (Game1.currentLevel == 2)
            {
                Body.Friction = 0.05f;
            }
            else
            {
                Body.Friction = 1f;
            }
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1;
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