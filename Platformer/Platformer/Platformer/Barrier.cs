using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Barrier : WorldComponents
    {
        public Barrier(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);

            Body.BodyType = BodyType.Static;
            Body.UserData = "barrier";
            Body.IsSensor = true;
            Body.Restitution = 0f;
            Body.Friction = 1f;
            Body.CollisionCategories = Category.Cat2;
            Body.CollidesWith = Category.Cat3 | Category.Cat4;

            Body.SleepingAllowed = false;
        }
    }
}
