using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Camera
    {
        // Body to center the camera on
        private Body trackingBody;

        public Matrix TransformationMatrix { get; private set; }

        // X value of the target
        public float CenterPointTarget { get; set; }

        // Create a singleton
        public static readonly Camera Current = new Camera();

        private Camera()
        {
            
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f));
        }

        public void Update()
        {
            if (trackingBody != null)
            {
                float pos = ConvertUnits.ToDisplayUnits(trackingBody.Position.X);
                pos = MathHelper.Clamp(pos - Game1.HalfScreenWidth, 0, CenterPointTarget - Game1.HalfScreenWidth);
                Position = new Vector2(pos, 0f) ;
            }
        }

        public void StartTracking(Body body)
        {
            trackingBody = body;
        }

        public void StopTracking()
        {
            trackingBody = null;
        }
    }

}
