using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class Enemy
    {
        public Body Body { get; protected set; }
        public Vector2 Origin { get; set; }
        protected Texture2D Texture { get; set; }
    }
}
