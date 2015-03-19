using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Goal : WorldComponents
    {
        public Goal(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            // Create a body
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);
            Body.IsStatic = true;
            Body.IsSensor = true;
            Body.UserData = "goal";
            Body.CollisionCategories = Category.Cat1;
            Body.CollidesWith = Category.Cat10;

            Body.SleepingAllowed = false;
        }
    }
}