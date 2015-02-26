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
    class Character
    {
        public Body Body { get; private set; }

        private Texture2D texture;

        public Character(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            //create a body for the character
            Body = BodyFactory.CreateCircle(world, 
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), position);

            Body.BodyType = BodyType.Dynamic;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                ConvertUnits.ToDisplayUnits(Body.Position),
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
