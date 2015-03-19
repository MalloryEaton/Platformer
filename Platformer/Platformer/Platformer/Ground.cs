using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Ground : WorldComponents
    {
        public Ground(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            // Create a body for the ground
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height - 5), 1f, position);

            Body.UserData = "ground";
            Body.IsStatic = true;
            Body.Restitution = 0f;
            Body.Friction = 1f;
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat4 | Category.Cat10 | Category.Cat15;

            Body.SleepingAllowed = false;
        }
    }
}