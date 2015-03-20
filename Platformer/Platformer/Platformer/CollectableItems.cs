using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class CollectableItems
    {
        public bool isAlive { get; set; }

        public Body Body { get; protected set; }

        protected Texture2D Texture;

        public void Destroy()
        {
            Body.CollisionCategories = Category.Cat9;
            isAlive = false;
        }
    }
}
