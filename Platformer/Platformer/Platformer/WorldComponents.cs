using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class WorldComponents
    {
        public Body Body { get; protected set; }

        protected Texture2D texture;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
