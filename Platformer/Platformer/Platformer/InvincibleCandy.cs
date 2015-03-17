using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class InvincibleCandy
    {
        public bool IsAlive { get; private set; }

        public Body Body { get; private set; }

        private Texture2D candyTexture;

        public InvincibleCandy(World world, Texture2D texture, Vector2 position)
        {
            IsAlive = true;

            this.candyTexture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(candyTexture.Width),
                ConvertUnits.ToSimUnits(candyTexture.Height), 1f, position);

            
            Body.IsStatic = false;
            Body.Restitution = 0f;
            Body.Friction = 0f;
            Body.Rotation = 0f;
            Body.FixedRotation = true;

            Body.UserData = "candy";

            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat10;

            Body.SleepingAllowed = false;
        }

        public void Destroy()
        {
            Body.IsSensor = true;
            IsAlive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(candyTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(candyTexture.Width / 2, candyTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}