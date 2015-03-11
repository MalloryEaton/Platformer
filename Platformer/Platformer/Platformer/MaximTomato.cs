using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class MaximTomato
    {
        public bool IsAlive { get; private set; }

        public Body Body { get; private set; }

        private Texture2D tomatoTexture;

        public MaximTomato(World world, Texture2D texture, Vector2 position)
        {
            IsAlive = true;

            this.tomatoTexture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(tomatoTexture.Width),
                ConvertUnits.ToSimUnits(tomatoTexture.Height), 1f, position);

            
            Body.IsStatic = false;
            Body.Restitution = 0f;
            Body.Friction = 0f;
            Body.Rotation = 0f;
            Body.FixedRotation = true;

            Body.UserData = "tomato";

            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1;

            Body.SleepingAllowed = false;
        }

        public void Destroy()
        {
            Body.IsSensor = true;
            IsAlive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tomatoTexture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(tomatoTexture.Width / 2, tomatoTexture.Height / 2),  // origin
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}