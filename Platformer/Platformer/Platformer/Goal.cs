using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Goal
    {
        public Body Body { get; private set; }

        private Texture2D goalTexture;

        public Goal(World world, Texture2D texture, Vector2 position)
        {
            this.goalTexture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(goalTexture.Width),
                ConvertUnits.ToSimUnits(goalTexture.Height), 1f, position);
            Body.IsStatic = true;
            Body.IsSensor = true;
            Body.UserData = "goal";
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1;

            Body.SleepingAllowed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goalTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(goalTexture.Width / 2, goalTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}