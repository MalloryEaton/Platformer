using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Border
    {
        public Body Body { get; private set; }

        private Texture2D borderTexture;

        public Border(World world, Texture2D texture, Vector2 position)
        {
            this.borderTexture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(borderTexture.Width),
                ConvertUnits.ToSimUnits(borderTexture.Height), 1f, position);
            Body.IsStatic = true;
            Body.Restitution = 0f;
            Body.Friction = 0f;
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat3;
            Body.UserData = "border";

            Body.SleepingAllowed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(borderTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(borderTexture.Width / 2, borderTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}