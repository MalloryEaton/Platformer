using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Pit
    {
        public Body Body { get; private set; }

        private Texture2D pitTexture;

        public Pit(World world, Texture2D texture, Vector2 position)
        {
            this.pitTexture = texture;

            // Create a body for the ground
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(pitTexture.Width),
                ConvertUnits.ToSimUnits(pitTexture.Height), 1f, position);
            Body.IsStatic = true;
            //body.IsSensor = true;
            Body.UserData = "pit";
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat10;

            Body.SleepingAllowed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pitTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(pitTexture.Width / 2, pitTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}