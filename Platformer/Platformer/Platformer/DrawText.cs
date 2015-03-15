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
    class DrawText
    {
        public void DrawLivesTimeScore(SpriteBatch spriteBatch)
        {
            float X = ConvertUnits.ToDisplayUnits(Game1.characterX);

            string time = "Time:";
            spriteBatch.DrawString(Game1.font, time,
                new Vector2(X, 25), Color.Black);
            string score = "Score:";
            spriteBatch.DrawString(Game1.font, score,
                new Vector2(X, 50), Color.Black);
            string lives = "Lives: " + Game1.lives;
            spriteBatch.DrawString(Game1.font, lives,
                new Vector2(X, 75), Color.Black);
        }

        public void DrawLoseLife(SpriteBatch spriteBatch)
        {
            float X = ConvertUnits.ToDisplayUnits(Game1.characterX);
            string lostLifeString = "You Lost a Life...";
            spriteBatch.DrawString(Game1.font, lostLifeString,
                new Vector2(X, 200), Color.Black);
            string keepPlaying = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, keepPlaying,
                new Vector2(X, 250), Color.Black);
        }

        public void DrawGameOver(SpriteBatch spriteBatch)
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

        public void DrawLevelCleared(SpriteBatch spriteBatch)
        {
            string levelClearedString = "You Cleared the Level!";
            spriteBatch.DrawString(Game1.font, levelClearedString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(levelClearedString).Length() + 100), 200), Color.Black);
            string playagain = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 100), 250), Color.Black);
        }

        public void DrawWinScreen(SpriteBatch spriteBatch)
        {
            string levelClearedString = "You Win!";
            spriteBatch.DrawString(Game1.font, levelClearedString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(levelClearedString).Length() + 100), 200), Color.Black);
            string playagain = "Press ENTER to Play Again.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 100), 250), Color.Black);
        }
    }
}
