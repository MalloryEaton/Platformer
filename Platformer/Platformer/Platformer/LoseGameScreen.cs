using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer
{
    class LoseGameScreen
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            string loseString = "You Lost...";
            spriteBatch.DrawString(Game1.font, loseString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(loseString).Length() + 100), 200), Color.Black);
            string playagain = "Press ENTER to Play Again.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 100), 250), Color.Black);
        }
    }
}