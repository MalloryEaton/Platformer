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

            Body.UserData = "ground";
            Body.IsStatic = true;
            Body.Restitution = 0f;
            Body.Friction = 1f;
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1;

            Body.SleepingAllowed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(groundTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(groundTexture.Width / 2, groundTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}