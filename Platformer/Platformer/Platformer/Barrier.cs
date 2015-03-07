using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Barrier
    {
        public Body Body { get; private set; }

        private Texture2D barrierTexture;

        public Barrier(World world, Texture2D texture, Vector2 position)
        {
            this.barrierTexture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(barrierTexture.Width),
                ConvertUnits.ToSimUnits(barrierTexture.Height), 1f, position);
            Body.IsStatic = true;
            Body.Restitution = 0f;
            Body.Friction = 1f;
            Body.CollisionCategories = Category.Cat2;
            Body.CollidesWith = Category.Cat3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barrierTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(barrierTexture.Width / 2, barrierTexture.Height / 2),  // origin  / middle point
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
