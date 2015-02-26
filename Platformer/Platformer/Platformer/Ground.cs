using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Ground
    {
        public Body Body { get; private set; }

        private Texture2D texture;

        public Ground(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            // Create a body for our projectile
            Body = BodyFactory.CreateRectangle(world,
                ConvertUnits.ToSimUnits(texture.Width * 2),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                ConvertUnits.ToDisplayUnits(Body.Position) + new Vector2(texture.Width / 2, 0),
                null,
                Color.White,
                Body.Rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),  // origin
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}