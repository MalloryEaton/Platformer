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
    class LevelClearedScreen
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            string playerWinString = "You Cleared the Level!";
            spriteBatch.DrawString(Game1.font, playerWinString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playerWinString).Length() + 100), 200), Color.Black);
            string playagain = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 100), 250), Color.Black);
        }
    }
}