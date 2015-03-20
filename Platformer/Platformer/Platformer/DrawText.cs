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
            int score = Game1.score;
            double timerSec = Game1.timerSec;
            int timerMin = Convert.ToInt32(Game1.timerMin);
            
            
            string timeString = "Time: " + timerSec.ToString("0.#");
            spriteBatch.DrawString(Game1.smallFont, timeString,
                new Vector2(10, 25), Color.Black);
            string scoreString = "Score: " + score;
            spriteBatch.DrawString(Game1.smallFont, scoreString,
                new Vector2(10, 50), Color.Black);
            string lives = "Lives: " + Game1.lives;
            spriteBatch.DrawString(Game1.smallFont, lives,
                new Vector2(10, 75), Color.Black);
        }

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
            int score = Game1.score;

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
                    - (Game1.font.MeasureString(finalScore).Length() - 375), 300), Color.Black);
            if(Game1.enemyNum == 23)
            {
                string enemy = "You killed all of the enemies!";
                spriteBatch.DrawString(Game1.font, enemy,
                    new Vector2((Game1.HalfScreenWidth)
                        - (Game1.font.MeasureString(enemy).Length() - 375), 350), Color.Black);
            }
            if (Game1.coinNum == 47)
            {
                string coin = "You collected all of the coins!";
                spriteBatch.DrawString(Game1.font, coin,
                    new Vector2((Game1.HalfScreenWidth)
                        - (Game1.font.MeasureString(coin).Length() - 375), 400), Color.Black);
            }
        }
    }
}
