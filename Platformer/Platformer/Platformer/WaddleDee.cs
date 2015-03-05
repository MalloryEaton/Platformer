﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class WaddleDee
    {
        // Texture stuff
        Point frameSize = new Point(46, 40);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(8, 1);

        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 200;
    }
}