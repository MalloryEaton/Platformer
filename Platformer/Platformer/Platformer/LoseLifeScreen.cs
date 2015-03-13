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
    class LoseLifeScreen
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            float X = ConvertUnits.ToDisplayUnits(Game1.characterX);
            string levelClearedString = "You Lost a Life...";
            //spriteBatch.DrawString(Game1.font, levelClearedString,
            //    new Vector2((Game1.HalfScreenWidth)
            //        - (Game1.font.MeasureString(levelClearedString).Length() / 2), 200), Color.Black);
            spriteBatch.DrawString(Game1.font, levelClearedString,
                new Vector2(X, 200), Color.Black);
            string playagain = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2(X, 250), Color.Black);
        }
    }
}