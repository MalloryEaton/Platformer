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
        #region DrawLivesTimeScore
        public void DrawLivesTimeScore(SpriteBatch spriteBatch)
        {
            int score = Game1.score;
            double timerSec = Game1.timerSec;
            int level = Game1.currentLevel;
            
            if(level == 2)
            {
                string timeString = "Time: " + timerSec.ToString("0.#");
                spriteBatch.DrawString(Game1.smallFont, timeString,
                    new Vector2(10, 25), Color.Red);
                string scoreString = "Score: " + score;
                spriteBatch.DrawString(Game1.smallFont, scoreString,
                    new Vector2(10, 50), Color.Red);
                string lives = "Lives: " + Game1.lives;
                spriteBatch.DrawString(Game1.smallFont, lives,
                    new Vector2(10, 75), Color.Red);
                string levelString = "Level: " + level;
                spriteBatch.DrawString(Game1.font, levelString,
                    new Vector2(Game1.HalfScreenWidth -
                        (Game1.font.MeasureString(levelString).Length() / 2), 1), Color.Red);
            }
            else
            {
                string timeString = "Time: " + timerSec.ToString("0.#");
                spriteBatch.DrawString(Game1.smallFont, timeString,
                    new Vector2(10, 25), Color.Black);
                string scoreString = "Score: " + score;
                spriteBatch.DrawString(Game1.smallFont, scoreString,
                    new Vector2(10, 50), Color.Black);
                string lives = "Lives: " + Game1.lives;
                spriteBatch.DrawString(Game1.smallFont, lives,
                    new Vector2(10, 75), Color.Black);
                string levelString = "Level: " + level;
                spriteBatch.DrawString(Game1.font, levelString,
                    new Vector2(Game1.HalfScreenWidth -
                        (Game1.font.MeasureString(levelString).Length() / 2), 1), Color.Black);
            }
        }
        #endregion

        #region DrawLoseLife
        public void DrawLoseLife(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            string lostLifeString = "You Lost a Life...";
            spriteBatch.DrawString(Game1.font, lostLifeString,
                new Vector2((graphicsDevice.Viewport.Width / 2) - 
                    (Game1.font.MeasureString(lostLifeString).Length() / 2), 
                    (graphicsDevice.Viewport.Height / 2) - 25), Color.Black);
            string keepPlaying = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, keepPlaying,
                new Vector2((graphicsDevice.Viewport.Width / 2) - 
                    (Game1.font.MeasureString(keepPlaying).Length() / 2), 
                    (graphicsDevice.Viewport.Height / 2) + 25), Color.Black);
        }
        #endregion

        #region DrawGameOver
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
            string exit = "Press ESCAPE to Exit.";
            spriteBatch.DrawString(Game1.font, exit,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(exit).Length() + 100), 300), Color.Black);
        }
        #endregion

        #region DrawLevelCleared
        public void DrawLevelCleared(SpriteBatch spriteBatch)
        {
            int score = Game1.score;
            double timerSec = Game1.timerSec;
            int coinNum = Game1.coinNum;

            string levelClearedString = "You Cleared the Level!";
            spriteBatch.DrawString(Game1.font, levelClearedString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(levelClearedString).Length() + 100), 150), Color.Black);
            string playagain = "Press ENTER to Continue.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 100), 200), Color.Black);
            string levelScore = "Score: " + score;
            spriteBatch.DrawString(Game1.font, levelScore,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(levelScore).Length() + 100), 300), Color.Black);
            string time = "Time: " + timerSec.ToString("0.#") + " seconds";
            spriteBatch.DrawString(Game1.font, time,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(time).Length() + 100), 350), Color.Black);
        }
        #endregion

        #region DrawWinScreen
        public void DrawWinScreen(SpriteBatch spriteBatch)
        {
            int score = Game1.score;
            double timerSec = Game1.timerSec;
            int timeBonus = 0;

            if(timerSec <= 100)
            {
                score += 1000;
                timeBonus = 1000;
            }
            else if(timerSec <= 110 && timerSec > 100)
            {
                score += 900;
                timeBonus = 900;
            }
            else if (timerSec <= 120 && timerSec > 110)
            {
                score += 800;
                timeBonus = 800;
            }
            else if (timerSec <= 130 && timerSec > 120)
            {
                score += 700;
                timeBonus = 700;
            }
            else if (timerSec <= 140 && timerSec > 130)
            {
                score += 600;
                timeBonus = 600;
            }
            else if (timerSec <= 150 && timerSec > 140)
            {
                score += 500;
                timeBonus = 500;
            }

            if (Game1.enemyNum == 23)
            {
                score += 1000;
                string enemy = "You killed all of the enemies! +1000";
                spriteBatch.DrawString(Game1.font, enemy,
                    new Vector2((Game1.HalfScreenWidth)
                        - (Game1.font.MeasureString(enemy).Length() - 395), 410), Color.Black);
            }
            if (Game1.coinNum == 47)
            {
                score += 1000;
                string coin = "You collected all of the coins! +1000";
                spriteBatch.DrawString(Game1.font, coin,
                    new Vector2((Game1.HalfScreenWidth)
                        - (Game1.font.MeasureString(coin).Length() - 395), 450), Color.Black);
            }
            string levelClearedString = "You Win!";
            spriteBatch.DrawString(Game1.font, levelClearedString,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(levelClearedString).Length() + 150), 200), Color.Black);
            string playagain = "Press ENTER to Play Again.";
            spriteBatch.DrawString(Game1.font, playagain,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(playagain).Length() + 150), 250), Color.Black);
            string exit = "Press ESCAPE to Exit.";
            spriteBatch.DrawString(Game1.font, exit,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(exit).Length() + 150), 300), Color.Black);
            string finalScore = "Final Score: " + score;
            spriteBatch.DrawString(Game1.font, finalScore,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(finalScore).Length() - 395), 330), Color.Black);
            string time = "Final Time: " + timerSec.ToString("0.#") + " seconds +" + timeBonus;
            spriteBatch.DrawString(Game1.font, time,
                new Vector2((Game1.HalfScreenWidth)
                    - (Game1.font.MeasureString(time).Length() - 395), 370), Color.Black);
        }
        #endregion
    }
}
