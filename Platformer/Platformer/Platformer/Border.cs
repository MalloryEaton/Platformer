using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Border : WorldComponents
    {
        public Border(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);
            Body.IsStatic = true;
            Body.Restitution = 0f;
            Body.Friction = 0f;
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat10;
            Body.UserData = "border";

            Body.SleepingAllowed = false;
        }
    }
}