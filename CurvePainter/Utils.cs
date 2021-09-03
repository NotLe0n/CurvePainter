using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.InteropServices;

namespace CurvePainter
{
    public static class Utils
    {
        [DllImport("SDL2.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_MaximizeWindow(IntPtr window);

        public static void MaximizeWindow(GameWindow window, GraphicsDeviceManager graphics)
        {
            window.Position = Point.Zero;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 50;
            SDL_MaximizeWindow(window.Handle);

            graphics.ApplyChanges();
        }

        public static float GetAngle(Vector2 A, Vector2 B)
        {
            return MathF.Atan2(B.Y - A.Y, B.X - A.X);
        }

        public static float GetMagnitude(this Vector2 v)
        {
            return MathF.Sqrt(Dot(v, v));
        }

        public static float Dot(Vector2 A, Vector2 B)
        {
            return (A.X * B.X) + (A.Y * B.Y);
        }

        public static float Cross(Vector2 A, Vector2 B)
        {
            return (A.X * B.Y) - (A.Y * B.X);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, int width, Color color)
        {
            spriteBatch.Draw(CurvePainter.solid,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)Vector2.Distance(start, end),
                    width)
                , null, color,
                GetAngle(start, end), default, SpriteEffects.None, 0);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, int width, Color color, float rotation = default, Vector2 anchor = default, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(CurvePainter.solid,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)Vector2.Distance(start, end),
                    width)
                , null, color,
                GetAngle(start, end) + rotation, anchor, effects, 0);
        }

        public static Vector2 RotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            Vector2 vector = spinningpoint - center;
            Vector2 result = center;
            result.X += vector.X * cos - vector.Y * sin;
            result.Y += vector.X * sin + vector.Y * cos;
            return result;
        }
    }
}
