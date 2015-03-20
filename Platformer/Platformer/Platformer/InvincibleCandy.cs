using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class InvincibleCandy : CollectableItems
    {
        public bool isCandy = false;

        public InvincibleCandy(World world, Texture2D texture, Vector2 position)
        {
            isAlive = true;

            this.Texture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);
            
            Body.IsStatic = true;
            Body.IsSensor = true;
            Body.Restitution = 0f;
            Body.Friction = 0f;
            Body.Rotation = 0f;
            Body.FixedRotation = true;

            Body.UserData = "candy";

            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat10;

            Body.SleepingAllowed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}